using Microsoft.AspNetCore.Mvc.Rendering;
using Shop.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Web.Models
{
    public class AddOrUpdateProductViewModel : ProductViewModel
    {
        public List<SelectListItem> Categories { get; } = new List<SelectListItem>
        {
            new SelectListItem { Text = "Electronics", Value = "Electronics"},
            new SelectListItem { Text = "Tools", Value = "Tools"},
            new SelectListItem { Text = "Trousers", Value = "Trousers"},
            new SelectListItem { Text = "Cinema", Value = "Cinema"}
        };

        public AddOrUpdateProductViewModel()
        {
        }

        public AddOrUpdateProductViewModel(ProductDTO dto) : base(dto)
        {
        }
    }
}
