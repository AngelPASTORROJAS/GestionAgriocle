namespace GestionAgriocle.App.Exceptions
{
    /// <summary>
    /// This exception can be used to capture and pass on information specific to errors occurring in Repository classes.
    /// It can be used to differentiate database access errors from other types of errors that may occur in the application.
    /// </summary>
    internal class RepositoryException : ApplicationException
    {
        public string RepositoryName { get; }
        public string OperationType { get; }
        public RepositoryException(string message, string repositoryName, string operationType, Exception innerException) : base(message, innerException)
        {
            RepositoryName = repositoryName;
            OperationType = operationType;
        }
    }
}
