using System.Collections.Generic;

namespace FlexiCart.Service.Interfaces
{

    /// <summary>
    /// Add Generic repository model to Abstract Database functionality and simplify Generic code for Repository represing Collections
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity> where TEntity : class
    {

        TEntity Get(int id);
        IEnumerable<TEntity> GetAll();

        void Add(TEntity entity);

        void AddRange(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);



    }
}
