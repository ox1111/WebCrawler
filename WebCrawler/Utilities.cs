using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.ComponentModel;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Net.Mail;

namespace WebCrawler
{
    public static class Utilities
    {
        public static DataTable ToDataTable<T>(this IList<T> data)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }

        public static void SendMailByGmail(List<string>MailList, List<string> Subject, string Body){
            MailMessage msg = new MailMessage();

            if (MailList.Count>0) { 
                msg.To.Add(string.Join(",", MailList.ToArray()));
                msg.From = new MailAddress("test2@gmail.com", "Web Crawler", Encoding.UTF8);
            
                msg.Subject = string.Join(",", Subject.ToArray());
                msg.SubjectEncoding = Encoding.UTF8;
            
                msg.Body = Body;
                msg.IsBodyHtml = true;
                msg.BodyEncoding = Encoding.UTF8;
                msg.Priority = MailPriority.Normal; 
                                               
                #region 其它 Host
                /*
                 *  outlook.com smtp.live.com port:25
                 *  yahoo smtp.mail.yahoo.com.tw port:465
                */
                #endregion
                SmtpClient MySmtp = new SmtpClient("smtp.gmail.com", 587);
                MySmtp.Credentials = new System.Net.NetworkCredential("govermentbuy@gmail.com", "Wang1688");
                MySmtp.EnableSsl = true;
                MySmtp.Send(msg);
            }

        }

        public static string getHTML(DataTable dt)

        {
            StringBuilder myBuilder = new StringBuilder();

            myBuilder.Append("<table border='1px' cellpadding='5' cellspacing='0' ");
            myBuilder.Append("style='border: solid 1px Silver; font-size: x-small;'>");

            myBuilder.Append("<tr align='left' valign='top'>");
            foreach (DataColumn myColumn in dt.Columns)
            {
                myBuilder.Append("<td align='left' valign='top'>");
                myBuilder.Append(myColumn.ColumnName);
                myBuilder.Append("</td>");
            }
            myBuilder.Append("</tr>");

            foreach (DataRow myRow in dt.Rows)
            {
                myBuilder.Append("<tr align='left' valign='top'>");
                foreach (DataColumn myColumn in dt.Columns)
                {
                    myBuilder.Append("<td align='left' valign='top'>");
                    myBuilder.Append(myRow[myColumn.ColumnName].ToString());
                    myBuilder.Append("</td>");
                }
                myBuilder.Append("</tr>");
            }
            myBuilder.Append("</table>");

            return myBuilder.ToString();
        }


        public static List<string> TextBoxListToList(IEnumerable<TextBox> data)
        {
            List<string> list = new List<string>();
            
            foreach (TextBox item in data)
            {
                if (item.Text != "")
                    list.Add(item.Text);
            }
            return list;
        }

    }
}
