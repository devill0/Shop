using System;
using System.Collections.Generic;
using System.Text;
using Shop.Core.Domain;
using System.Linq;

namespace Shop.Core.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly static ISet<User> users = new HashSet<User>
        {
            new User("user@shop.com", "secret"),
            new User("admin@shop.com", "secret", role: Role.Admin)
        };

        public void Add(User user)
            => users.Add(user);

        public User Get(string email)
            => users.SingleOrDefault(x => string.Equals(x.Email, email, StringComparison.InvariantCultureIgnoreCase));

        public User Get(Guid id)
            => users.SingleOrDefault(x => x.Id == id);
    }
}
