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
using Newtonsoft.Json;


namespace WebCrawler
{
    public class Baggie
    {
        

        DataTable CombineTable;
        StreamWriter textWritter;
        DateTime today;
        DialogResult msgBox;
        string Filename;
        int counter = 1;

        public Baggie()
        {
            CombineTable = new DataTable("crawlTable");
            CombineTable.Columns.Add("id", typeof(int));
            CombineTable.Columns.Add("Keyword", typeof(string));
            CombineTable.Columns.Add("Content", typeof(string));
            CombineTable.Columns.Add("From", typeof(string));
            CombineTable.Columns.Add("Title", typeof(string));
        }


        public DataTable Table{
            get{ return CombineTable; }    
            set{ CombineTable = value; }
        }

        public void addRowToTable(HtmlNodeCollection keywordHtml, string keyword, string web,string title)
        {
            DataRow row;
            

            foreach (HtmlNode node in keywordHtml)
            {
                row = CombineTable.NewRow();
                row["id"] = counter++;
                row["Keyword"] = keyword;
                row["Content"] = node.InnerText;
                row["From"] = web;
                row["Title"] = title;
                CombineTable.Rows.Add(row);
                //Console.WriteLine(node.InnerText);
            }
           
        }

        public string generateReport(string filename)
        {
            today = DateTime.Today;
            Filename = filename+ "-" + today.ToString("yyyy-dd-MM") + ".csv";
            
            if(CombineTable.Rows.Count != 0){
                using (textWritter = new StreamWriter(Application.StartupPath + "\\OutputReport\\" + Filename, false, Encoding.UTF8))
                {
                    var csv = new CsvWriter(textWritter);
                    csv.Configuration.Encoding = Encoding.GetEncoding("utf-8");
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
                    textWritter.Dispose();
                }
            }

            #region Set Stream (try...catch)
            //try
            //{

            //}
            //catch (IOException)
            //{
            //    msgBox = MessageBox.Show("Turn Off Exsistence File!!! Otherwise the file will not be saved!!", "Warning!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            //    if (msgBox == DialogResult.Yes)
            //    {
            //        textWritter = new StreamWriter(Application.StartupPath + "\\OutputReport\\" + Filename, false, Encoding.UTF8);

            //    }
            //    else if (msgBox == DialogResult.No)
            //    {
            //        return null;
            //    }
            //}
            #endregion



            return Filename;

        }

    }
}
