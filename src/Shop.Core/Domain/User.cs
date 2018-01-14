using System;

namespace Shop.Core.Domain
{
    public class User
    {
        public Guid Id { get; }
        public string Email { get; }
        public string Password { get; }
        public Role Role { get; }

        public User(string email, string password, Role role = Role.User)
        {
            Id = Guid.NewGuid();
            Email = email.ToLowerInvariant();
            Password = password;
            Role = role;
        }
    }
}
