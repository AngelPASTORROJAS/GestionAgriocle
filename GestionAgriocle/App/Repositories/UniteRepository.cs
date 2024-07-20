using GestionAgriocle.App.Db;
using GestionAgriocle.App.Entity;
using GestionAgriocle.App.Interfaces;
using MySql.Data.MySqlClient;
using GestionAgriocle.App.Exceptions;

namespace GestionAgriocle.App.Repositories
{
    /// <summary>
    /// Responsible for data access, retrieval, backup, update and deletion of entities.
    /// Encapsulates data access logic, typically using an ORM(Object-Relational Mapping) as the Entity Framework.
    /// Provides an abstract interface for interacting with data, without exposing implementation details.
    /// </summary>
    internal class UniteRepository : IRepository<Unite, string>
    {
        private readonly Database _database;

        public UniteRepository()
        {
             _database = Database.GetDatabase();
        }

        public void Add(Unite entity)
        {
            using (MySqlCommand command = new MySqlCommand("INSERT INTO Unite (unite) VALUES (@unite);",_database.Connection))
            {
                command.Parameters.AddWithValue("@unite",entity.unite); // Set parameters
                try
                {
                    _database.Connection.Open();
                     command.ExecuteNonQuery();
                }
                catch (Exception exception)
                {
                    throw new RepositoryException($"Erreur lors de l'ajout de l'unité",nameof(UniteRepository),"Add",exception);
                }
                finally
                {
                    _database.Connection.Close();
                }
            }
        }

        public void Delete(string id)
        {
            using (MySqlCommand command = new MySqlCommand("DELETE FROM Unite WHERE unite = @unite;", _database.Connection))
            {
                try
                {
                    _database.Connection.Open();
                    command.Parameters.AddWithValue("@unite", id);

                    if (command.ExecuteNonQuery() == 0)
                    {
                        throw new Exception("L'unité a supprimer n'as pas pu être trouver.");
                    }
                }
                catch (Exception exception)
                {
                    throw new RepositoryException($"Erreur lors de la suppression de l'unité", nameof(UniteRepository), "Delete", exception);
                }
                finally
                {
                    _database.Connection.Close();
                }
            }
        }

        public Unite Get(string id)
        {
            Unite unite = null;
            using (MySqlCommand command = new MySqlCommand("SELECT unite FROM Unite WHERE unite = @unite;", _database.Connection))
            {
                try
                {
                    _database.Connection.Open();
                    command.Parameters.AddWithValue("@unite", id);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            unite = new Unite
                            {
                                unite = reader.GetString("unite")
                            };
                        }
                        reader.Close();
                    }
                    if (unite == null)
                    {
                        throw new Exception("Erreur l'unité n'as pas été trouvé");
                    }
                }
                catch (Exception exception)
                {
                    throw new RepositoryException($"Erreur lors de la récuperation des unités", nameof(UniteRepository), "GetAdd", exception);
                }
                finally
                {
                    _database.Connection.Close();
                }
            }
            return unite;
        }

        public IEnumerable<Unite> GetAll()
        {
            List<Unite> unites = new List<Unite>();
            using (MySqlCommand command= new MySqlCommand("SELECT unite FROM Unite;",_database.Connection))
            {
                try
                {
                    _database.Connection.Open();

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            unites.Add(new Unite
                            {
                               unite = reader.GetString("unite")
                            });
                        }
                        reader.Close();
                        return unites;
                    }
                }
                catch (Exception exception)
                {
                    throw new RepositoryException($"Erreur lors de la récuperation des unités",nameof(UniteRepository),"GetAdd",exception);
                }
                finally
                {
                    _database.Connection.Close();
                }
            }
        }
    }
}
