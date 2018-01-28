using System;
using System.Collections.Generic;
using System.Text;
using Shop.Core.DTO;
using Shop.Core.Repositories;
using System.Linq;
using Shop.Core.Domain;
using AutoMapper;

namespace Shop.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
        }

        public ProductDTO Get(Guid id)
        {
            var product = productRepository.Get(id);

            return product == null ? null : mapper.Map<ProductDTO>(product);
        }

        public IEnumerable<ProductDTO> GetAll()
            => productRepository.GetAll()
                                .Select(p => mapper.Map<ProductDTO>(p));

        public void Add(Guid id, string name, string category, decimal price)
        {
            var product = new Product(id, name, category, price);
            productRepository.Add(product);
        }

        public void Update(ProductDTO product)
        {
            var existingProduct = productRepository.Get(product.Id);
            if(existingProduct == null)
            {
                throw new Exception($"Product was not found, id: '{product.Id}'");
            }
            existingProduct.SetName(product.Name);
            existingProduct.SetCategory(product.Category);
            existingProduct.SetPrice(product.Price);
            productRepository.Update(existingProduct);
        }
    }
}
