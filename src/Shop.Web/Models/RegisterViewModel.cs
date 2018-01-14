using Microsoft.AspNetCore.Mvc.Rendering;
using Shop.Core.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Web.Models
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password))]
        [DataType(DataType.Password)]
        public string PasswordConfirmation { get; set; }

        [Required]
        public RoleDTO Role { get; set; }

        public List<SelectListItem> Roles { get; } = Enum.GetValues(typeof(RoleDTO))
            .Cast<RoleDTO>()
            .Select(r => new SelectListItem
            {
                Text = r.ToString(),
                Value = r.ToString()
            }).ToList();
    }
}
