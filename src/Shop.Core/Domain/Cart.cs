using System;
using System.Collections.Generic;
using System.Linq;

namespace Shop.Core.Domain
{
    public class Cart
    {
        private ISet<CartItem> items = new HashSet<CartItem>();
        public IEnumerable<CartItem> Items => items;
        public decimal TotalPrice => Items.Sum(i => i.TotalPrice);

        public void AddProduct(Product product)
        {
            var item = items.SingleOrDefault(x => x.ProductId == product.Id);
            if (item == null)
            {
                item = new CartItem(product);
                items.Add(item);

                return;
            }
            item.IncreaseQuantity();
        }

        public void DeleteProduct(Guid productId)
        {
            var item = items.SingleOrDefault(x => x.ProductId == productId);

            if(item == null)
            {
                throw new Exception($"Product with id: '{productId}' was not found in cart.");
            }
            if(item.Quantity == 1)
            {
                items.Remove(item);

                return;
            }
            item.DecreaseQuantity();
        }

        public void Clear() => items.Clear();
    }
}
