using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StopWord;
using HtmlAgilityPack;
using ViewModel;
using TextDiscovery;

namespace Service
{
    public class MyAnalyzer : AnalyzerBase
    {

        public ViewMessage GetSiteResults(string url)
        {
            var htmlWeb = new HtmlWeb();
            HtmlDocument htmlDocument = null;
            try
            {
                 htmlDocument = htmlWeb.Load(url);
                // htmlDocument = htmlWeb.Load("http://www.ecosadmin.com.kk/");
            }
            catch
            {
                return new ViewMessage()
                {
                    Message = "Fail to load website.",
                    StatusID = "2"
                };
            }

            ViewMessage viewMessage = new ViewMessage();
            var metaTags = htmlDocument.DocumentNode.SelectNodes("//meta");

            string metaContentCombine = string.Empty;
            StringBuilder metaStringBuilder = new StringBuilder();

            if (metaTags != null)
            {
                foreach (var tag in metaTags)
                {
                    if (tag.Attributes["name"] != null && tag.Attributes["content"] != null)
                    {
                        metaStringBuilder.AppendLine(tag.Attributes["content"].Value);
                    }
                }
            }

            viewMessage.MetaWordResults =  base.CountWordResults(metaStringBuilder.ToString());

            var links = htmlDocument.DocumentNode.SelectNodes("//a");

            if (links != null)
            {
                var exLinks = links.Where(attr => attr.Attributes["href"] != null && attr.Attributes["href"].Value.Contains("http"))
                    .Select(selector => new
                    {
                        Text = selector.Attributes["href"]
                    }).Distinct()
                    .ToList();

                foreach (var exlink in exLinks)
                {
                    // do something
                }


            }
            return viewMessage;
        }

        public ViewMessage GetWordResults(string text)
        {
            var viewMessages = new ViewMessage();
            viewMessages.StatusID = "0";
            viewMessages.WordResults = base.CountWordResults(text);

            return viewMessages;
        }
    }
}
