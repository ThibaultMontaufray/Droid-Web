using System;

namespace Droid_web
{
    public static class GetText
    {
        public enum Language
        {
            FRENCH,
            ENGLISH
        }
        public static Language CurrentLanguage = (Language)Enum.Parse(typeof(Language), Properties.Settings.Default.language);
        public static string Text(string text)
        {
            switch (CurrentLanguage)
            {
                case Language.FRENCH:
                    return Properties.ResourceFrench.ResourceManager.GetString(text);
                case Language.ENGLISH:
                    return Properties.ResourceEnglish.ResourceManager.GetString(text);
                default:
                    return text;
            }
        }
    }
}
