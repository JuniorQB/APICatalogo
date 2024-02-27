using APICatalogo.Models;

namespace APICatalogo.Repositories;

public interface ICategorieRepository  : IRepository<Category>
{   
    IEnumerable<Category> GetCategoriesWithProducts();
 
}
