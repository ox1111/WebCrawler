using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Data;
using System.IO;
using System.Windows.Forms;
using CsvHelper;
using System.Data.SqlClient;

namespace WebCrawler
{
    public class Baggie
    {
        int KeywordCount;
        int WebsiteCount;

        DataTable CombineTable;
        StreamWriter textWritter;
        List<string> KeywordList;
        List<string> WebsiteList;
        DateTime today;



        public Baggie(int keywordCount, int websiteCount, List<string> keywordList, List<string> websiteList) {
            today = DateTime.Today;

            textWritter = new StreamWriter(Application.StartupPath + "\\OutputReport\\" + today.ToString("yyyy-dd-MM") +".csv", true, Encoding.UTF8);

            KeywordList = keywordList;
            WebsiteList = websiteList;
            KeywordCount = keywordCount;
            WebsiteCount = websiteCount;

            CombineTable = new DataTable("crawlTable");
            CombineTable.Columns.Add("id", typeof(int));
            CombineTable.Columns.Add("關鍵字", typeof(string));
            CombineTable.Columns.Add("內容", typeof(string));
            CombineTable.Columns.Add("From", typeof(string));

        }

        public void combineTable(DataTable table) {
            CombineTable.Merge(table);
        }

        public string generateReport() {
            string outputName = today.ToString("yyyy-dd-MM") + ".xml";

            if (CombineTable != null)
            {
                var csv = new CsvWriter(textWritter);
                csv.Configuration.Encoding =Encoding.GetEncoding("utf-8");
                foreach (DataColumn column in CombineTable.Columns)
                {
                    csv.WriteField(column.ColumnName);
                }
                csv.NextRecord();

                foreach (DataRow temprow in CombineTable.Rows)
                {
                    for (var i = 0; i < CombineTable.Columns.Count; i++)
                    {
                        csv.WriteField(temprow[i]);
                    }
                    csv.NextRecord();
                }
            }
            textWritter.Dispose();
            //textWritter.Close();
            return outputName;
        }

        public DataTable generateTable(HtmlNodeCollection keywordHtml, DataTable crawlTable, string keyword,string web) {
            DataRow row;
            int counter = 1;

            foreach (HtmlNode node in keywordHtml)
            {
                row = crawlTable.NewRow();
                row["id"] = counter++;
                row["關鍵字"] = keyword;
                row["內容"] = node.InnerText;
                row["From"] = web;
                crawlTable.Rows.Add(row);
                Console.WriteLine(node.InnerText);
            }
            return crawlTable;
        }


    }
}
