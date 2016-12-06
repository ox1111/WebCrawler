using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abot.Crawler;
using Abot.Poco;

namespace WebCrawler
{
    class WebDev
    {
        void CrawlConfig() {
            CrawlConfiguration crawlConfig = new CrawlConfiguration();
            crawlConfig.MaxPagesToCrawl = 1000;

            PoliteWebCrawler crawler = new PoliteWebCrawler();


        }
        
    }
}
