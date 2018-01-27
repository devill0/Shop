using System;
using System.Collections.Generic;

namespace Shop.Core.DTO
{
    public class OrderDTO
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public IEnumerable<OrderItemDTO> Items { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
