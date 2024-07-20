namespace GestionAgriocle.App.Interfaces
{
    internal interface IRepository<T, Tkey>
    {
        T Get(Tkey id);
        IEnumerable<T> GetAll();
        void Add(T entity);
        void Delete(Tkey id);
    }
}
