namespace GestionAgriocle.App.Entity
{
    internal class Parcelle
    {
        private int? _noParcelle;
        private decimal? _surface;
        private string? _coordonnees;
        private string? _nomParcelle;

        public int? NoParcelle { get => _noParcelle; set => _noParcelle = value; }
        public decimal? Surface {  get => _surface; set => _surface = value; }
        public string? Coordonnees
        {
            get { return _coordonnees; } 
            set
            {
                if ( value?.Length > 20)
                {
                    throw new ArgumentException("coordonées doit comporter au maximum 20 caractères.");
                }
                _coordonnees = value;
            }
        }
        public string? NomParcelle
        { 
            get {  return _nomParcelle; } 
            set
            {
                if ( value?.Length > 20)
                {
                    throw new ArgumentException("le nom de la parcelle doit comporter au maximum 20 caractères.");
                }
                _nomParcelle = value;
            }
        }
    }
}
