using System.Collections.Generic;

namespace Shop.Core.DTO
{
    public class CartDTO
    {
        public IEnumerable<CartItemDTO> Items { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
