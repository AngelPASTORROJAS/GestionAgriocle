using GestionAgriocle;
using GestionAgriocle.App.Db;
using GestionAgriocle.App.Repositories;
using GestionAgriocle.App.Utils;
using MySql.Data.MySqlClient;
using System.Net;
using System.Text;
using System.Text.Json;

/*
// TODO : Make a code documentation

// TODO : Use logs in excepetion :
//      log them more appropriately (for example, with a logging tool such as Serilog or NLog) or
//      return them to the caller for finer management.

// TODO: Use interface IDispose in Repository :
//      When the repository uses managed resources, such as a connection to a database or an Entity Framework context, it is important to release them correctly.
//      By implementing IDisposable, the repository can close the connection, release the context or any other dynamically allocated resource when it is no longer needed.

// TODO: Use interface IDispose in Service :
//      When the service holds a reference to a UserRepository instance which it must release.
//      This ensures that any resources used by the service are properly cleaned up when it is no longer needed.

// TODO : Build Test and more ...
*/

HttpListener listener = new HttpListener();
listener.Prefixes.Add("http://localhost:8080/");
listener.Start();
Console.WriteLine("Service web démaré sur http://localhost:8080/");

// Attendre les requêtes
while (true)
{
    HttpListenerContext context = listener.GetContext();
    // on prend la file d'attente de toutes les requêtes, pour chaque requête on applique la fonction RequestHandler
    ThreadPool.QueueUserWorkItem(o => RequestHandler(context));
}

// récupérer une requête static
static void RequestHandler(HttpListenerContext context)
{
    HttpListenerRequest request = context.Request;
    Console.WriteLine($"Requête reçu : {request.Url}");
    /*
    //  Créer la liste des objets à envoyées
    List<object> parcelles = new List<object>();

    // Récuperation des données
    Database database = Database.GetDatabase();
    try
    {
        database.Connection.Open();
        string sql = "SELECT * FROM Parcelle";
        MySqlCommand command = new MySqlCommand(sql, database.Connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            parcelles.Add(new
            {
                no_parcelle = reader["no_parcelle"],
                surface = reader["surface"],
                nom_parcelle = reader["nom_parcelle"],
                coordonnees = reader["coordonnees"]
            });
        }
        reader.Close();
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Source + " : " + ex.Message);
    }
    finally
    {
        database.Connection.Close();
    }*/
    object data = new
    {
        request1 = RessourceQuery.GetAllCultures(),
        request2 = RessourceQuery.GetAllCulturesByNoParcelle(10),
    };

    string jsonReponse = JsonSerializer.Serialize(data);

    // créer la réponse
    byte[] reponseBytes = Encoding.UTF8.GetBytes(jsonReponse);
    context.Response.ContentType = "application/json";
    context.Response.OutputStream.Write(reponseBytes, 0, reponseBytes.Length);
}