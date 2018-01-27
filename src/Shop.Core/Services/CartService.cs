using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Shop.Core.Domain;
using Shop.Core.DTO;
using Shop.Core.Extensions;
using Shop.Core.Repositories;
using System;

namespace Shop.Core.Services
{
    public class CartService : ICartService
    {
        private readonly IUserRepository userRepository;
        private readonly IProductRepository productRepository;
        private readonly ICartManager cartManager;
        private readonly IMapper mapper;

        public CartService(IUserRepository userRepository,
            IProductRepository productRepository,
            ICartManager cartManager,
            IMapper mapper)
        {
            this.userRepository = userRepository;
            this.productRepository = productRepository;
            this.cartManager = cartManager;
            this.mapper = mapper;
        }

        public CartDTO Get(Guid userId)
        {
            var cart = cartManager.Get(userId);

            return cart == null ? null : mapper.Map<CartDTO>(cart);
        }

        public void AddProduct(Guid userId, Guid productId)
            => ExecuteOnCart(userId, cart =>
            {
                var product = productRepository.Get(productId)
                    .FailIfNull($"Product with id: '{productId}' was not found.");
                cart.AddProduct(product);
            });

        public void DeleteProduct(Guid userId, Guid productId)
            => ExecuteOnCart(userId, cart => cart.DeleteProduct(productId));

        public void Clear(Guid userId)
            => ExecuteOnCart(userId, cart => cart.Clear());

        public void Create(Guid userId)
        {
            cartManager.Get(userId).FailIfExists($"Cart already exists for user with id: '{userId}'.");
            cartManager.Set(userId, new Cart());
        }

        public void Delete(Guid userId)
        {
            GetCartOrFail(userId);
            cartManager.Delete(userId);
        }

        private void ExecuteOnCart(Guid userId, Action<Cart> action)
        {
            var cart = GetCartOrFail(userId);
            action(cart);
            cartManager.Set(userId, cart);
        }

        private Cart GetCartOrFail(Guid userId)
            => cartManager.Get(userId).FailIfNull($"Cart was not found for user with id: '{userId}'.");
    }
}
