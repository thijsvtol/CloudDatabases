using System.Threading.Tasks;

namespace Service
{
    public interface IMortgageService
    {
        Task SendEmailWithMortgages(string queue);
        Task SendMortgageQueue();
        Task AddMortgage(string mortgage);
        Task CreateMortgageQueue();
        Task DeleteMortgage(string queue);
        Task DeleteMortgageQueue();
    }
}
