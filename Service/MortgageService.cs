using DAL;
using Domain;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class MortgageService : IMortgageService
    {
        private readonly IUserService _UserService;
        private readonly IBlob _Blob;
        private readonly string _ServiceBusConnectString = Environment.GetEnvironmentVariable("AzureConnectionString");
        private readonly string _CreateQueueName = Environment.GetEnvironmentVariable("ServiceBusNameCreate");
        private readonly string _DeleteQueueName = Environment.GetEnvironmentVariable("ServiceBusNameDelete");
        private readonly string _SendQueueName = Environment.GetEnvironmentVariable("ServiceBusNameSend");

        public MortgageService(IUserService userService, IBlob blob)
        {
            _UserService = userService;
            _Blob = blob;
        }

        public async Task AddMortgage(string queue)
        {
            var userids = System.Text.Json.JsonSerializer.Deserialize<List<string>>(queue);

            foreach (var userid in userids)
            {
                var user = await _UserService.GetUserById(userid);
                var mortgage = new Mortgage()
                {
                    id = Guid.NewGuid().ToString(),
                    Email = user.Email,
                    Amount = user.Income,
                    ZipCode = user.ZipCode
                };

                var pdf = PDFMaker.CreatePDF(mortgage);
                user.PDF = await _Blob.CreateFile(Convert.ToBase64String(pdf), Guid.NewGuid() + ".pdf");
                await _UserService.UpdateUser(user);
            }
        }

        public async Task CreateMortgageQueue()
        {
            var users = await _UserService.GetUsers();

            if (!string.IsNullOrEmpty(_CreateQueueName))
            {
                IQueueClient client = new QueueClient(_ServiceBusConnectString, _CreateQueueName);
                var batchListOfUsers = Splitter.Split(users.ToList());
                foreach (var batchusers in batchListOfUsers)
                {
                    var messageBody = JsonConvert.SerializeObject(batchusers.Select(a => a.id));
                    var message = new Message(Encoding.UTF8.GetBytes(messageBody));
                    await client.SendAsync(message);
                }
            }
        }

        public async Task SendEmailWithMortgages(string queueString)
        {
            var userids = System.Text.Json.JsonSerializer.Deserialize<List<string>>(queueString);
            foreach (var userid in userids)
            {
                var user = await _UserService.GetUserById(userid);
                var link = await _Blob.GetBlob(user.PDF);
                var email = new EmailAddress(user.Email);
                Email.Send(email, "Mortgage PDF", "", $"<a href='{link}'>Your mortgage.</a>");
            }
        }

        public async Task SendMortgageQueue()
        {
            var users = await _UserService.GetUsers();

            if (!string.IsNullOrEmpty(_CreateQueueName))
            {
                IQueueClient client = new QueueClient(_ServiceBusConnectString, _SendQueueName);
                var batchListOfUsers = Splitter.Split(users.ToList());
                foreach (var batchusers in batchListOfUsers)
                {
                    var messageBody = JsonConvert.SerializeObject(batchusers.Select(a => a.id));
                    var message = new Message(Encoding.UTF8.GetBytes(messageBody));
                    await client.SendAsync(message);
                }
            }
        }

        public async Task DeleteMortgage(string queueString)
        {
            var fileIds = System.Text.Json.JsonSerializer.Deserialize<List<string>>(queueString);

            foreach (var fileId in fileIds)
            {
                await _Blob.DeleteBlob(fileId);
            }
        }

        public async Task DeleteMortgageQueue()
        {
            var users = await _UserService.GetUsers();

            if (!string.IsNullOrEmpty(_DeleteQueueName))
            {
                IQueueClient client = new QueueClient(_ServiceBusConnectString, _DeleteQueueName);
                var usersMortgage = users.ToList().Where(a => !string.IsNullOrEmpty(a.PDF)).ToList();
                var batchListOfUsers = Splitter.Split(usersMortgage);
                foreach (var batchusers in batchListOfUsers)
                {
                    var messageBody = JsonConvert.SerializeObject(batchusers.Select(a => a.PDF));
                    batchusers.ForEach(a => a.PDF = "");
                    await _UserService.UpdateUsers(batchusers);
                    var message = new Message(Encoding.UTF8.GetBytes(messageBody));
                    await client.SendAsync(message);
                }
            }
        }
    }
}
