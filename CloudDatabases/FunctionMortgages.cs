using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Service;
using System.Threading.Tasks;
using ServiceBusTriggerAttribute = Microsoft.Azure.Functions.Worker.ServiceBusTriggerAttribute;

namespace CloudDatabases
{
    class FunctionMortgages
    {
        private readonly IMortgageService _MortgageService;
        public FunctionMortgages(IMortgageService mortgageService)
        {
            _MortgageService = mortgageService;
        }


        [Function("CalculateMortgages")]
        public async void StartCalculateMortgages([TimerTrigger("0 0 0 * * *")] TimerInfo timerInfo, FunctionContext context, ILogger log)
        {
            await _MortgageService.CreateMortgageQueue();
        }

        [Function("SendMails")]
        public async Task SendMails([TimerTrigger("0 0 8 * * *")] TimerInfo timerInfo, ILogger log)
        {
            await _MortgageService.SendMortgageQueue();
        }


        [Function("DeleteMortgages")]
        public async Task DeleteMortgage([TimerTrigger("0 0 20 * * *")] TimerInfo timerInfo, ILogger log)
        {
            await _MortgageService.DeleteMortgageQueue();
        }

        [Function("CalculateMortgagesQueue")]
        public async Task CalculateMortgages([ServiceBusTrigger("mortgages", Connection = "AzureConnectionString")] string myQueueItem, FunctionContext context)
        {
            await _MortgageService.AddMortgage(myQueueItem);
        }

        [Function("SendMortgagesQueue")]
        public async Task SendMortgages([ServiceBusTrigger("sendPDF", Connection = "AzureConnectionString")] string myQueueItem, FunctionContext context)
        {
            await _MortgageService.SendEmailWithMortgages(myQueueItem);
        }

        [Function("DeleteMortgagesQueue")]
        public async Task DeleteMortgages([ServiceBusTrigger("deletePDF", Connection = "AzureConnectionString")] string myQueueItem, FunctionContext context)
        {
            await _MortgageService.DeleteMortgage(myQueueItem);
        }
    }
}
