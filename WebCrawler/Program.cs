using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using Abot.Crawler;
using Abot.Poco;

namespace WebCrawler
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

            //Console.WriteLine("Hello World");
            //string url = "https://arachnode.net/media/g/releases/default.aspx";

            //HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            //request.UserAgent = "A .NET Web Crawler";
            //WebResponse response = request.GetResponse();
            //Stream stream = response.GetResponseStream();
            //StreamReader reader = new StreamReader(stream);
            //string htmlText = reader.ReadToEnd();
            //Console.WriteLine(htmlText);

        }

       

    }
}
