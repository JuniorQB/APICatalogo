using APICatalogo.Models;
using APICatalogo.Pagination;

namespace APICatalogo.Repositories;

public interface IProductRepository : IRepository<Product>
{
    IEnumerable<Product> GetProducts(ProductsParameters parameters);
    IEnumerable<Product> GetProductsByCategory(int id);

}
