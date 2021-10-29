using Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUserById(string id);
        Task AddUser(User user);
        Task<User> UpdateUser(User user);
        Task UpdateUsers(IEnumerable<User> users);
    }
}
