using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Pagination;
using System.Collections;

namespace APICatalogo.Repositories;

public class ProductRepository : Repository<Product>, IProductRepository
{
   

    public ProductRepository(AppDbContext context) :base(context)
    {
    }

    public IEnumerable<Product> GetProducts(ProductsParameters parameters)
    {
       return GetAll().OrderBy(p=>p.Name)
            .Skip((parameters.PageSize - 1) * parameters.PageSize)
            .Take(parameters.PageSize).ToList();

    }

    public IEnumerable<Product> GetProductsByCategory(int id)
    {
        return GetAll().Where(c => c.CategoryId == id);
    }

    
}
