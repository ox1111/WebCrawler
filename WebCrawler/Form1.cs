using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;


namespace WebCrawler
{
    
    public partial class Form1 : Form
    {
        public IEnumerable<TextBox> keywordCollection;
        public IEnumerable<TextBox> targetCollection;
        public CrawlWebsite crawler;
        public DateTime timeVar;
        public Form1()
        {
            InitializeComponent();
            timer1.Stop();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            targetCollection = groupBox1.Controls.OfType<TextBox>();
            keywordCollection = groupBox2.Controls.OfType<TextBox>();
            crawler = new CrawlWebsite(keywordCollection, targetCollection,this.label41);
            this.label39.BackColor = Color.LightGreen;
            this.label39.Text = "Task On Going";
            timer1.Start();
        }

        private void label39_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.label40.Text = DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss");

            if (DateTime.Now.ToString("HH:mm:ss")=="08:00:00") {
                timer1.Stop();
                crawler = new CrawlWebsite(keywordCollection, targetCollection, this.label41);
                Thread.Sleep(3000);
                timer1.Start();
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            Process.Start(Application.StartupPath + "\\OutputReport");
        }
    }
}
