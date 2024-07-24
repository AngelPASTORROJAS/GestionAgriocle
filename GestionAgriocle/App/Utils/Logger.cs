namespace GestionAgriocle.App.Utils
{
    internal sealed class Logger
    {
        // Variable privée statique qui contiendra l'instance unique de la classe
        private readonly static Logger instance = new Logger();

        // Chemin du fichier de log
        private readonly string logFilePath = "log.txt";

        // Constructeur privé pour empêcher la création d'instances en dehors de la classe
        private Logger()
        {
        }

        // Méthode publique statique qui retourne l'instance unique de la classe
        public static Logger GetInstance()
        {
            return instance;
        }

        // Méthode d'enregistrement d'un message dans le fichier de log
        public void Log(string message)
        {
            // Écrire le message dans le fichier de log
            File.AppendAllText(logFilePath, message + "\n");
        }
    }
}
