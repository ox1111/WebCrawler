using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Abot.Crawler;
using Abot.Poco;
using HtmlAgilityPack;
using System.Net;
using System.Data;
using CsvHelper;
using System.IO;


namespace WebCrawler
{
    public class CrawlWebsite
    {
        public List<string> keywordList;
        public List<string> targetList;
        
        public CrawlWebsite(IEnumerable<TextBox> keywordCollection, IEnumerable<TextBox> targetCollectionn) {

           
            PoliteWebCrawler crawler;
            CrawlResult result;
            CrawlConfiguration crawlConfig = new CrawlConfiguration();
            

            crawlConfig.CrawlTimeoutSeconds = 100;
            crawlConfig.MaxConcurrentThreads = 1;
            crawlConfig.MaxPagesToCrawl = 1;
            

            keywordList = new List<string>();
            targetList = new List<string>();

            foreach (TextBox item in keywordCollection)
            {
                if(item.Text!="")
                    keywordList.Add(item.Text);
            }

            foreach (TextBox item in targetCollectionn)
            {
                if (item.Text != "")
                    targetList.Add(item.Text);
            }


            crawler = new PoliteWebCrawler(crawlConfig);

            //crawler.CrawlBag.MyStream = new TextStream("D:");

            foreach (string keyword in keywordList)
            {
                foreach (string web in targetList)
                {
                    crawler.CrawlBag.MyBaggie = new Baggie(keyword);
                    crawler.PageCrawlStartingAsync += crawler_ProcessPageCrawlStarting;
                    crawler.PageCrawlCompletedAsync += crawler_ProcessPageCrawlCompleted;
                    result = crawler.Crawl(new Uri(web));
                    crawler = new PoliteWebCrawler(crawlConfig);
                }
            }
            
        }

        

        static void crawler_ProcessPageCrawlStarting(object sender, PageCrawlStartingArgs e)
        {
            PageToCrawl pageToCrawl = e.PageToCrawl;
            Console.WriteLine("About to crawl link {0} which was found on page {1}", pageToCrawl.Uri.AbsoluteUri, pageToCrawl.ParentUri.AbsoluteUri);
        }

        static void crawler_ProcessPageCrawlCompleted(object sender, PageCrawlCompletedArgs e)
        {
            int counter = 0;
            DataRow row;
            CrawledPage crawledPage = e.CrawledPage;
            CrawlContext context = e.CrawlContext;
            string keyword;
            //StreamWriter textWritter;
            DataTable crawlTable;

            if (crawledPage.WebException != null || crawledPage.HttpWebResponse.StatusCode != HttpStatusCode.OK)
                Console.WriteLine("Crawl of page failed {0}", crawledPage.Uri.AbsoluteUri);
            else
                Console.WriteLine("Crawl of page succeeded {0}", crawledPage.Uri.AbsoluteUri);

            if (string.IsNullOrEmpty(crawledPage.Content.Text))
                Console.WriteLine("Page had no content {0}", crawledPage.Uri.AbsoluteUri);

            var htmlAgilityPackDocument = crawledPage.HtmlDocument; //Html Agility Pack parser
            var angleSharpHtmlDocument = crawledPage.AngleSharpHtmlDocument; //AngleSharp parser

            // Get attribute from bag
            keyword = context.CrawlBag.MyBaggie.getBaggie();
            //textWritter = context.CrawlBag.MyStream.getTextStream();
            //crawlTable = context.CrawlBag.MyStream.getDataTable();
            crawlTable = new DataTable("crawlTable");
            crawlTable.Columns.Add("id", typeof(int));
            crawlTable.Columns.Add("關鍵字", typeof(string));
            crawlTable.Columns.Add("內容", typeof(string));


            // Search keyword
            HtmlNodeCollection keywordContent = htmlAgilityPackDocument.DocumentNode.SelectNodes("//*[text()[contains(., '" + keyword + "')]]");

            // Generate output log
            foreach (HtmlNode node in keywordContent)
            {
                row = crawlTable.NewRow();
                row["id"] = counter++;
                row["關鍵字"] = keyword;
                row["內容"] = node.InnerText;
                crawlTable.Rows.Add(row);
                Console.WriteLine(node.InnerText);
            }
           

            crawlTable.WriteXml(Application.StartupPath +"\\" + keyword + ".xml");
            //var csv = new CsvWriter(textWritter);

            //foreach (DataColumn column in crawlTable.Columns)
            //{
            //    csv.WriteField(column.ColumnName);
            //}
            //csv.NextRecord();

            //foreach (DataRow temprow in crawlTable.Rows)
            //{
            //    for (var i = 0; i < crawlTable.Columns.Count; i++)
            //    {
            //        csv.WriteField(temprow[i]);
            //    }
            //    csv.NextRecord();
            //}

            //textWritter.Close();
        }


    }
}
