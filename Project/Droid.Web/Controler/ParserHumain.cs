namespace Droid.Web
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Text.RegularExpressions;

    public static class ParserHumain
    {
        #region Attribute
        public static string WEBLINK = @"https://fr.wikipedia.org/w/index.php?title=Special%3ASearch&profile=default&search={0}";
        public static event HumainEventHandler OnEnrichmentCompleted;
        private static string _dumpWeb;
        private static bool _found;
        #endregion

        #region Methods public
        public static Humain Parse(string url)
        {
            _found = false;
            string dump = Web.GetPage(url);
            Humain humain = new Humain();
            if (Regex.Split(dump, "infobox_v").Length > 1)
            {
                string table = Regex.Split(dump, "infobox_v")[1];
                table = Regex.Split(table, "</table>")[0];
                GetNom(table, ref humain);
                GetNaissance(table, ref humain);
                GetDeces(table, ref humain);
                GetProfession(table, ref humain);
                GetPartisPolitique(table, ref humain);
                GetOeuvresprincipales(table, ref humain);
                GetNationalite(table, ref humain);
                GetLienPhoto(table, ref humain);
                GetGenre(table, ref humain);
                GetNomDeNaissance(table, ref humain);
                humain.EnrichmentDone = "true";
            }
            if (OnEnrichmentCompleted != null) { OnEnrichmentCompleted(humain); }
            return _found ? humain : null;
        }
        #endregion

        #region Methods private
        private static void GetNom(string table, ref Humain humain)
        {
            try
            {
                if (Regex.Split(table, "entete defaut").Length > 1)
                {
                    GetNomParsed(Regex.Split(table, "entete defaut")[1].Split('>')[1].Split('<')[0], ref humain);
                }
                if (string.IsNullOrEmpty(humain.Nom) && Regex.Split(table, "entete icon auteur").Length > 1)
                {
                    GetNomParsed(Regex.Split(Regex.Split(table, "entete icon auteur")[1], "<div>")[1].Split('<')[0], ref humain);
                }
                if (string.IsNullOrEmpty(humain.Nom) && Regex.Split(table, "entete").Length > 1)
                {
                    GetNomParsed(Regex.Split(Regex.Split(table, "entete")[1], "<div>")[1].Split('<')[0], ref humain);
                }
                if (string.IsNullOrEmpty(humain.Nom) && Regex.Split(table, "class=\"nowrap\"").Length > 1)
                {
                    GetNomParsed(Regex.Split(Regex.Split(table, "class=\"nowrap\"")[1], ">")[1].Split('<')[0], ref humain);
                }
                if (string.IsNullOrEmpty(humain.Nom)) _found = true;
            }
            catch (Exception exp)
            {
                Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + " : " + exp.Message);
            }
        }
        private static void GetNomParsed(string table, ref Humain humain)
        {
            if (table.Split(' ').Length > 1)
            {
                humain.Prenom = table.Split(' ')[0];
                for (int i = 1; i < table.Split(' ').Length; i++)
                {
                    humain.Nom += table.Split(' ')[i];
                }
                humain.Nom = humain.Nom.Trim();
                _found = true;
            }
        }
        private static void GetNaissance(string table, ref Humain humain)
        {
            try
            {
                if (Regex.Split(table.ToLower(), "date de naissance").Length > 1)
                {
                    _dumpWeb = Regex.Split(table.ToLower(), "date de naissance")[1];
                    if (_dumpWeb.Contains("datetime=\"")) humain.Naissance = Regex.Split(_dumpWeb, "datetime=\"")[1].Split('"')[0];
                    _found = true;
                }
                if (Regex.Split(table.ToLower(), ">naissance<").Length > 1)
                {
                    _dumpWeb = Regex.Split(table.ToLower(), ">naissance<")[1];
                    if (_dumpWeb.Contains("datetime=\"")) humain.Naissance = Regex.Split(_dumpWeb, "datetime=\"")[1].Split('"')[0];
                    _found = true;
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + " : " + exp.Message);
            }
        }
        private static void GetDeces(string table, ref Humain humain)
        {
            try
            {
                if (Regex.Split(table.ToLower(), "date de décès").Length > 1)
                {
                    _dumpWeb = Regex.Split(table.ToLower(), "date de décès")[1];
                    if (_dumpWeb.Contains("date de décès"))
                    {
                        humain.Deces = Regex.Split(Regex.Split(table.ToLower(), "date de décès")[1], "datetime=\"")[1].Split('"')[0];
                        _found = true;
                    }
                }
                if (Regex.Split(table.ToLower(), ">décès<").Length > 1)
                {
                    _dumpWeb = Regex.Split(table.ToLower(), ">décès<")[1];
                    if (_dumpWeb.Contains("datetime=\""))
                    {
                        humain.Deces = Regex.Split(_dumpWeb, "datetime=\"")[1].Split('"')[0];
                        _found = true;
                    }
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + " : " + exp.Message);
            }
        }
        private static void GetProfession(string table, ref Humain humain)
        {
            try
            {
                humain.Profession = string.Empty;
                if (Regex.Split(table.ToLower(), ">profession<").Length > 1)
                {
                    _dumpWeb = Regex.Split(Regex.Split(Regex.Split(table.ToLower(), ">profession<")[1], "<td>")[1], "</td>")[0];
                    foreach (var item in Regex.Split(_dumpWeb, "\">"))
                    {
                        if (!string.IsNullOrEmpty(item.Split('<')[0])) humain.Profession += string.Format("[{0}]", item.Split('<')[0]);
                        _found = true;
                    }
                }
                if (Regex.Split(table.ToLower(), ">activité principale<").Length > 1)
                {
                    _dumpWeb = Regex.Split(Regex.Split(Regex.Split(table.ToLower(), ">activité principale<")[1], "<td>")[1], "</td>")[0];
                    foreach (var item in Regex.Split(_dumpWeb, "\">"))
                    {
                        if (!string.IsNullOrEmpty(item.Split('<')[0]))
                        {

                            if (item.Contains(','))
                            {
                                foreach (var w in item.Split(','))
                                {
                                    if (!string.IsNullOrEmpty(w)) humain.Profession += string.Format("[{0}]", w.Split('<')[0].Trim());
                                }
                            }
                            else humain.Profession += string.Format("[{0}]", item.Split('<')[0]);
                            _found = true;
                        }
                    }
                }
                humain.Profession = humain.Profession.Replace("\r\n", string.Empty).Replace("\r", string.Empty).Replace("\n", string.Empty).Trim().Replace("[]", string.Empty).Replace("[ ]", string.Empty).Replace("[  ]", string.Empty).Replace("&nbsp;", string.Empty).Replace("&amp;", "&");
            }
            catch (Exception exp)
            {
                Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + " : " + exp.Message);
            }
        }
        private static void GetPartisPolitique(string table, ref Humain humain)
        {
            try
            {
                if (Regex.Split(table.ToLower(), ">parti politique<").Length > 1)
                {
                    foreach (var item in Regex.Split(Regex.Split(Regex.Split(table.ToLower(), ">parti politique<")[1], "<td>")[1], "</td>")[0].Split('<'))
                    {
                        if (item.Split('>').Length > 1 && !string.IsNullOrEmpty(HtmlRemoval.StripTagsRegex(item.Split('>')[1])) && !item.Split('>')[1].Contains('('))
                        {
                            humain.PartisPolitique = HtmlRemoval.StripTagsRegex(item.Split('>')[1]);
                            _found = true;
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + " : " + exp.Message);
            }
        }
        private static void GetOeuvresprincipales(string table, ref Humain humain)
        {
            try
            {
                humain.Oeuvresprincipales = string.Empty;
                if (Regex.Split(table.ToLower(), ">films notables<").Length > 1)
                {
                    _dumpWeb = Regex.Split(Regex.Split(Regex.Split(table.ToLower(), ">films notables<")[1], "<td>")[1], "</td>")[0];
                    foreach (var item in Regex.Split(_dumpWeb, "\">"))
                    {
                        if (!string.IsNullOrEmpty(item.Split('<')[0])) humain.Oeuvresprincipales += HtmlRemoval.StripTagsRegex(string.Format("[{0}]", item.Split('<')[0]));
                    }
                }
                if (Regex.Split(table, ">Œuvres principales<").Length > 1)
                {
                    _dumpWeb = Regex.Split(Regex.Split(Regex.Split(table, ">Œuvres principales<")[1], "<td>")[1], "</td>")[0];
                    foreach (var item in Regex.Split(_dumpWeb, "\">"))
                    {
                        if (!string.IsNullOrEmpty(item.Split('<')[0])) humain.Oeuvresprincipales += HtmlRemoval.StripTagsRegex(string.Format("[{0}]", item.Split('<')[0]));
                    }
                }
                humain.Oeuvresprincipales = HtmlRemoval.StripTagsRegex(humain.Oeuvresprincipales.Replace("\r\n", string.Empty).Replace("\r", string.Empty).Replace("\n", string.Empty).Trim().Replace("[]", string.Empty).Replace("[ ]", string.Empty).Replace("[  ]", string.Empty).Replace("&nbsp;", string.Empty).Replace("&amp;", "&"));
                if (!string.IsNullOrEmpty(humain.Oeuvresprincipales)) _found = true;
            }
            catch (Exception exp)
            {
                Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + " : " + exp.Message);
            }
        }
        private static void GetNationalite(string table, ref Humain humain)
        {
            try
            {
                humain.Nationnalite = string.Empty;
                if (Regex.Split(table.ToLower(), "nationalité").Length > 1)
                {
                    string prevItem = string.Empty;
                    _dumpWeb = Regex.Split(table.ToLower(), "nationalité")[1];
                    foreach (string item in _dumpWeb.Split('<'))
                    {
                        if (!string.IsNullOrEmpty(prevItem) && !prevItem.EndsWith(">"))
                        {
                            if (prevItem.Split('>').Length > 1)
                            {
                                humain.Nationnalite = HtmlRemoval.StripTagsRegex(prevItem.Split('>')[1]);
                                if (string.IsNullOrEmpty(humain.Nationnalite))
                                {
                                    if (item.Split('>').Length > 1)
                                    {
                                        humain.Nationnalite = HtmlRemoval.StripTagsRegex(item.Split('>')[1]);
                                        _found = true;
                                    }
                                }
                                break;
                            }
                        }
                        prevItem = item;
                        if (prevItem.Contains("\r") || prevItem.Contains("\n"))
                        {
                            prevItem = string.Empty;
                        }
                    }
                }
                humain.Nationnalite = humain.Nationnalite.Replace("&nbsp;", string.Empty).Replace("&amp;", "&");
            }
            catch (Exception exp)
            {
                Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + " : " + exp.Message);
            }
        }
        private static void GetLienPhoto(string table, ref Humain humain)
        {
            try
            {
                humain.LienPhoto = string.Empty;
                if (Regex.Split(table.ToLower(), "src=\"").Length > 1)
                {
                    humain.LienPhoto = "https:" + Regex.Split(table, "src=\"")[1].Split('\"')[0];
                    _found = true;
                }
                humain.LienPhoto = humain.LienPhoto.Replace("&nbsp;", string.Empty).Replace("&amp;", "&");
            }
            catch (Exception exp)
            {
                Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + " : " + exp.Message);
            }
        }
        private static void GetGenre(string table, ref Humain humain)
        {
            try
            {
                humain.Style = string.Empty;
                if (Regex.Split(table.ToLower(), ">genres<").Length > 1)
                {
                    humain.Style = HtmlRemoval.StripTagsRegex(Regex.Split(Regex.Split(table.ToLower(), ">genre<")[1], "<td>")[1].Split('<')[0]);
                    _found = true;
                }
                humain.Style = HtmlRemoval.StripTagsRegex(humain.Style.Replace("&nbsp;", string.Empty).Replace("&amp;", "&"));
            }
            catch (Exception exp)
            {
                Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + " : " + exp.Message);
            }
        }
        private static void GetNomDeNaissance(string table, ref Humain humain)
        {
            try
            {
                if (Regex.Split(table.ToLower(), ">nom de naissance<").Length > 1)
                {
                    humain.NomDeNaissance = Regex.Split(Regex.Split(table.ToLower(), ">nom de naissance<")[1], "<td>")[1].Split('<')[0];
                    humain.NomDeNaissance = humain.NomDeNaissance.Replace("&nbsp;", string.Empty).Replace("&amp;", "&");
                    _found = true;
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + " : " + exp.Message);
            }
        }
        #endregion
    }
}
