﻿using System;
using System.Collections.Generic;
using Shop.Core.DTO;

namespace Shop.Core.Services
{
    public class OrderService : IOrderService
    {
        public IEnumerable<OrderDTO> Browse(Guid userId)
        {
            throw new NotImplementedException();
        }

        public void Create(Guid userId)
        {
            throw new NotImplementedException();
        }

        public OrderDTO Get(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
