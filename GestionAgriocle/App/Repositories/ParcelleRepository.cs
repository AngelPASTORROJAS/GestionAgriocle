using System.Runtime.Versioning;
using GestionAgriocle.App.Db;
using GestionAgriocle.App.Entity;
using GestionAgriocle.App.Exceptions;
using GestionAgriocle.App.Interfaces;
using GestionAgriocle.App.Utils;
using MySql.Data.MySqlClient;

namespace GestionAgriocle.App.Repositories
{
    internal class ParcelleRepository : IUpdatableRepository<Parcelle, int>
    {
        private readonly MySqlConnection _connection;

        public ParcelleRepository()
        {
            _connection = Database.GetDatabase().Connection;
        }

        public void Add(Parcelle entity)
        {
            using (MySqlCommand command = new MySqlCommand("INSERT INTO Parcelle (no_parcelle, surface, nom_parcelle, coordonnees) VALUES (@no_parcelle, @surface, @nom_parcelle, @coordonnees);", _connection))
            {
                command.Parameters.AddWithValue("@no_parcelle", entity.NoParcelle);
                command.Parameters.AddWithValue("@surface", entity.Surface);
                command.Parameters.AddWithValue("@nom_parcelle", entity.NomParcelle);
                command.Parameters.AddWithValue("@coordonnees", entity.Coordonnees);
                try
                {
                    _connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception exception)
                {
                    throw new RepositoryException($"Erreur lors de l'ajout de la parcelle.", nameof(ParcelleRepository), "Add", exception);
                }
                finally
                {
                    _connection.Close();
                }
            }
        }

        public void Delete(int id)
        {
            using (MySqlCommand command = new MySqlCommand("DELETE FROM Palette WHERE no_parcelle = @no_parcelle;", _connection))
            {
                try
                {
                    _connection.Open();
                    command.Parameters.AddWithValue("@no_parcelle", id);

                    if (command.ExecuteNonQuery() == 0)
                    {
                        throw new Exception("L'unité a supprimer n'as pas pu être trouver.");
                    }
                }
                catch (Exception exception)
                {
                    throw new RepositoryException($"Erreur lors de la suppression de la parcelle.", nameof(ParcelleRepository), "Delete", exception);
                }
                finally
                {
                    _connection.Close();
                }
            }
        }

        public Parcelle Get(int id)
        {
            Parcelle parcelle = null;
            using (MySqlCommand command = new MySqlCommand("SELECT no_parcelle, surface, nom_parcelle, coordonnees FROM Parcelle WHERE no_parcelle = @no_parcelle;", _connection))
            {
                try
                {
                    _connection.Open();
                    command.Parameters.AddWithValue("@no_parcelle", id);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            parcelle = new Parcelle
                            {
                                NoParcelle = reader.GetInt32("no_parcelle"),
                                Surface = reader.GetDecimal("surface"),
                                NomParcelle = reader.GetString("nom_parcelle"),
                                Coordonnees = reader.GetString("coordonnees")
                            };
                        }
                        reader.Close();
                    }
                    if (parcelle == null)
                    {
                        throw new Exception("Erreur la parcelle n'as pas été trouvé.");
                    }
                }
                catch (Exception exception)
                {
                    Logger log = Logger.GetInstance();
                    log.Log($"Erreur lors de la récuperation de la parcelle {id}.");
                    //throw new RepositoryException($"Erreur lors de la récuperation de la parcelle.", nameof(UniteRepository), "Get", exception);
                }
                finally
                {
                    _connection.Close();
                }
            }
            return parcelle;
        }

        public IEnumerable<Parcelle> GetAll()
        {
            List<Parcelle> parcelles = new List<Parcelle>();
            using (MySqlCommand command = new MySqlCommand("SELECT no_parcelle, surface, nom_parcelle, coordonnees FROM Parcelle;", _connection))
            {
                try
                {
                    _connection.Open();

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            parcelles.Add(new Parcelle
                            {
                                NoParcelle = reader.GetInt32("no_parcelle"),
                                Surface = reader.GetDecimal("surface"),
                                NomParcelle = reader.GetString("nom_parcelle"),
                                Coordonnees = reader.GetString("coordonnees")
                            });
                        }
                        reader.Close();
                        return parcelles;
                    }
                }
                catch (Exception exception)
                {
                    throw new RepositoryException($"Erreur lors de la récuperation des parcelles", nameof(UniteRepository), "GetAll", exception);
                }
                finally
                {
                    _connection.Close();
                }
            }
        }

        public void Update(Parcelle entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            if (entity.NoParcelle == null)
                throw new ArgumentNullException(nameof(entity));

            using (MySqlCommand command = new MySqlCommand($"UPDATE Parcelle SET no_parcelle = @NoParcelle, nom_parcelle = @NomParcelle, surface = @Surface, coordonnees = @Coordonnees WHERE no_parcelle=@NoParcelle);", _connection))
            {
                command.Parameters.AddWithValue("@NoParcelle", entity.NoParcelle);
                command.Parameters.AddWithValue("@NomParcelle", entity.NomParcelle);
                command.Parameters.AddWithValue("@Surface", entity.Surface);
                command.Parameters.AddWithValue("@Noordonnees", entity.Coordonnees);
                try
                {
                    _connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception exception)
                {
                    throw new RepositoryException($"Erreur lors de la mise à jour de la parcelle.", nameof(ParcelleRepository), "Update", exception);
                }
                finally
                {
                    _connection.Close();
                }
            }
        }

        public void UpdatePartial(Parcelle entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            if (entity.NoParcelle == null) 
                throw new ArgumentNullException(nameof(entity));
            
            List<string>columnsToUpdate = new List<string>();
            if (entity.NomParcelle == null) columnsToUpdate.Add("nom_parcelle = @NomParcelle");
            if (entity.Surface == null) columnsToUpdate.Add("surface = @Surface");
            if (entity.Coordonnees == null) columnsToUpdate.Add("coordonnees = @Coordonnees");

            using (MySqlCommand command = new MySqlCommand($"UPDATE Parcelle SET {string.Join(", ", columnsToUpdate)} WHERE no_parcelle=@NoParcelle);", _connection))
            {
                command.Parameters.AddWithValue("@NoParcelle", entity.NoParcelle);
                if (entity.NomParcelle == null)
                    command.Parameters.AddWithValue("@NomParcelle", entity.NomParcelle);
                if(entity.Surface == null)
                    command.Parameters.AddWithValue("@Surface", entity.Surface);
                if (entity.Coordonnees == null)
                    command.Parameters.AddWithValue("@Noordonnees", entity.Coordonnees);
                try
                {
                    _connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception exception)
                {
                    throw new RepositoryException($"Erreur lors de la mise à jour partiel de la parcelle.", nameof(ParcelleRepository), "UpdatePartial", exception);
                }
                finally
                {
                    _connection.Close();
                }
            }
        }
    }
}
