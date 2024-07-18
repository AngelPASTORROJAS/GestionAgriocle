namespace GestionAgriocle.App.Database
{
    internal class BuilderDb
    {
        private readonly Db _db;

        private Db Db {  get => _db; }

        public BuilderDb(Db database)
        {
            _db = database;
        }

        private void CreateTables()
        {
            string tableName;
            List<string> proprieties;
            Dictionary<string, List<string>> tables = new Dictionary<string, List<string>>();

            tableName = "Unite";
            proprieties = new List<string>()
            {
                "unite VARCHAR(50) PRIMARY KEY NOT NULL AUTO_INCREMENT"
            };

            tableName ="Date";
            proprieties = new List<string>
            {
                "date DATE PRIMARY KEY",
            };

            foreach (KeyValuePair<string, List<string>> table in tables)
            {
                QueryCreateTable(table.Key, table.Value);
            }

        }

        private void CreateMiddleTables()
        {
        }

        private string QueryCreateTable(string tableName, List<string> proprieties)
        {
            string query = $"CREATE TABLE {tableName} (";
            for (int i = 0; i < proprieties.Count()-1; i++)
            {
                query += proprieties[i] +"";
            }
            query += proprieties[proprieties.Count()-1]+");";
            return query;
        }

        //private void DeleteTables() { }
    }
}