using Microsoft.EntityFrameworkCore;

namespace ITHelpDeskApp.Models.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected HelpDeskContext context { get; set; }
        private DbSet<T> dbset { get; set; }
        public Repository(HelpDeskContext ctx)
        {
            context = ctx;
            dbset = context.Set<T>();
        }

        public virtual List<T> GetAll()
        {
            return dbset.ToList();
        }
        public virtual IEnumerable<T> List(QueryOptions<T> options)
        {
            IQueryable<T> query = dbset;

            foreach (string include in options.GetIncludes())
            {
                query = query.Include(include);
            }

            if (options.HasWhere)
                query = query.Where(options.Where);
            if (options.HasOrderBy)
                query = query.OrderBy(options.OrderBy);
            return query.ToList();
        }
        public virtual T? Get(int id) => dbset.Find(id);
        public virtual void Insert(T entity) => dbset.Add(entity);
        public virtual void Update(T entity) => dbset.Update(entity);
        public virtual void Delete(T entity) => dbset.Remove(entity);
        public virtual void Save() => context.SaveChanges();
    }
}
