using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace ShauliBlog.Utils
{
    public class Translator
    {
        //private const string URL = "https://translate.google.com/?hl=fi&ie=UTF8&text=my+name+is+mike&langpair=fi";
        private const string URL = "https://translate.google.com/?hl={0}&ie=UTF8&text={1}&langpair={0}";

        public string translate(string origPhrase, string languageCode)
        {
            string result = string.Empty;

            string text = origPhrase.Replace(" ", "+");

            string url = String.Format(URL, languageCode, text);

            WebClient webClient = new WebClient();

            webClient.Encoding = System.Text.Encoding.UTF8;

            string rawResult = webClient.DownloadString(url);

            rawResult  = rawResult.Substring(result.IndexOf("<span title=\"") + "<span title=\"".Length);
            rawResult = rawResult.Substring(result.IndexOf(">") + 1);
            rawResult = rawResult.Substring(0, result.IndexOf("</span>"));

            result = rawResult.Trim();

            return result;
        }
    }
}