using Shop.Core.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Core.Services
{
    public interface IProductService
    {
        ProductDTO Get(Guid id);
        IEnumerable<ProductDTO> GetAll();
        void Add(Guid id, string name, string category, decimal price);
        void Update(ProductDTO product);
        void Delete(Guid id);
    }
}
