using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using System;
using System.IO;
using System.Threading.Tasks;

namespace DAL
{
    public class Blob : IBlob
    {

        public async Task<string> CreateFile(string file, string refFile)
        {
            var containerClient = await GetContainerClient();

            var blobReference = containerClient.GetBlockBlobReference(refFile);
            var bytes = Decode(file);
            using (var stream = new MemoryStream(bytes))
            {
                await blobReference.UploadFromStreamAsync(stream);
            }
            return $"{refFile}";
        }

        public async Task<string> GetBlob(string file)
        {
            var containerClient = await GetContainerClient();
            var blob = containerClient.GetBlockBlobReference($"{file}");
            return blob.StorageUri.PrimaryUri.ToString();
        }

        public async Task<bool> DeleteBlob(string file)
        {
            var containerClient = await GetContainerClient();
            var blob = containerClient.GetBlockBlobReference($"{file}");
            var isDeleted = await blob.DeleteIfExistsAsync();
            return isDeleted;
        }

        private async Task<CloudBlobContainer> GetContainerClient()
        {
            var connectionstring = Environment.GetEnvironmentVariable("BlobStorage");
            var storageAccount = CloudStorageAccount.Parse(connectionstring);
            var serviceClient = storageAccount.CreateCloudBlobClient();
            var container = serviceClient.GetContainerReference(
                    $"{Environment.GetEnvironmentVariable("FileContainer")}"
            );
            await container.CreateIfNotExistsAsync();
            return container;
        }

        public static byte[] Decode(string input)
        {
            var output = input;

            output = output.Replace('-', '+');
            output = output.Replace('_', '/');

            switch (output.Length % 4)
            {
                case 0:
                    break;
                case 2:
                    output += "==";
                    break;
                case 3:
                    output += "=";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(
                        nameof(input), "Illegal base64url string!");
            }

            var converted = Convert.FromBase64String(output);

            return converted;
        }
    }
}
