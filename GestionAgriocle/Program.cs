using GestionAgriocle.App.Database;


string server = "127.0.0.1";
string uid = "root";
string password = "";
string database = "challenge";
Db db = new Db(server,uid,password,database);
BuilderDb builderDb = new BuilderDb(db);
builderDb.Build();
//builderDb.DropTables();