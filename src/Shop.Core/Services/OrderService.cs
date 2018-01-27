using System;
using System.Collections.Generic;
using Shop.Core.DTO;
using Shop.Core.Repositories;
using AutoMapper;
using System.Linq;
using Shop.Core.Extensions;
using Shop.Core.Domain;

namespace Shop.Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository orderRepository;
        private readonly IUserRepository userRepository;
        private readonly ICartManager cartManager;
        private readonly IMapper mapper;

        public OrderService(IOrderRepository orderRepository, 
            IUserRepository userRepository,
            ICartManager cartManager,
            IMapper mapper)
        {
            this.orderRepository = orderRepository;
            this.userRepository = userRepository;
            this.cartManager = cartManager;
            this.mapper = mapper;
        }               

        public void Create(Guid userId)
        {
            var user = userRepository.Get(userId)
                .FailIfNull($"User with id: '{userId}' was not found.");
            var cart = cartManager.Get(userId)
                .FailIfNull($"Cart was not found for user with id: '{userId}'.");
            var order = new Order(user, cart);
            orderRepository.Add(order);
            cart.Clear();
            cartManager.Set(userId, cart);
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
