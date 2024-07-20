using GestionAgriocle.IHM;

// TODO : Use logs in excepetion :
//      log them more appropriately (for example, with a logging tool such as Serilog or NLog) or
//      return them to the caller for finer management.

// TODO: Use interface IDispose in Repository :
//      When the repository uses managed resources, such as a connection to a database or an Entity Framework context, it is important to release them correctly.
//      By implementing IDisposable, the repository can close the connection, release the context or any other dynamically allocated resource when it is no longer needed.

// TODO: Use interface IDispose in Service :
//      When the service holds a reference to a UserRepository instance which it must release.
//      This ensures that any resources used by the service are properly cleaned up when it is no longer needed.

new Menu();