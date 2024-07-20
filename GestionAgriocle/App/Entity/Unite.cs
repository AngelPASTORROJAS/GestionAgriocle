namespace GestionAgriocle.App.Entity
{
    internal class Unite
    {
        private string _unite;

        public string unite { // TODO : demander à repenser la conception de base de données.
            get => _unite;
            set
            {
                if (string.IsNullOrEmpty(value) || value.Length > 20)
                {
                    throw new ArgumentException("unite doit comporter entre 1 et 20 caractères.");
                }
                _unite = value;
            }
        }
    }
}
