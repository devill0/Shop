using System;
using System.Collections.Generic;
using System.Text;
using Shop.Core.DTO;
using Shop.Core.Repositories;
using Shop.Core.Domain;

namespace Shop.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public void Login(string email, string password)
        {
            var user = userRepository.Get(email);
            if(user == null)
            {
                throw new Exception($"User '{email}' was not found.");
            }
            if(user.Password != password)
            {
                throw new Exception("Invalid password.");
            }
        }

        public void Register(string email, string password, RoleDTO role)
        {
            var user = userRepository.Get(email);
            if (user != null)
            {
                throw new Exception($"User '{email}' already exists.");
            }
            var userRole = (Role)Enum.Parse(typeof(Role), role.ToString(), true);
            user = new User(email, password, userRole);
            userRepository.Add(user);
        }
    }
}
