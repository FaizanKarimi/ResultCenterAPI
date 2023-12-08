namespace betway_result_center_api.Helpers
{
    public static class CommonHelper
    {
        public static string _GetUserLanguage(string userLanguage)
        {
            string language = string.Empty;
            switch (userLanguage)
            {
                case "de":  //Germany
                    language = "1";
                    break;
                case "da":  //Danish
                    language = "2";
                    break;
                case "sv":  //Swedish
                    language = "3";
                    break;
                case "nb":  //Norwegian
                    language = "4";
                    break;
                case "it":  //Italian
                    language = "5";
                    break;
                case "es":  //Spanish
                    language = "6";
                    break;
                case "fr":  //French
                    language = "7";
                    break;
                default:    //English
                    language = "0";
                    break;
            }
            return language;
        }

        public static string _GetUserLanguageFile(string userLanguage)
        {
            string language = string.Empty;
            switch (userLanguage)
            {
                case "de":  //Germany
                    language = "de-ger";
                    break;
                case "da":  //Danish
                    language = "en-us";
                    break;
                case "sv":  //Swedish
                    language = "sv-sw";
                    break;
                case "nor":  //Norwegian
                    language = "en-us";
                    break;
                case "it":  //Italian
                    language = "it-it";
                    break;
                case "es":  //Spanish
                    language = "es-spa";
                    break;
                case "fr":  //French
                    language = "en-us";
                    break;
                default:    //English
                    language = "en-us";
                    break;
            }
            return language;
        }
    }
}