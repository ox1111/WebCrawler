using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace WebCrawler
{
    public class Baggie
    {
        string Keyword;
        public Baggie(string keyword) {
            Keyword = keyword;
            
        }

        public string getBaggie() {
            return Keyword;
        }
    }
}
