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
        public IEnumerable<GroupBox> groupBoxCollection;
        public CrawlWebsite crawler;
        public DateTime timeVar;
        public TabPage selectedTab;
        GroupBox webGPB;
        GroupBox keywordGPB;


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
            //MessageBox.Show(Application.StartupPath);
            Process.Start(Application.StartupPath + "\\OutputReport");
        }

        private void button4_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < 10; i++)
            {
                selectedTab = tabControl1.TabPages[i];
                webGPB = (GroupBox)selectedTab.Controls.Find("WebGroupBox"+(i+1), true)[0];
                keywordGPB = (GroupBox)selectedTab.Controls.Find("keywordGB" + (i + 1), true)[0];

                targetCollection = webGPB.Controls.OfType<TextBox>();
                keywordCollection = keywordGPB.Controls.OfType<TextBox>();

                crawler = new CrawlWebsite(keywordCollection, targetCollection, this.label41);
                crawler.SetFilename = selectedTab.Text;
                crawler.Crawl();
                
            }
            this.label39.BackColor = Color.LightGreen;
            this.label39.Text = "Task On Going";
            timer1.Start();

        }

        private void groupBox23_Enter(object sender, EventArgs e)
        {

        }
    }
}
