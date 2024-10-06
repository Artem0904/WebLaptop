
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace DataAccess.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        [JsonIgnore]
        [NonSerialized]
        internal TelegramBotDbContext context;
        internal DbSet<TEntity> dbSet;

        public Repository(TelegramBotDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return dbSet.ToList();
        }

        public virtual TEntity GetById(object id)
        {
            return dbSet.Find(id);
        }

        public virtual async Task Insert(TEntity entity)
        {
            await dbSet.AddAsync(entity);
        }

        public virtual async Task Delete(object id)
        {
            TEntity entityToDelete = await dbSet.FindAsync(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public async Task Save()
        {
             await context.SaveChangesAsync();
        }
    }
}
