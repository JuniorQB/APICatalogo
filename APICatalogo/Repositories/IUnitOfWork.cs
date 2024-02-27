namespace APICatalogo.Repositories;

public interface IUnitOfWork
{
    IProductRepository ProductRepository { get; }
    ICategorieRepository CategorieRepository { get; }

    void Commit();
    
}
