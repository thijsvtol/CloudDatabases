using DAL;
using Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _UserRepository;

        public UserService(IUserRepository userRepository)
        {
            _UserRepository = userRepository;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _UserRepository.GetUsers();
        }

        public async Task<User> GetUserById(string userid)
        {
            return await _UserRepository.GetUserById(userid);
        }

        public async Task AddUser(User user)
        {
            user.id = Guid.NewGuid().ToString();
            var createUser = await _UserRepository.AddUser(user);
        }

        public async Task<User> UpdateUser(User user)
        {
            return await _UserRepository.UpdateUser(user);
        }

        public async Task UpdateUsers(IEnumerable<User> users)
        {
            await _UserRepository.UpdateUsers(users);
        }
    }
}
