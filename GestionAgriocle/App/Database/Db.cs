using MySql.Data.MySqlClient;

namespace GestionAgriocle.App.Database
{
    internal class Db
    {
        #region "Attributs"
        private MySqlConnection _connection;
        #endregion

        #region "Propriétés"
        public MySqlConnection Connection { get => _connection; }
        #endregion

        #region "Constructeur"
        public Db(string server, string uid, string password, string database)
        {
            InitConnection(server, uid, password, database);
        }
        #endregion

        #region "Méthodes"
        private void InitConnection(string server, string uid, string password, string database)
        {
            string connectionString = $"server={server};uid={uid};pwd={password};database={database}";
            _connection = new MySqlConnection(connectionString);
        }

        public void Query(string query)
        {
            try
            {
                Connection.Open();
                MySqlCommand cmd = new MySqlCommand(query, Connection);
                int i = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Source + " : " + ex.Message);
            }
            finally
            {
                Connection.Close();
            }
        }
        #endregion
    }
}
