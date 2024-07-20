namespace GestionAgriocle.App.Interfaces
{
    internal interface IUpdatableRepository<T, Tkey> : IRepository<T, Tkey>
    {
        void Update(T entity);
    }
}
