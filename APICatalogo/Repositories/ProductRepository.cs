using APICatalogo.Context;
using APICatalogo.Models;

namespace APICatalogo.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public IQueryable<Product> GetProducts()
    {
        return _context.Products;
    }
    public Product GetProduct(int id)
    {
        Product product = _context.Products.FirstOrDefault(p => p.ProductId == id);

        if(product is null)
        {
            throw new InvalidOperationException("Product is null");
        }

        return product;
     
    }
    public Product CreateProduct(Product product)
    {
        if (product is null)
        {
            throw new InvalidOperationException("Product is null");
            
        }
        _context.Products.Add(product);
        _context.SaveChanges();
        return product;
    }

    public bool UpdateProduct(Product product)
    {
        if (product is null)
        {
            throw new InvalidOperationException("Product is null");

        }
        if(_context.Products.Any(p => p.ProductId == product.ProductId))
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return true;
        }
       
        return false;
    }
    public bool DeleteProduct(int id)
    {
        var product = _context.Products.Find(id);
        if (product is not null)
        {
            _context.Products.Remove(product);
            _context.SaveChanges();
            return true;
        }

        return false;
    }

}
