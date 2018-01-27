﻿using System.Collections.Generic;
using System.Linq;

namespace Shop.Web.Models
{
    public class CartViewModel
    {
        public IList<CartItemViewModel> Items { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
