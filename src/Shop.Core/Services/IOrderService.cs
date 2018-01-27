using Shop.Core.DTO;
using System;
using System.Collections.Generic;

namespace Shop.Core.Services
{
    public interface IOrderService
    {
        void Create(Guid userId);
        OrderDTO Get(Guid id);
        IEnumerable<OrderDTO> Browse(Guid userId);
    }
}
