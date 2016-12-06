using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Abot.Crawler;
using Abot.Poco;
using log4net.Config;
using HtmlAgilityPack;

namespace AbotDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //log4net.Config.XmlConfigurator.Configure();

            CrawlConfiguration crawlConfig = new CrawlConfiguration();
            //crawlConfig.DownloadableContentTypes = "text/html, text/plain";
            crawlConfig.CrawlTimeoutSeconds = 100;
            crawlConfig.MaxConcurrentThreads = 1;
            crawlConfig.MaxPagesToCrawl = 1;
            

            PoliteWebCrawler crawler = new PoliteWebCrawler(crawlConfig);

            crawler.PageCrawlStartingAsync += crawler_ProcessPageCrawlStarting;
            crawler.PageCrawlCompletedAsync += crawler_ProcessPageCrawlCompleted;
            crawler.PageCrawlDisallowedAsync += crawler_PageCrawlDisallowed;
            crawler.PageLinksCrawlDisallowedAsync += crawler_PageLinksCrawlDisallowed;

            CrawlResult result = crawler.Crawl(new Uri("http://www.kmhk.kmu.edu.tw/news/list.asp?P_classify=9")); //This is synchronous, it will not go to the next line until the crawl has completed

            if (result.ErrorOccurred)
                Console.WriteLine("Crawl of {0} completed with error: {1}", result.RootUri.AbsoluteUri, result.ErrorException.Message);
            else
                Console.WriteLine("Crawl of {0} completed without error.", result.RootUri.AbsoluteUri);

            
        }

        static void crawler_ProcessPageCrawlStarting(object sender, PageCrawlStartingArgs e)
        {
            PageToCrawl pageToCrawl = e.PageToCrawl;
            Console.WriteLine("About to crawl link {0} which was found on page {1}", pageToCrawl.Uri.AbsoluteUri, pageToCrawl.ParentUri.AbsoluteUri);
        }

        static void crawler_ProcessPageCrawlCompleted(object sender, PageCrawlCompletedArgs e)
        {
            CrawledPage crawledPage = e.CrawledPage;

            if (crawledPage.WebException != null || crawledPage.HttpWebResponse.StatusCode != HttpStatusCode.OK)
                Console.WriteLine("Crawl of page failed {0}", crawledPage.Uri.AbsoluteUri);
            else
                Console.WriteLine("Crawl of page succeeded {0}", crawledPage.Uri.AbsoluteUri);

            if (string.IsNullOrEmpty(crawledPage.Content.Text))
                Console.WriteLine("Page had no content {0}", crawledPage.Uri.AbsoluteUri);

            HtmlDocument htmlAgilityPackDocument = crawledPage.HtmlDocument; //Html Agility Pack parser
            var angleSharpHtmlDocument = crawledPage.AngleSharpHtmlDocument; //AngleSharp parser

            HtmlNodeCollection table = htmlAgilityPackDocument.DocumentNode.SelectNodes("//table");

            foreach (HtmlNode node in table)
            {
                string strValue = node.InnerText;  //擷取字串
                Console.WriteLine(strValue);
            }


        }

        static void crawler_PageLinksCrawlDisallowed(object sender, PageLinksCrawlDisallowedArgs e)
        {
            CrawledPage crawledPage = e.CrawledPage;
            Console.WriteLine("Did not crawl the links on page {0} due to {1}", crawledPage.Uri.AbsoluteUri, e.DisallowedReason);
        }

        static void crawler_PageCrawlDisallowed(object sender, PageCrawlDisallowedArgs e)
        {
            PageToCrawl pageToCrawl = e.PageToCrawl;
            Console.WriteLine("Did not crawl page {0} due to {1}", pageToCrawl.Uri.AbsoluteUri, e.DisallowedReason);
        }
    }
}
