using System.Threading.Tasks;

namespace DAL
{
    public interface IBlob
    {
        Task<string> CreateFile(string file, string refFile);
        Task<string> GetBlob(string file);
        Task<bool> DeleteBlob(string file);
    }
}
