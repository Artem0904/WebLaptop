using System.Linq.Expressions;

namespace DataAccess.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        //IEnumerable<TEntity> Get(
        //    Expression<Func<TEntity, bool>> filter = null,
        //    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        //    string includeProperties = "");
        TEntity GetById(object id);
        Task Insert(TEntity entity);
        Task Delete(object id);
        void Delete(TEntity entityToDelete);
        void Update(TEntity entityToUpdate);
        Task Save();

    }
}
