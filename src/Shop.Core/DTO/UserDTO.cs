using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Core.DTO
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public RoleDTO Role { get; set; }
    }
}
