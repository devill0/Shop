using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Shop.Core.Domain;
using Shop.Core.DTO;
using Shop.Core.Repositories;
using System;

namespace Shop.Core.Services
{
    public class CartService : ICartService
    {
        private readonly IUserRepository userRepository;
        private readonly IProductRepository productRepository;
        private readonly IMemoryCache cache;
        private readonly IMapper mapper;

        public CartService(IUserRepository userRepository,
            IProductRepository productRepository,
            IMemoryCache cache,
            IMapper mapper)
        {
            this.userRepository = userRepository;
            this.productRepository = productRepository;
            this.cache = cache;
            this.mapper = mapper;
        }

        public CartDTO Get(Guid userId)
        {
            var cart = GetCart(userId);

            return cart == null ? null : mapper.Map<CartDTO>(cart);
        }

        public void AddProduct(Guid userId, Guid productId)
            => ExecuteOnCart(userId, cart =>
            {
                var product = productRepository.Get(productId);
                if (product == null)
                {
                    throw new Exception($"Product with id: {productId} was not found.");
                }
                cart.AddProduct(product);
            });    

        public void DeleteProduct(Guid userId, Guid productId)
            => ExecuteOnCart(userId, cart => cart.DeleteProduct(productId));

        public void Clear(Guid userId)
            => ExecuteOnCart(userId, cart => cart.Clear());
       
        public void Create(Guid userId)
        {
            var cart = GetCart(userId);
            if(cart != null)
            {
                throw new Exception($"Cart already exists for user with id: '{userId}'.");
            }
            SetCart(userId, new Cart());
        }

        public void Delete(Guid userId)
        {
            GetCartOrFail(userId);
            cache.Remove(GetCartKey(userId));
        }

        private void ExecuteOnCart(Guid userId, Action<Cart> action)
        {
            var cart = GetCartOrFail(userId);
            action(cart);
            SetCart(userId, cart);
        }

        private Cart GetCartOrFail(Guid userId)
        {
            var cart = GetCart(userId);
            if (cart == null)
            {
                throw new Exception($"Cart was not found for user with id: '{userId}'.");
            }

            return cart;
        }

        private Cart GetCart(Guid userId) => cache.Get<Cart>(GetCartKey(userId));

        private Cart SetCart(Guid userId, Cart cart) => cache.Set(GetCartKey(userId), cart);

        private string GetCartKey(Guid userId) => $"{userId}:cart";
    }
}
