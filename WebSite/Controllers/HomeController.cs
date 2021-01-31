using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModel;
using Service;

namespace MySEOAnalyzer.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(new ViewMessage()
            {
                StatusID = "0",
                Message = ""
            });
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
                if (!string.IsNullOrWhiteSpace(webURL))
                {
                    Uri uriResult;
                    if (Uri.TryCreate(webURL, UriKind.Absolute, out uriResult))
                    {
                        var analyzer = new MyAnalyzer();
                        viewMessage = analyzer.GetSiteResults(url: webURL, host: uriResult.Host);
                        if (string.Compare(viewMessage.StatusID, "0") != 0)
                        {
                            return View("Index", viewMessage);

                        }
                        return View("AnalyzeSite", viewMessage);

                    }
                    else
                    {
                        // Text mode
                        var analyzer = new MyAnalyzer();
                        viewMessage = analyzer.GetWordResults(webURL);
                        return View("AnalyzeText", viewMessage);
                    }
                }
                // SEO disabled
                return View("Index", new ViewMessage()
                {
                    StatusID = "1",
                    Message = "Please submit text or URL."
                });

            }
            else
            {
                // SEO disabled
                return View("Index", new ViewMessage()
                {
                    StatusID = "1",
                    Message = "SEO is Disabled."
                });
            }

        }

    }
}