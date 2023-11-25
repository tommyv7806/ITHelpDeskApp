namespace ITHelpDeskApp.Models.Repository
{
    public interface IRepository<T> where T : class
    {
        List<T> GetAll();
        IEnumerable<T> List(QueryOptions<T> options);
        T? Get(int id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Save();
    }
}
