using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Repositories;

public class CategoryRepository : Repository<Category>,ICategorieRepository
{
    public CategoryRepository(AppDbContext context) : base(context)
    {
        
    }
    public IEnumerable<Category> GetCategoriesWithProducts()
    {
        return _context.Categories.Include(p => p.Products).ToList(); 
    }

   
   
}
