namespace Droid_web
{
    public class Carte
    {
        #region Attribute
        private double _longitude;
        private double _latitude;
        private string _frameCarte;
        private string _frameVue;
        private string _nom;
        private string _frameTerre;
        #endregion

        #region Properties
        public string Nom
        {
            get { return _nom; }
            set { _nom = value; }
        }
        public string FrameVue
        {
            get { return _frameVue; }
            set { _frameVue = value; }
        }
        public string FrameCarte
        {
            get { return _frameCarte; }
            set { _frameCarte = value; }
        }
        public double Latitude
        {
            get { return _latitude; }
            set { _latitude = value; }
        }
        public double Longitude
        {
            get { return _longitude; }
            set { _longitude = value; }
        }
        public string FrameTerre
        {
            get { return _frameTerre; }
            set { _frameTerre = value; }
        }
        #endregion

        #region Constructor
        public Carte()
        {

        }
        #endregion
    }
}
