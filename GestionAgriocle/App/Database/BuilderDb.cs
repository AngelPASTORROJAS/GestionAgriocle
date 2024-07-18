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

        private void Build()
        {
            string ct = "CREATE TABLE ";
            string nn = "NOT NULL";
            string d = "DATE";
            string si = "SMALL INT";
            string v5 = "VARCHAR(5)";
            string v20 = "VARCHAR(20)";
            string pk = "PRIMARY KEY";
            string fk = "FOREIGN KEY";
            string r = "REFERENCES";
            string n = "NUMERIC";
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
                    $" unite {v20} {fk} {r} Unite(unite)" +
                ");",

                "Culture (" +
                    $" identifiant_culture {si} {pk} {nn}," +
                    $" date_debut {d}," +
                    $" dat_fin {d}," +
                    $" qt_recolte {n}," +
                    $" no_parcelle {si} {fk} {r} Parcelle(no_parcelle)," +
                    $" code_production {si} {fk} {r} Production(code_production)" +
                ");",

                "Element_Chimiques (" +
                    $" code_element {v5} {pk} {nn}," +
                    $" libelle_element {v20}," +
                    $" unite {v20} {fk} {r} Unite(unite)" +
                ");",

                "Engrais (" +
                    $" id_engrais {v20} {pk} {nn}," +
                    $" nom_engrais {v20}," +
                    $" unite {v20} {fk} {r} Unite(unite)" +
                ");",

                "Posseder (" +
                    $" valeur {v20}," +
                    $" id_engrais {v20} {fk} {r} Engrais(id_engrais)," +
                    $" code_element {v20} {fk} {r} Element_Chimiques(code_element)" +
                ");",

                "Epandre (" +
                    $" qte_epandue {n}," +
                    $" id_engrais {v20} {fk} {r} Engrais(id_engrais)," +
                    $" no_parcelle {si} {fk} {r} Parcelle(no_parcelle)" +
                ");"
            };

            foreach (string table in listTables)
            {
                Db.Query(ct +table);
            }
        }

        //private void DeleteTables() { }
    }
}