using GestionAgriocle.App.Entity;
using GestionAgriocle.App.Repositories;

namespace GestionAgriocle.IHM
{
    internal class Menu
    {
        public Menu()
        {
            UniteRepository repository = new UniteRepository();

            foreach (Unite unite in repository.GetAll())
            {
                Console.WriteLine(unite.unite);
            }
            Console.WriteLine();

            Unite unite1 = new Unite { unite= "P" };
            repository.Add(unite1);
            Console.WriteLine(repository.Get("P"));
            Console.WriteLine();
        }
    }
}
