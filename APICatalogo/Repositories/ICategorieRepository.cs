using APICatalogo.Models;

namespace APICatalogo.Repositories;

public interface ICategorieRepository
{
    IEnumerable<Category> GetCategories();
    Category GetCategory(int id);

    Category CreateCategory(Category category);
    Category UpdateCategory(Category category);
    Category DeleteCategory(int id);

}
