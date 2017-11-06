using ShauliBlog.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShauliBlog.Controllers
{
    public class TranslateController : Controller
    {
        [HttpGet]
        public ActionResult Translate(String text)
        {
            Translator translator = new Translator();

            String translation = translator.Translate(text, "en");

            return Json(translation, JsonRequestBehavior.AllowGet);
        }
    }
}