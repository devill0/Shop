﻿using Shop.Core.DTO;

namespace Shop.Core.Services
{
    public interface IUserService
    {
        UserDTO Get(string email);
        void Login(string email, string password);
        void Register(string email, string password, RoleDTO role);
    }
}
