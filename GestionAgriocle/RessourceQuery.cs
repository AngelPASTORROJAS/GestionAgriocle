using System.Data.Common;
using GestionAgriocle.App.Db;
using GestionAgriocle.App.Entity;
using GestionAgriocle.App.Utils;
using MySql.Data.MySqlClient;

namespace GestionAgriocle
{
    internal class RessourceQuery
    {
        private static MySqlConnection _connection = Database.GetDatabase().Connection;
        /*

-- Trouver les cultures ayant une quantité récoltée supérieur à 1
SELECT identifiant_culture, date_debut, date_fin, qt_recoltee FROM Culture WHERE qt_recolte > 1;

-- Afficher les engrais utilisés à une date spécifique
SELECT id_engrais, nom_engrais FROM Engrais 
LEFT JOIN Epandre ON Epandre.id_engrais = Engrais.id_engrais
WHERE Epandre.datation = "yyyy-mm-dd";

-- Trouver la quantité totale d'engrais épandue pour chaque parcelle à date spécifique
SELECT no_parcelle, SUM(qte_epandue) FROM Epandre WHERE date = "yyyy-mm-dd" GROUP BY no_parcelle;

-- Lister toutes les productions avec leur unité
SELECT nom_production, unite FROM production;

-- Trouver la parcelle ayant la plus grande surface
SELECT nom_parcelle, MAX(surface) FROM parcelle;

-- Trouver les productions dont la quantité récoltée totale est supérieur à 1
SELECT nom_production FROM Production INNER JOIN Culture ON Production.code_production = Culture.code_production GROUP BY code_production HAVING SUM(qte_recoltee) > 1 
        */

        /// <summary>
        /// 
        /// </summary>
        /// <returns>
        /// Renvoie la liste de toutes les cultures
        /// </returns>
        public static List<object> GetAllCultures()
        {
            List<object> cultures = new List<object>();
            using (MySqlCommand command = new MySqlCommand("SELECT * FROM Culture;", _connection))
            {
                try
                {
                    _connection.Open();

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            cultures.Add(new
                            {
                                identifiant_culture = reader.GetInt16("identifiant_culture"),
                                date_debut = reader.GetDateTime("date_debut"),
                                date_fin = reader.GetDateTime("date_fin"),
                                qt_recolte = reader.GetDecimal("qt_recolte"),
                                no_parcelle = reader.GetInt16("no_parcelle"),
                                code_production = reader.GetInt16("code_production")
                            });
                        }
                        reader.Close();
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine($"{exception.Source} : {exception.Message}");
                }
                finally
                {
                    _connection.Close();
                }
            }
            return cultures;
        }

        /// <summary>
        /// Trouver les cultures d'une parcelle spécifique
        /// </summary>
        /// <returns>
        /// Renvoie la liste des cultures 
        /// </returns>
        public static List<object> GetAllCulturesByNoParcelle(int no_parcelle)
        {
            List<object> cultures = new List<object>();
            string query = "SELECT Parcelle.no_parcelle, Culture.* FROM Culture INNER JOIN Parcelle ON Culture.no_parcelle = Parcelle.no_parcelle WHERE Parcelle.no_parcelle = @no_parcelle;";
            using (MySqlCommand command = new MySqlCommand(query, _connection))
            {
                try
                {
                    _connection.Open();
                    command.Parameters.AddWithValue("@no_parcelle", no_parcelle);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            cultures.Add(new
                            {
                                identifiant_culture = reader.GetInt16("identifiant_culture"),
                                date_debut = reader.GetDateTime("date_debut"),
                                date_fin = reader.GetDateTime("date_fin"),
                                qt_recolte = reader.GetDecimal("qt_recolte"),
                                no_parcelle = reader.GetInt16("no_parcelle"),
                                code_production = reader.GetInt16("code_production")
                            });
                        }
                        reader.Close();
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine($"{exception.Source} : {exception.Message}");
                }
                finally
                {
                    _connection.Close();
                }
            }
            return cultures;
        }
    }
}
