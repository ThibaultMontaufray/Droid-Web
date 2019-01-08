namespace Droid.Web
{
    using System;
    using System.Collections.Generic;

    public class Recette
    {
        #region Attribute
        private int _assiettes;
        private string _titre;
        private string _description;
        private string _boisson;
        private TimeSpan _preparation;
        private TimeSpan _cuisson;
        private List<Ingredient> _ingredient;
        private string _imagePath;
        private int _difficulte;
        #endregion

        #region Properties
        public int Assiettes
        {
            get { return _assiettes; }
            set { _assiettes = value; }
        }
        public string Boisson
        {
            get { return _boisson; }
            set { _boisson = value; }
        }
        public TimeSpan Preparation
        {
            get { return _preparation; }
            set { _preparation = value; }
        }
        public TimeSpan Cuisson
        {
            get { return _cuisson; }
            set { _cuisson = value; }
        }
        public string Titre
        {
            get { return _titre; }
            set { _titre = value; }
        }
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        public List<Ingredient> Ingredients
        {
            get { return _ingredient; }
            set { _ingredient = value; }
        }
        public string ImagePath
        {
            get { return _imagePath; }
            set { _imagePath = value; }
        }
        public int Difficulte
        {
            get { return _difficulte; }
            set { _difficulte = value; }
        }
        #endregion

        #region Constructor
        public Recette()
        {
            _ingredient = new List<Ingredient>();
        }
        #endregion
    }
}
