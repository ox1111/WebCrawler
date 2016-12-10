using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;

namespace WebCrawler
{
    public class TextStream
    {
        StreamWriter textWritter;
        DataTable crawlTable;
        public TextStream(string filepath) {
            //textWritter = File.CreateText(filepath + "\\NewCsv.csv");
            crawlTable = new DataTable("crawlTable");
            crawlTable.Columns.Add("id", typeof(int));
            crawlTable.Columns.Add("關鍵字", typeof(string));
            crawlTable.Columns.Add("內容", typeof(string));

        }

        public StreamWriter getTextStream()
        {
            return textWritter;
        }

        public DataTable getDataTable()
        {
            return crawlTable;
        }

        public void Terminate()
        {
            textWritter.Close();
        }

    }
}
