using System;
using System.Collections.Generic;
using Shop.Core.DTO;
using Shop.Core.Repositories;
using AutoMapper;
using System.Linq;

namespace Shop.Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository orderRepository;
        private readonly IUserRepository userRepository;
       
        private readonly IMapper mapper;

        public OrderService(IOrderRepository orderRepository, IMapper mapper)
        {
            this.orderRepository = orderRepository;
            this.mapper = mapper;
        }               

        public void Create(Guid userId)
        {
            var user = userRepository.Get(userId)
                                     

        }

        public OrderDTO Get(Guid id)
        {
            var order = orderRepository.Get(id);

            return order == null ? null : mapper.Map<OrderDTO>(order);
        }
        public IEnumerable<OrderDTO> Browse(Guid userId)
            => orderRepository.Browse(userId)
                              .Select(x => mapper.Map<OrderDTO>(x));
    }
}
