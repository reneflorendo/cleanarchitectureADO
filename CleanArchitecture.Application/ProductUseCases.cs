using CleanArchitecture.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application
{
    public class ProductUseCases
    {
        private readonly IProductRepository _productRepository;

        public ProductUseCases(IProductRepository productRepository)
        {
                _productRepository = productRepository;
        }

        public void CreateProduct(Product product) { 

          _productRepository.Add(product);
        }

        public void UpdateProduct(Product product) {
            _productRepository.Update(product);
        }

        public void DeleteProduct(int id) {
            _productRepository.Delete(id);
        }

        public async Task<Product> GetProduct(int id) {
            return await _productRepository.GetById(id);
        }

        public async  Task<IEnumerable<Product>> GetProducts() {
            return await _productRepository.GetProducts();
        }

    }
}
