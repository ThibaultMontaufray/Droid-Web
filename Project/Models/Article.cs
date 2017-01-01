namespace Droid_web
{
    using System;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;

    public class Article
    {
        #region Attribute
        private string _titre;
        private string _imgPath;
        private string _cat;
        private string _source;
        private string _date;
        private string _text;
        private string _dump;
        private string _lien;
        #endregion

        #region Properties
        public string Dump
        {
            get { return _dump; }
            set { _dump = value; }
        }
        public string Date
        {
            get { return _date; }
            set { _date = value; }
        }
        public string Source
        {
            get { return _source; }
            set { _source = value; }
        }
        public string Categorie
        {
            get { return _cat; }
            set { _cat = value; }
        }
        public string Titre
        {
            get { return _titre; }
            set { _titre = value; }
        }
        public string Texte
        {
            get { return _text; }
            set { _text = value; }
        }
        public string ImagePath
        {
            get { return _imgPath; }
            set { _imgPath = value; }
        }
        public string Lien
        {
            get { return _lien; }
            set { _lien = value; }
        }
        #endregion

        #region Constructor
        public Article()
        {
        }
        #endregion

        #region Methods public
        #endregion

        #region Methods private
        #endregion
    }
}
