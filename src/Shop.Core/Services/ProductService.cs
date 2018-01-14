using System;
using System.Collections.Generic;
using System.Text;
using Shop.Core.DTO;
using Shop.Core.Repositories;
using System.Linq;
using Shop.Core.Domain;

namespace Shop.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;

        public ProductService(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public ProductDTO Get(Guid id)
        {
            var product = productRepository.Get(id);

            return product == null ? null : new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Category = product.Category,
                Price = product.Price
            };
        }

        public IEnumerable<ProductDTO> GetAll()
            => productRepository.GetAll()
                                .Select(p => new ProductDTO
                                {   Id = p.Id,
                                    Name = p.Name,
                                    Category = p.Category,
                                    Price = p.Price
                                });

        public void Add(string name, string category, decimal price)
        {
            var product = new Product(name, category, price);
            productRepository.Add(product);
        }        
    }
}
