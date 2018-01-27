using System;
using Shop.Core.Domain;
using Microsoft.Extensions.Caching.Memory;

namespace Shop.Core.Services
{
    public class CartManager : ICartManager
    {
        private readonly IMemoryCache cache;

        public CartManager(IMemoryCache cache)
        {
            this.cache = cache;
        }

        public void Delete(Guid userId)
            => cache.Remove(GetCartKey(userId));

        public Cart Get(Guid userId)
            => cache.Get<Cart>(GetCartKey(userId));

        public void Set(Guid userId, Cart cart)
            => cache.Set(GetCartKey(userId), cart);

        private string GetCartKey(Guid userId) => $"{userId}:cart";
    }
}
