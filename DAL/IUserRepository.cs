using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUserById(string id);
        Task<User> AddUser(User user);
        Task<User> UpdateUser(User user);
        Task UpdateUsers(IEnumerable<User> users);
    }
}
