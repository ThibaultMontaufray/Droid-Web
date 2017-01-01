namespace Droid_web
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;

    public class ParserRecette
    {
        #region Attribute
        public static string WEBLINKRAND = @"http://www.marmiton.org/recettes/recette-hasard.aspx";
        public static string WEBLINK = @"http://www.marmiton.org/recettes/recherche.aspx?aqt={0}";

        public static List<string> IngredientsCommuns;
        private static List<string> _mesures = new List<string>() { "mg", "g", "gr", "kg", "ml", "cl", "dl", "l", "milligramme", "gramme", "kilogramme", "kilo", "millilitre",
                "centilitre", "litre", "milligrammse", "grammes", "kilogrammes", "millilitres", "centilitres", "litres", "decilitre", "decilitres", "mm", "cm", "m", "dm" };
        private static List<string> _contenant = new List<string>() { "c à c", "c à s", "c a c", "c a s", "cuillères", "cuillère", "cuillères à café", "cuillères à soupe",
                "cuillère à café", "cuillère à soupe", "cuiller à café", "cuiller à soupe", "cuillerée à café", "cuillerée à soupe", "cuillerées à café",
                "cuillerées à soupe", "cuillers à café", "cuillers à soupe", "c à café", "c à soupe", "c. à café", "c. à soupe", "poignée", "grosse poignée",
                "petite poignée", "poignées", "grosses poignées", "petites poignées", "verre", "boite", "boîte", "boites", "boîtes", "sachet", "sachets",
                "c-à-soupe", "c-à-café", "gousse", "racine", "tranche", "gousses", "racines", "tranches", "petites tranches", "belles tranches", "tranches fines",
                "fines tranches", "pincée", "pincées", "feuille", "feuilles", "brin", "brins", "paquet", "paquets", "pot", "pots", "tasse", "tasses", "botte",
                "bottes", "grandes feuilles", "petites feuilles", "cuillère à café", "cs", "cc", "bouchon", "bouchons", "bol", "bols", "cube", "cubes", "petit verre",
                "grand verre", "soupçon", "soupçons", "zeste", "zestes", "bouquet", "bouquets", "branche", "branches" };
        private static List<string> _liaison = new List<string>() { "a", "à", "de", "d'" };
        private static bool _found;
        #endregion

        #region Methods public
        public static Recette Parse(string url)
        {
            string webDumpResearch = Web.GetPage(url);
            string[] dump = Regex.Split(webDumpResearch, "ctl00_cphMainContent_m_ctrlSearchEngine_m_ctrlSearchListDisplay_rptResultSearch_ctl00_m_linkTitle");
            if (dump.Length > 1)
            {
                url = "http://www.marmiton.org/" + Regex.Split(dump[1], "href=\"")[1].Split('"')[0];
            }
            else
            {
                return null;
            }

            _found = false;
            string webDump = Web.GetPage(url);
            Recette rec = new Recette();
            GetTitre(webDump, ref rec);
            GetDescription(webDump, ref rec);
            GetPreparation(webDump, ref rec);
            GetCuisson(webDump, ref rec);
            GetBoisson(webDump, ref rec);
            GetNombre(webDump, ref rec);
            GetIngredients(webDump, ref rec);
            GetImage(webDump, ref rec);
            return _found ? rec : null;
        }
        #endregion

        #region Methods private
        private static void GetTitre(string table, ref Recette recette)
        {
            try
            {
                if (Regex.Split(table.ToLower(), "<span class=\"fn\">").Length > 1)
                {
                    recette.Titre = HtmlRemoval.StripTagsRegex(Regex.Split(table.ToLower(), "<span class=\"fn\">")[1].Split('<')[0]);
                    _found = true;
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + " : " + exp.Message);
            }
        }
        private static void GetBoisson(string table, ref Recette recette)
        {
            try
            {
                if (Regex.Split(table.ToLower(), "<h4>boisson conseillée :</h4>\r\n<p>").Length > 1)
                {
                    recette.Boisson = HtmlRemoval.StripTagsRegex(Regex.Split(table.ToLower(), "<h4>boisson conseillée :</h4>\r\n<p>")[1].Split('<')[0]);
                    _found = true;
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + " : " + exp.Message);
            }
        }
        private static void GetDescription(string table, ref Recette recette)
        {
            try
            {
                if (Regex.Split(table, "Préparation de la recette :").Length > 1)
                {
                    recette.Description = HtmlRemoval.StripTagsRegex(Regex.Split(Regex.Split(Regex.Split(Regex.Split(table, "Préparation de la recette :")[1], "Remarques :")[0], "Boisson conseillée :")[0], "af_utils")[0]);
                    _found = true;
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + " : " + exp.Message);
            }
        }
        private static void GetPreparation(string table, ref Recette recette)
        {
            try
            {
                if (Regex.Split(table.ToLower(), "<span class=\"preptime\">").Length > 1)
                {
                    recette.Preparation = new TimeSpan(0, int.Parse(Regex.Split(table.ToLower(), "<span class=\"preptime\">")[1].Split('<')[0]), 0);
                    _found = true;
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + " : " + exp.Message);
            }
        }
        private static void GetCuisson(string table, ref Recette recette)
        {
            try
            {
                if (Regex.Split(table.ToLower(), "<span class=\"cooktime\">").Length > 1)
                {
                    recette.Cuisson = new TimeSpan(0, int.Parse(Regex.Split(table.ToLower(), "<span class=\"cooktime\">")[1].Split('<')[0]), 0);
                    _found = true;
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + " : " + exp.Message);
            }
        }
        private static void GetNombre(string table, ref Recette recette)
        {
            try
            {
                if (Regex.Split(table.ToLower(), @"<span>ingrédients ").Length > 1)
                {
                    if (Regex.Split(Regex.Split(table.ToLower(), @"<span>ingrédients ")[1], "pour ").Length > 1)
                    {
                        recette.Assiettes = int.Parse(Regex.Split(Regex.Split(table.ToLower(), @"<span>ingrédients ")[1], "pour ")[1].Split(' ')[0]);
                        _found = true;
                    }
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + " : " + exp.Message);
            }
        }
        private static void GetImage(string table, ref Recette recette)
        {
            //photo m_pinitimage
            try
            {
                if (Regex.Split(table, @"http://images.marmitoncdn.org/recipephotos").Length > 1)
                {
                    if (Regex.Split(Regex.Split(table, @"http://images.marmitoncdn.org/recipephotos")[1], "\"").Length > 1)
                    {
                        recette.ImagePath = @"http://images.marmitoncdn.org/recipephotos" + Regex.Split(Regex.Split(table, @"http://images.marmitoncdn.org/recipephotos")[1], ".jpg")[0] + ".jpg";
                        _found = true;
                    }
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + " : " + exp.Message);
            }
        }

        private static void GetIngredients(string table, ref Recette recette)
        {
//            CultureInfo.CurrentCulture = CultureInfo.CreateSpecificCulture("fr-FR");
            string cleanDump;
            string cleanDumpAlpha;
            string cleanDumpBeta;
            Ingredient ingredient;
            try
            {
                GetIngredientInitConst();
                if (Regex.Split(table.ToLower(), "m_content_recette_ingredients").Length > 1)
                {
                    foreach (string dumpIngredientAlpha in Regex.Split(Regex.Split(table.ToLower(), "m_content_recette_ingredients")[1], "- "))
                    {
                        cleanDumpAlpha = HtmlRemoval.StripTagsRegex(Regex.Split(Regex.Split(dumpIngredientAlpha, "<br>")[0], "Préparation de la recette :")[0]);
                        if (string.IsNullOrEmpty(cleanDumpAlpha) || cleanDumpAlpha.Contains("data-content") || cleanDumpAlpha.Contains("=") || cleanDumpAlpha.Length > 500) continue;
                        foreach (string dumpIngredientBeta in cleanDumpAlpha.Split('+'))
                        {
                            cleanDumpBeta = HtmlRemoval.StripTagsRegex(Regex.Split(Regex.Split(dumpIngredientBeta, "<br>")[0], "Préparation de la recette :")[0]);
                            if (string.IsNullOrEmpty(cleanDumpBeta) || cleanDumpBeta.Contains("data-content") || cleanDumpBeta.Contains("=") || cleanDumpBeta.Length > 500) continue;
                            foreach (string dumpIngredient in Regex.Split(cleanDumpBeta, " et "))
                            {
                                cleanDump = HtmlRemoval.StripTagsRegex(Regex.Split(Regex.Split(dumpIngredient, "<br>")[0], "Préparation de la recette :")[0]);
                                if (string.IsNullOrEmpty(cleanDump) || cleanDump.Contains("data-content") || cleanDump.Contains("-->") || cleanDump.Contains("=") || cleanDump.Length > 500) continue;

                                ingredient = new Ingredient();

                                GetIngredientsCleanStupidsWords(ref cleanDump);
                                GetIngredientCleanLiaison(ref cleanDump);
                                GetIngredientSplitNumAlpha(ref cleanDump);
                                GetIngredientAmountMedium(ref cleanDump);
                                GetIngredientAmount(ref cleanDump, ref ingredient);
                                GetIngredientsCleanStupidsWords(ref cleanDump);
                                GetIngredientCleanLiaison(ref cleanDump);
                                GetIngredientUnit(ref cleanDump, ref ingredient);
                                cleanDump = cleanDump.Split('(')[0].Trim();
                                GetIngredientCleanLiaison(ref cleanDump);
                                ingredient.Name = cleanDump;

                                if (ingredient.Quantity == 0 && ingredient.Unit == null && ingredient.Name.Split(' ').Length > 5)
                                {
                                    Console.WriteLine("That's again a stupid parsing from human that never respect rules ...");
                                }
                                else
                                {
                                    recette.Ingredients.Add(ingredient);
                                }
                                _found = true;
                            }
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + " : " + exp.Message);
            }
        }
        private static void GetIngredientInitConst()
        {
            _contenant = _contenant.OrderByDescending(x => x.Length).ToList();
            _mesures = _mesures.OrderByDescending(x => x.Length).ToList();
            _liaison = _liaison.OrderByDescending(x => x.Length).ToList();
        }
        private static void GetIngredientAmount(ref string cleanDump, ref Ingredient ingredient)
        {
            double count;
            if (double.TryParse(cleanDump.Split(' ')[0].ToLower().Replace(".", ",").Replace("une", "1").Replace("un", "1").Replace("un demi", "0,5").Replace("moitiée", "0,5").Replace("un quart", "0,25").Replace("1/2", "0,5").Replace("1/4", "0,25").Replace("1/8", "0,125"), out count))
            {
                ingredient.Quantity = count;
                cleanDump = cleanDump.Replace(cleanDump.Split(' ')[0], string.Empty).Trim();
            }
        }
        private static void GetIngredientUnit(ref string cleanDump, ref Ingredient ingredient)
        {
            foreach (string item in _contenant)
            {
                if (cleanDump.StartsWith(string.Format("{0} ", item)))
                {
                    ingredient.Unit = item;
                    cleanDump = cleanDump.Substring(item.Length + 1, cleanDump.Length - (item.Length + 1)).Trim();
                    GetIngredientCleanLiaison(ref cleanDump);
                }
            }

            if (string.IsNullOrEmpty(ingredient.Unit))
            {
                try
                {
                    if (cleanDump.Split(' ').Length > 1 && _mesures.Contains(cleanDump.Split(' ')[0]))
                    {
                        ingredient.Unit = cleanDump.Split(' ')[0];
                        if (_liaison.Contains(cleanDump.Split(' ')[1])) cleanDump = cleanDump.Substring(cleanDump.Split(' ')[1].Length + 1, cleanDump.Length - (cleanDump.Split(' ')[1].Length + 1)).Trim();
                        cleanDump = cleanDump.Substring(ingredient.Unit.Length, cleanDump.Length - ingredient.Unit.Length).Trim();
                    }
                    else if (cleanDump.Split(' ').Length > 2 && _mesures.Contains(cleanDump.Split(' ')[1]))
                    {
                        ingredient.Unit = cleanDump.Split(' ')[1];
                        if (_liaison.Contains(cleanDump.Split(' ')[2])) cleanDump = cleanDump.Substring(cleanDump.Split(' ')[2].Length + 1, cleanDump.Length - (cleanDump.Split(' ')[2].Length + 1)).Trim();
                        cleanDump = cleanDump.Substring(ingredient.Unit.Length, cleanDump.Length - ingredient.Unit.Length).Trim();
                    }
                    else
                    {
                        Console.WriteLine("oups : " + cleanDump);
                    }
                }
                catch (Exception exp)
                {
                    Console.WriteLine(exp.Message);
                }
            }
        }
        private static void GetIngredientSplitNumAlpha(ref string cleanDump)
        {
            string tmp = string.Empty;
            Regex rexNumChar = new Regex("[0-9]+[a-zA-Z-]");
            Regex rexNum = new Regex("[0-9]+");
            Regex rexChar = new Regex("[a-zA-Z-]+");
            foreach (string word in cleanDump.Split(' '))
            {
                if (rexNumChar.IsMatch(word))
                {
                    tmp += Regex.Matches(word, "[0-9]+")[0] + " " + Regex.Matches(word, "[a-zA-Z-]+")[0] + " ";
                }
                else
                {
                    tmp += word + " ";
                }
            }
            cleanDump = tmp;
        }
        private static void GetIngredientAmountMedium(ref string cleanDump)
        {
            double doubleVal1 = 0;
            double doubleVal2 = 0;
            if (double.TryParse(cleanDump.Split(' ')[0], out doubleVal1) && double.TryParse(cleanDump.Split(' ')[2], out doubleVal2) && (cleanDump.Split(' ')[1].Equals("à") || cleanDump.Split(' ')[1].Equals("ou") || cleanDump.Split(' ')[1].Equals("a")))
            {
                cleanDump = cleanDump.Replace(cleanDump.Split(' ')[0] + " " + cleanDump.Split(' ')[1] + " " + cleanDump.Split(' ')[2], ((doubleVal1 + doubleVal2) / 2).ToString()).Trim();
            }
        }
        private static void GetIngredientCleanLiaison(ref string cleanDump)
        {
            cleanDump = cleanDump.Trim();
            if (cleanDump.StartsWith("a ")) { cleanDump = cleanDump.Substring(2, cleanDump.Length - 2).Trim(); GetIngredientCleanLiaison(ref cleanDump); }
            if (cleanDump.StartsWith("d'")) { cleanDump = cleanDump.Substring(2, cleanDump.Length - 2).Trim(); GetIngredientCleanLiaison(ref cleanDump); }
            if (cleanDump.StartsWith("l'")) { cleanDump = cleanDump.Substring(2, cleanDump.Length - 2).Trim(); GetIngredientCleanLiaison(ref cleanDump); }
            if (cleanDump.StartsWith("à ")) { cleanDump = cleanDump.Substring(2, cleanDump.Length - 2).Trim(); GetIngredientCleanLiaison(ref cleanDump); }
            if (cleanDump.StartsWith("de ")) { cleanDump = cleanDump.Substring(3, cleanDump.Length - 3).Trim(); GetIngredientCleanLiaison(ref cleanDump); }
            if (cleanDump.StartsWith("du ")) { cleanDump = cleanDump.Substring(3, cleanDump.Length - 3).Trim(); GetIngredientCleanLiaison(ref cleanDump); }
            if (cleanDump.StartsWith("le ")) { cleanDump = cleanDump.Substring(3, cleanDump.Length - 3).Trim(); GetIngredientCleanLiaison(ref cleanDump); }
            if (cleanDump.StartsWith("la ")) { cleanDump = cleanDump.Substring(3, cleanDump.Length - 3).Trim(); GetIngredientCleanLiaison(ref cleanDump); }
            if (cleanDump.StartsWith("des ")) { cleanDump = cleanDump.Substring(4, cleanDump.Length - 4).Trim(); GetIngredientCleanLiaison(ref cleanDump); }
            if (cleanDump.StartsWith("les ")) { cleanDump = cleanDump.Substring(4, cleanDump.Length - 4).Trim(); GetIngredientCleanLiaison(ref cleanDump); }
            if (cleanDump.StartsWith("de la ")) { cleanDump = cleanDump.Substring(6, cleanDump.Length - 6).Trim(); GetIngredientCleanLiaison(ref cleanDump); }
        }
        private static void GetIngredientsCleanStupidsWords(ref string cleanDump)
        {
            if (cleanDump.ToLower().StartsWith("environ ")) cleanDump = cleanDump.ToLower().Replace("environ ", string.Empty).Trim();
            if (cleanDump.ToLower().StartsWith("quelque ")) cleanDump = cleanDump.ToLower().Replace("quelque ", string.Empty).Trim();
            if (cleanDump.ToLower().StartsWith("quelques ")) cleanDump = cleanDump.ToLower().Replace("quelques ", string.Empty).Trim();
            if (cleanDump.ToLower().StartsWith("grosse ")) cleanDump = cleanDump.ToLower().Replace("grosse ", string.Empty).Trim();
            if (cleanDump.ToLower().StartsWith("grosses ")) cleanDump = cleanDump.ToLower().Replace("grosses ", string.Empty).Trim();
            if (cleanDump.ToLower().StartsWith("gros ")) cleanDump = cleanDump.ToLower().Replace("gros ", string.Empty).Trim();
            if (cleanDump.ToLower().StartsWith("petites ")) cleanDump = cleanDump.ToLower().Replace("petites ", string.Empty).Trim();
            if (cleanDump.ToLower().StartsWith("petite ")) cleanDump = cleanDump.ToLower().Replace("petite ", string.Empty).Trim();
            if (cleanDump.ToLower().StartsWith("petits ")) cleanDump = cleanDump.ToLower().Replace("petits ", string.Empty).Trim();
            if (cleanDump.ToLower().StartsWith("petit ")) cleanDump = cleanDump.ToLower().Replace("petit ", string.Empty).Trim();
            if (cleanDump.ToLower().StartsWith("belles ")) cleanDump = cleanDump.ToLower().Replace("belles ", string.Empty).Trim();
            if (cleanDump.ToLower().StartsWith("belle ")) cleanDump = cleanDump.ToLower().Replace("belle ", string.Empty).Trim();
            if (cleanDump.ToLower().StartsWith("un peu ")) cleanDump = cleanDump.ToLower().Replace("un peu ", string.Empty).Trim();
            if (cleanDump.ToLower().StartsWith("facultatif :")) cleanDump = cleanDump.ToLower().Replace("facultatif :", string.Empty).Trim();
            if (cleanDump.ToLower().StartsWith("en accompagnement :")) cleanDump = cleanDump.ToLower().Replace("en accompagnement :", string.Empty).Trim();
        }
        #endregion
    }
}
