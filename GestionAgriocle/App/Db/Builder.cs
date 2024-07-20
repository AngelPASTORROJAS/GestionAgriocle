using MySql.Data.MySqlClient;

namespace GestionAgriocle.App.Db
{
    internal class Builder
    {
        private readonly MySqlConnection _connection;
        private MySqlConnection Connection { get => _connection; }

        public Builder(Database database)
        {
            _connection = database.Connection;
        }

        private void Query(string query)
        {
            try
            {
                Connection.Open();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Source + " : " + exception.Message);
                throw;
            }
            finally
            {
                Connection.Close();
            }
        }

        public void DropTables()
        {
            string[] tables =
            {
                "Unite",
                "Datation",
                "Parcelle",

                "Epandre",
                "Posseder",
                "Engrais",
                "Element_Chimique",
                "Culture",
                "Production"
            };

            foreach (string table in tables)
            {
                Query($"DROP TABLE {table};");
            }
        }

        public void Build()
        {
            //string ai = "AUTO_INCREMENT";
            string ct = "CREATE TABLE ";
            string nn = "NOT NULL";
            string d = "DATE";
            string si = "SMALLINT";
            string v5 = "VARCHAR(5)";
            string v20 = "VARCHAR(20)";
            string pk = "PRIMARY KEY";
            string fk = "FOREIGN KEY";
            string r = "REFERENCES";
            string n = "NUMERIC";
            string c = "CONSTRAINT"; // Sert à vérifier si la clé étrangère existe dans la table de réference
            string[] listTables =
            {
                "Parcelle (" +
                    $" no_parcelle {si} {pk} {nn}," +
                    $" surface {n}," +
                    $" nom_parcelle {v20}," +
                    $" coordonnees {v20}" +
                ");",

                $"Datation ( datation {d} {pk} {nn} );",

                $"Unite ( unite {v20} {pk} {nn} );",

                "Production (" +
                    $" code_production {si} {pk} {nn}," +
                    $" nom_production {v20}," +
                    $" unite {v20}," +
                    $" {c} FK_UniteProduction {fk} (unite) {r} Unite(unite)" +
                ");",

                "Culture (" +
                    $" identifiant_culture {si} {pk} {nn}," +
                    $" date_debut {d}," +
                    $" dat_fin {d}," +
                    $" qt_recolte {n}," +
                    $" no_parcelle {si}," +
                    $" code_production {si}," +
                    $" {c} FK_ProductionCulture {fk} (code_production) {r} Production(code_production)," +
                    $" {c} FK_ParcelleCulture {fk} (no_parcelle) {r} Parcelle(no_parcelle)" +
                ");",

                "Element_Chimique (" +
                    $" code_element {v5} {pk} {nn}," +
                    $" libelle_element {v20}," +
                    $" unite {v20}," +
                    $" {c} FK_UniteElement_Chimique {fk} (unite) {r} Unite(unite)" +
                ");",

                "Engrais (" +
                    $" id_engrais {v20} {pk} {nn}," +
                    $" nom_engrais {v20}," +
                    $" unite {v20}," +
                    $" {c} FK_UniteEngrais {fk} (unite) {r} Unite(unite)" +
                ");",

                "Posseder (" +
                    $" valeur {v20}," +
                    $" id_engrais {v20}," +
                    $" code_element {v20}," +
                    $" {c} FK_EngraisPosseder {fk} (id_engrais) {r} Engrais(id_engrais)," +
                    $" {c} FK_Element_ChimiquePosseder {fk} (code_element) {r} Element_Chimique(code_element)," +
                    $" {pk} (id_engrais, code_element)" +
                ");",

                "Epandre (" +
                    $" qte_epandue {n}," +
                    $" datation {d}," +
                    $" id_engrais {v20}," +
                    $" no_parcelle {si}," +
                    $" {c} FK_DatationEpandre {fk} (datation) {r} Datation(datation)," +
                    $" {c} FK_EngraisEpandre {fk} (id_engrais) {r} Engrais(id_engrais)," +
                    $" {c} FK_ParcelleEpandre {fk} (no_parcelle) {r} Parcelle(no_parcelle)," +
                    $" {pk} (id_engrais, no_parcelle)" +
                ");"
            };

            foreach (string table in listTables)
            {
                Query(ct +table);
            }
        }
    }
}