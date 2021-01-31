using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TextDiscovery;
using System.Linq;
using System.Collections.Generic;
using ViewModel;
using Service;
using HtmlAgilityPack;

namespace UnitTestSeo
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            List<Result> results = new List<Result>();
            var textSlice = TextSlicer.Default;

            var mySle = textSlice.CreateSlices(source: "a b c d a b c d . & **")
                .Where(x => x.IsToken);
            var distinctWords = mySle.Select(selector => new { Text = selector.Text }).Distinct();
            foreach (var item in distinctWords)
            {
                int count = mySle.Where(x => string.Compare(x.Text, item.Text, true) == 0).Count();
            }

        }

        [TestMethod]
        public void TextSeriveTest()
        {
            string text = @"The Visual Studio Customer Experience Improvement Program (VSCEIP) is designed
                            to help Microsoft improve Visual Studio over time. This program collects information about errors,
                            computer hardware, and how people use Visual Studio, without interrupting users in their tasks at the computer.
                            The information that's collected helps Microsoft identify which features to improve.
                            This document covers how to opt in or out of the VSCEIP.";

            MyAnalyzer service = new MyAnalyzer();
            var result = service.GetWordResults(text: text);
        }

        [TestMethod]
        public void URLSeriveTest()
        {
            string text = @"https://www.4guysfromrolla.com/articles/011211-1.aspx";

            MyAnalyzer service = new MyAnalyzer();
            var result = service.GetSiteResults(url: text);
        }

        [TestMethod]
        public void HtmlTest()
        {
            var htmlWeb = new HtmlWeb();
            // var document = htmlWeb.Load("https://www.4guysfromrolla.com/articles/011211-1.aspx1");
            // var document = htmlWeb.Load("http://www.ecosadmin.com/");
             var document = htmlWeb.Load("http://www.ecosadmin.com.kk/");

            var links = document.DocumentNode.SelectNodes("//a");

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

                }


            }


            var metaTags = document.DocumentNode.SelectNodes("//meta");
            if (metaTags != null)
            {
                foreach (var tag in metaTags)
                {
                    if (tag.Attributes["name"] != null && tag.Attributes["content"] != null)
                    {
                        // ... output tag.Attributes["name"].Value and tag.Attributes["content"].Value...
                    }
                }
            }

        }
    }
}
