using System;
using System.Collections.Generic;
using Shop.Core.Domain;
using System.Linq;

namespace Shop.Core.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private static readonly ISet<Product> products = new HashSet<Product>
        {
            new Product("Laptop", "Electronics", 3000),
            new Product("Jeans", "Trousers", 150),
            new Product("Hammer", "Tools", 50),
            new Product("Ticket", "Cinema", 20)
        };

        public Product Get(Guid id)
            => products.SingleOrDefault(x => x.Id == id);

        public IEnumerable<Product> GetAll()
            => products;

        public void Add(Product product)
            => products.Add(product);          
    }
}
