using APICatalogo.Context;

namespace APICatalogo.Repositories;

public class UnitOfWork : IUnitOfWork

{
    private IProductRepository? _productRepository;

    private ICategorieRepository? _categorieRepository;

    public AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public ICategorieRepository CategorieRepository
    {
        get
        {
            return _categorieRepository = _categorieRepository ?? new CategoryRepository(_context);
        }
    }

    public IProductRepository ProductRepository 
    {
        get {
            return _productRepository = _productRepository ?? new ProductRepository(_context);
        }
    }

    public void Commit()
    {
        _context.SaveChanges();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
