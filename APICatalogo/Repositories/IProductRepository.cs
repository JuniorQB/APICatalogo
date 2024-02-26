using APICatalogo.Models;

namespace APICatalogo.Repositories;

public interface IProductRepository
{

    IQueryable<Product> GetProducts();

    Product GetProduct(int id);

    Product CreateProduct(Product product);

    bool UpdateProduct(Product product);

    bool DeleteProduct(int id);

}
