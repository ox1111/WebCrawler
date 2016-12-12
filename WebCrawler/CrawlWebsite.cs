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
using Abot.Core;
using System.IO.Compression;
using System.Text.RegularExpressions;

namespace WebCrawler
{
    public class CrawlWebsite
    {
        public List<string> keywordList;
        public List<string> targetList;
        
        public CrawlWebsite(IEnumerable<TextBox> keywordCollection, IEnumerable<TextBox> targetCollectionn,Label outputLabel) {

            WebClient url = new WebClient();
            HtmlAgilityPack.HtmlDocument doc;

            HtmlNodeCollection keywordContent;
            keywordList = new List<string>();
            targetList = new List<string>();
            DataTable crawlTable;
            Baggie myBag;
            //MemoryStream ms ;

            crawlTable = new DataTable("crawlTable");
            doc = new HtmlAgilityPack.HtmlDocument();
            

            crawlTable.Columns.Add("id", typeof(int));
            crawlTable.Columns.Add("關鍵字", typeof(string));
            crawlTable.Columns.Add("內容", typeof(string));
            crawlTable.Columns.Add("From", typeof(string));

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

            myBag = new Baggie(keywordList.Count, targetList.Count, keywordList, targetList);



            foreach (string keyword in keywordList)
            {
                foreach (string web in targetList)
                {
                    
                    HttpDownloader downloader = new HttpDownloader(web, null, null);
                    doc.LoadHtml(downloader.GetPage());
                    keywordContent = doc.DocumentNode.SelectNodes("//*[text()[contains(., '" + keyword + "')]]");

                    if (keywordContent != null) { 
                    crawlTable = myBag.generateTable(keywordContent, crawlTable, keyword, web);
                    myBag.combineTable(crawlTable);
                    }
                }
            }


            outputLabel.Text = myBag.generateReport();
        }

        public class HttpDownloader
        {
            private readonly string _referer;
            private readonly string _userAgent;

            public Encoding Encoding { get; set; }
            public WebHeaderCollection Headers { get; set; }
            public Uri Url { get; set; }

            public HttpDownloader(string url, string referer, string userAgent)
            {
                Encoding = Encoding.GetEncoding("ISO-8859-1");
                Url = new Uri(url); // verify the uri
                _userAgent = userAgent;
                _referer = referer;
            }

            public string GetPage()
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                if (!string.IsNullOrEmpty(_referer))
                    request.Referer = _referer;
                if (!string.IsNullOrEmpty(_userAgent))
                    request.UserAgent = _userAgent;

                request.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip,deflate");

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    Headers = response.Headers;
                    Url = response.ResponseUri;
                    return ProcessContent(response);
                }

            }

            private string ProcessContent(HttpWebResponse response)
            {
                SetEncodingFromHeader(response);

                Stream s = response.GetResponseStream();
                if (response.ContentEncoding.ToLower().Contains("gzip"))
                    s = new GZipStream(s, CompressionMode.Decompress);
                else if (response.ContentEncoding.ToLower().Contains("deflate"))
                    s = new DeflateStream(s, CompressionMode.Decompress);

                MemoryStream memStream = new MemoryStream();
                int bytesRead;
                byte[] buffer = new byte[0x1000];
                for (bytesRead = s.Read(buffer, 0, buffer.Length); bytesRead > 0; bytesRead = s.Read(buffer, 0, buffer.Length))
                {
                    memStream.Write(buffer, 0, bytesRead);
                }
                s.Close();
                string html;
                memStream.Position = 0;
                using (StreamReader r = new StreamReader(memStream, Encoding))
                {
                    html = r.ReadToEnd().Trim();
                    html = CheckMetaCharSetAndReEncode(memStream, html);
                }

                return html;
            }

            private void SetEncodingFromHeader(HttpWebResponse response)
            {
                string charset = null;
                if (string.IsNullOrEmpty(response.CharacterSet))
                {
                    Match m = Regex.Match(response.ContentType, @";\s*charset\s*=\s*(?<charset>.*)", RegexOptions.IgnoreCase);
                    if (m.Success)
                    {
                        charset = m.Groups["charset"].Value.Trim(new[] { '\'', '"' });
                    }
                }
                else
                {
                    charset = response.CharacterSet;
                }
                if (!string.IsNullOrEmpty(charset))
                {
                    try
                    {
                        Encoding = Encoding.GetEncoding(charset);
                    }
                    catch (ArgumentException)
                    {
                    }
                }
            }

            private string CheckMetaCharSetAndReEncode(Stream memStream, string html)
            {
                Match m = new Regex(@"<meta\s+.*?charset\s*=\s*(?<charset>[A-Za-z0-9_-]+)", RegexOptions.Singleline | RegexOptions.IgnoreCase).Match(html);
                if (m.Success)
                {
                    string charset = m.Groups["charset"].Value.ToLower() ?? "iso-8859-1";
                    if ((charset == "unicode") || (charset == "utf-16"))
                    {
                        charset = "utf-8";
                    }

                    try
                    {
                        Encoding metaEncoding = Encoding.GetEncoding(charset);
                        if (Encoding != metaEncoding)
                        {
                            memStream.Position = 0L;
                            StreamReader recodeReader = new StreamReader(memStream, metaEncoding);
                            html = recodeReader.ReadToEnd().Trim();
                            recodeReader.Close();
                        }
                    }
                    catch (ArgumentException)
                    {
                    }
                }

                return html;
            }
        }

    }
}
