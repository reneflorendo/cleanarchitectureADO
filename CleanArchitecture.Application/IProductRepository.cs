using CleanArchitecture.Domain;

namespace CleanArchitecture.Application
{
    public interface IProductRepository
    {
        Task Add(Product product);
        Task Update(Product product);   
        Task Delete(int id);
        Task<Product> GetById(int id);
        Task<IEnumerable<Product>> GetProducts();

    }
}
