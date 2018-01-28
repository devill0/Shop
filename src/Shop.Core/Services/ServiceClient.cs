using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shop.Core.DTO;
using System.Net.Http;
using System.Linq;
using Newtonsoft.Json;
using Microsoft.Extensions.Options;
using Shop.Core.Options;
using System.Text;

namespace Shop.Core.Services
{
    public class ServiceClient : IServiceClient
    {
        private readonly HttpClient httpClient;

        public ServiceClient(IOptions<ServiceClientOptions> options)
        {
            httpClient = new HttpClient();
            //httpClient.BaseAddress = new Uri("http://localhost:3747");
            httpClient.BaseAddress = new Uri(options.Value.Url);
            httpClient.DefaultRequestHeaders.Remove("Accept");
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }
       
        public async Task<IEnumerable<ProductDTO>> GetProductAsync()
        {
            var response = await httpClient.GetAsync("http://localhost:3747/products");

            if (!response.IsSuccessStatusCode)
            {
                return Enumerable.Empty<ProductDTO>();
            }
            var content = await response.Content.ReadAsStringAsync();
            var products = JsonConvert.DeserializeObject<IEnumerable<ProductDTO>>(content);

            return products;
        }

        public async Task AddProductAsync(string name, string category, decimal price)
        {
            var product = new ProductDTO
            {
                Name = name,
                Category = category,
                Price = price
            };
            var payload = JsonConvert.SerializeObject(product);
            var content = new StringContent(payload, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("products", content);
            if (response.IsSuccessStatusCode)
            {
                return;
            }
            throw new Exception($"Could not create a product. {response.ReasonPhrase}");
        }
    }
}
