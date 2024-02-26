using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Repositories;

public class CategoryRepository : ICategorieRepository
{
    private readonly AppDbContext _context;

    public CategoryRepository(AppDbContext context)
    {
        _context = context;
    }
    public IEnumerable<Category> GetCategories()
    {
        return _context.Categories.ToList();
    }

    public Category GetCategory(int id)
    {
        return _context.Categories.FirstOrDefault(c => c.CategoryId == id);
    }


    public Category CreateCategory(Category category)
    {
        if (category is null)
        {
            throw new ArgumentNullException(nameof(category));
        }
        _context.Categories.Add(category);
        _context.SaveChanges();
        return category;
    }


    public Category UpdateCategory(Category category)
    {
        if (category is null)
        {
            throw new ArgumentNullException(nameof(category));
        }
        _context.Categories.Entry(category).State = EntityState.Modified;
        _context.SaveChanges();
        return category;
    }

    public Category DeleteCategory(int id)
    {
       var category = _context.Categories.Find(id);
        if (category is null)
        {
            throw new ArgumentNullException(nameof(category));
        }

        _context.Categories.Remove(category);
        _context.SaveChanges();
        return category;

    }

}
