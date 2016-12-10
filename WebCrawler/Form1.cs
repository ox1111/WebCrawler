using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebCrawler
{
    

    public partial class Form1 : Form
    {
        public IEnumerable<TextBox> keywordCollection;
        public IEnumerable<TextBox> targetCollection;
        public CrawlWebsite crawler;
        public Form1()
        {
            InitializeComponent();
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
            crawler = new CrawlWebsite(keywordCollection, targetCollection);
        }
    }
}
