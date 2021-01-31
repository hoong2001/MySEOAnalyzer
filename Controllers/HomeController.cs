using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModel;
using Service;

namespace MySEOAnalyer.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Analyze()
        {
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Analyze(string webURL, string isEnabledAnalyze)
        {
            ViewMessage viewMessage = null;

            if (string.IsNullOrWhiteSpace(isEnabledAnalyze))
            {
                Uri uriResult;
                if (Uri.TryCreate(webURL, UriKind.Absolute, out uriResult))
                {
                    // valid uri
                    var analyzer = new MyAnalyzer();

                }
                else
                {
                    // Text mode
                    var analyzer = new MyAnalyzer();
                    viewMessage = analyzer.GetWordResults(webURL);
                }
            }
            else
            {
                // SEO disabled
                return View("Index", new ViewMessage()
                {
                    StatusID ="1",
                    Message = "SEO is Disabled."
                });
            }

            return View(viewMessage);
        }

    }
}