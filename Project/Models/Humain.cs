namespace Droid_web
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public delegate void HumainEventHandler(Humain p);
    public class Humain
    {
        #region Static Attribute
        #endregion

        #region Attribute / Properties
        public string Prenom { get; set; }
        public string Nom { get; set; }
        public string Genre { get; set; }
        public string Profession { get; set; }
        public string Nationnalite { get; set; }
        public string Naissance { get; set; }
        public string Deces { get; set; }
        public string Region { get; set; }
        public string PartisPolitique { get; set; }
        public string DebutActivite { get; set; }
        public string FinActivite { get; set; }
        public string Oeuvresprincipales { get; set; }
        public string LienPhoto { get; set; }
        public string Style { get; set; }
        public string NomDeNaissance { get; set; }
        public string EnrichmentDone { get; set; }
        #endregion

        #region Constructor
        public Humain()
        {
            EnrichmentDone = "false";
        }
        public Humain(string[] tab)
        {
            int index = 0;
            List<string> list = typeof(Humain).GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).Select(field => field.Name).ToList();
            foreach (string item in list)
            {
                if (tab.Length > index)
                {
                    if (item.StartsWith("_") || item.StartsWith("On"))
                    {
                    }
                    else
                    {
                        PropertyInfo propertyInfo = typeof(Humain).GetProperty(item.Split('>')[0].Split('<')[1]);
                        propertyInfo.SetValue(this, Convert.ChangeType(tab[index], propertyInfo.PropertyType), null);
                        index++;
                    }
                }
            }
        }
        #endregion        
    }
}
