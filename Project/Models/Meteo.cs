namespace Droid_web
{
    public delegate void MeteoEventHandler(Meteo m);
    public class Meteo
    {
        #region Attribute
        private string _ville;
        private string _temperature;
        private string _humidite;
        private string _vent;
        private string _precipitaion;
        private string _description;
        private string _image;
        private string _frameTerre;
        #endregion

        #region Properties
        public string Precipitation
        {
            get { return _precipitaion; }
            set { _precipitaion = value; }
        }
        public string Vent
        {
            get { return _vent; }
            set { _vent = value; }
        }
        public string Humidite
        {
            get { return _humidite; }
            set { _humidite = value; }
        }
        public string Temperature
        {
            get { return _temperature; }
            set { _temperature = value; }
        }
        public string Ville
        {
            get { return _ville; }
            set { _ville = value; }
        }
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        public string Image
        {
            get { return _image; }
            set { _image = value; }
        }
        public string Titre
        {
            get
            {
                return Ville + " : " + Description;
            }
        }
        public string FrameTerre
        {
            get { return _frameTerre; }
            set { _frameTerre = value; }
        }
        #endregion

        #region Constructor
        public Meteo()
        {

        }
        #endregion
    }
}
