using UPskillify_Forum.Models.Domain;

namespace UPskillify_Forum.Repositories;

// T -> where T: class
// this is a generic interface, T will be an entity 
// where T : class is a generic type constraint that restricts the type parameter T to reference types only. Class
public interface ICrudRepository<T> where T: class
{
    // here we are going to define the methods that each class that
    // implements this interface, needs to have
    
    // get all Ts
    Task<IEnumerable<T?>> GetAllAsync();
    
    // get one T
    Task<T?> GetAsync(Guid id);
    
    // create one T
    Task<T?> AddAsync(T entity);
    
    // update one T
    Task<T?> UpdateAsync(T entity);
    
    // remove one T
    // is going to return a bool if removed successfully or not
    Task<bool> DeleteAsync(Guid id);
}