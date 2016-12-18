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
        public DataTable DtDB = null;
        string Filename;
        int counter = 1;
        bool dbExists = false;

        public Baggie(DataTable dtDB)
        {
            CombineTable = new DataTable();
            CombineTable.Columns.Add("id", typeof(int));
            CombineTable.Columns.Add("Keyword", typeof(string));
            CombineTable.Columns.Add("Content", typeof(string));
            CombineTable.Columns.Add("From", typeof(string));
            CombineTable.Columns.Add("Title", typeof(string));
            DtDB = dtDB;

            if (DtDB!=null) { 
                if (DtDB.Rows.Count>0) {
                    dbExists = true;
                }
            }

        }


        public DataTable Table{
            get{ return CombineTable; }    
            set{ CombineTable = value; }
        }

        public void addRowToTable(HtmlNodeCollection keywordHtml, string keyword, string web,string title)
        {
            DataRow row;
            bool exists = false;


            foreach (HtmlNode node in keywordHtml)
            {

                if (dbExists) {
                    exists = DtDB.AsEnumerable().Any(c => c.Field<string>("Content") == node.InnerText);
                }

                if (exists == false)
                {
                    row = CombineTable.NewRow();
                    row["id"] = counter++;
                    row["Keyword"] = keyword;
                    row["Content"] = node.InnerText;
                    row["From"] = web;
                    row["Title"] = title;
                    CombineTable.Rows.Add(row);
                    if (dbExists)
                    {
                        DtDB.Rows.Add(row.ItemArray);
                    }
                }
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
            return Filename;

        }

    }
}
