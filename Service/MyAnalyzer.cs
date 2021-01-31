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

        /// <summary>
        /// Site submit process.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="host"></param>
        /// <returns></returns>
        public ViewMessage GetSiteResults(string url, string host)
        {
            var htmlWeb = new HtmlWeb();
            HtmlDocument htmlDocument = null;
            try
            {
                htmlDocument = htmlWeb.Load(url);
            }
            catch
            {
                return new ViewMessage()
                {
                    Message = "Fail to load website.",
                    StatusID = "1"
                };
            }

            // Meta tags word count
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
                        metaStringBuilder.AppendLine(tag.Attributes["content"].Value); // combine into a string
                    }
                }
            }

            viewMessage.MetaWordResults = base.CountWordResults(metaStringBuilder.ToString()); // word count and return result

            // External link
            var links = htmlDocument.DocumentNode.SelectNodes("//a");

            if (links != null)
            {
                string onlyHostName = string.Empty;

                if (!string.IsNullOrWhiteSpace(host))
                {
                    string[] hostSplit = null;
                    hostSplit = host.Split('.');
                    if (string.Compare(hostSplit[0], "www", true) != 0)
                    {
                        onlyHostName = hostSplit[0];
                    }
                    else
                    {
                        onlyHostName = hostSplit[1];
                    }
                }

                var exLinks = links.Where(attr => attr.Attributes["href"] != null &&
                                                attr.Attributes["href"].Value.Contains("http") &&
                                                !attr.Attributes["href"].Value.Contains(onlyHostName))
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
            viewMessage.StatusID = "0";
            return viewMessage;
        }

        /// <summary>
        /// Text submit process.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public ViewMessage GetWordResults(string text)
        {
            var viewMessages = new ViewMessage();
            viewMessages.StatusID = "0";
            viewMessages.WordResults = base.CountWordResults(text);

            return viewMessages;
        }
    }
}
