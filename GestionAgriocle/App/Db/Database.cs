using MySql.Data.MySqlClient;

namespace GestionAgriocle.App.Db
{
    /// <summary>
    /// The Database class can be made responsible for managing the database connection, using the MySql.Data.MySqlClient library.
    /// This isolates the details of database connection and access from the rest of the application, promoting decoupling.
    /// </summary>
    internal sealed class Database
    {
        // Static members are 'eagerly initialized', that is, 
        // immediately when class is loaded for the first time.
        // .NET guarantees thread safety for static initialization
        private static readonly Database instance = new Database();

        private readonly string _server = "127.0.0.1";
        private readonly string _uid = "root";
        private readonly string _password = "";
        private readonly string _database = "challenge";

        private readonly MySqlConnection _connection;

        public MySqlConnection Connection { get => _connection; }

        private Database()
        {
            string connectionString = $"server={_server};uid={_uid};pwd={_password};database={_database}";
            _connection = new MySqlConnection(connectionString);
        }

        public static Database GetDatabase()
        {
            return instance;
        }
    }
}
