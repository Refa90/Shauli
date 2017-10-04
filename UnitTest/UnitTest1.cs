using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Diagnostics;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //string url = String.Format("http://www.google.com/translate_t?hl=en&ie=UTF8&text={0}&langpair={1}", "mi+nombre", "en");
            string url = "https://translate.google.com/?hl=fi&ie=UTF8&text=my+name+is+mike&langpair=fi";
            WebClient webClient = new WebClient();
            webClient.Encoding = System.Text.Encoding.UTF8;
            string result = webClient.DownloadString(url);
            Debug.WriteLine(result);
            result = result.Substring(result.IndexOf("<span title=\"") + "<span title=\"".Length);
            result = result.Substring(result.IndexOf(">") + 1);
            result = result.Substring(0, result.IndexOf("</span>"));
            Debug.WriteLine(result.Trim());
        }
    }
}
