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
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;

namespace WebCrawler
{
    
    public partial class Form1 : Form
    {
        public IEnumerable<TextBox> keywordCollection;
        public IEnumerable<TextBox> targetCollection;
        public IEnumerable<TextBox> groupBoxCollection;
        public IEnumerable<TextBox> mailListCollection;
        public CrawlWebsite crawler;
        public DateTime scheduleRunTime;
        public TabPage selectedTab;
        public DataTable fullTable;
        
        GroupBox webGPB;
        GroupBox keywordGPB;
        GroupBox mailListGPB;
        List<RootObject> JsonDB;
        List<string> mailList;
        DataTable dtDB;
        

        public class RootObject
        {
            public int id { get; set; }
            public string Keyword { get; set; }
            public string Content { get; set; }
            public string From { get; set; }
            public string Title { get; set; }
        }

        public Form1()
        {
            InitializeComponent();
            timer1.Stop();
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "HH:mm:ss";
            dateTimePicker1.ShowUpDown = true;
            this.FormClosed += new FormClosedEventHandler(Contact_FormClosing);

            this.textBox8.Text = Properties.Settings.Default.textBox8;
            this.textBox9.Text = Properties.Settings.Default.textBox9;
            this.textBox10.Text = Properties.Settings.Default.textBox10;
            this.textBox4.Text = Properties.Settings.Default.textBox4;
            this.textBox5.Text = Properties.Settings.Default.textBox5;
            this.textBox6.Text = Properties.Settings.Default.textBox6;
            this.textBox1.Text = Properties.Settings.Default.textBox1;
            this.textBox2.Text = Properties.Settings.Default.textBox2;
            this.textBox3.Text = Properties.Settings.Default.textBox3;
            this.keyword3.Text = Properties.Settings.Default.keyword3;
            this.keyword1.Text = Properties.Settings.Default.keyword1;
            this.keyword2.Text = Properties.Settings.Default.keyword2;
            this.webBox3.Text = Properties.Settings.Default.webBox3;
            this.webBox1.Text = Properties.Settings.Default.webBox1;
            this.webBox2.Text = Properties.Settings.Default.webBox2;
            this.textBox14.Text = Properties.Settings.Default.textBox14;
            this.textBox15.Text = Properties.Settings.Default.textBox15;
            this.textBox16.Text = Properties.Settings.Default.textBox16;
            this.textBox38.Text = Properties.Settings.Default.textBox38;
            this.textBox39.Text = Properties.Settings.Default.textBox39;
            this.textBox40.Text = Properties.Settings.Default.textBox40;
            this.textBox41.Text = Properties.Settings.Default.textBox41;
            this.textBox42.Text = Properties.Settings.Default.textBox42;
            this.textBox43.Text = Properties.Settings.Default.textBox43;
            this.textBox11.Text = Properties.Settings.Default.textBox11;
            this.textBox12.Text = Properties.Settings.Default.textBox12;
            this.textBox13.Text = Properties.Settings.Default.textBox13;
            this.textBox17.Text = Properties.Settings.Default.textBox17;
            this.textBox18.Text = Properties.Settings.Default.textBox18;
            this.textBox19.Text = Properties.Settings.Default.textBox19;
            this.textBox23.Text = Properties.Settings.Default.textBox23;
            this.textBox24.Text = Properties.Settings.Default.textBox24;
            this.textBox25.Text = Properties.Settings.Default.textBox25;
            this.textBox44.Text = Properties.Settings.Default.textBox44;
            this.textBox45.Text = Properties.Settings.Default.textBox45;
            this.textBox46.Text = Properties.Settings.Default.textBox46;
            this.textBox47.Text = Properties.Settings.Default.textBox47;
            this.textBox48.Text = Properties.Settings.Default.textBox48;
            this.textBox49.Text = Properties.Settings.Default.textBox49;
            this.textBox20.Text = Properties.Settings.Default.textBox20;
            this.textBox21.Text = Properties.Settings.Default.textBox21;
            this.textBox22.Text = Properties.Settings.Default.textBox22;
            this.textBox26.Text = Properties.Settings.Default.textBox26;
            this.textBox27.Text = Properties.Settings.Default.textBox27;
            this.textBox28.Text = Properties.Settings.Default.textBox28;
            this.textBox32.Text = Properties.Settings.Default.textBox32;
            this.textBox33.Text = Properties.Settings.Default.textBox33;
            this.textBox34.Text = Properties.Settings.Default.textBox34;
            this.textBox50.Text = Properties.Settings.Default.textBox50;
            this.textBox51.Text = Properties.Settings.Default.textBox51;
            this.textBox52.Text = Properties.Settings.Default.textBox52;
            this.textBox53.Text = Properties.Settings.Default.textBox53;
            this.textBox54.Text = Properties.Settings.Default.textBox54;
            this.textBox55.Text = Properties.Settings.Default.textBox55;
            this.textBox29.Text = Properties.Settings.Default.textBox29;
            this.textBox30.Text = Properties.Settings.Default.textBox30;
            this.textBox31.Text = Properties.Settings.Default.textBox31;
            this.textBox35.Text = Properties.Settings.Default.textBox35;
            this.textBox36.Text = Properties.Settings.Default.textBox36;
            this.textBox37.Text = Properties.Settings.Default.textBox37;
            this.textBox56.Text = Properties.Settings.Default.textBox56;
            this.textBox57.Text = Properties.Settings.Default.textBox57;
            this.textBox58.Text = Properties.Settings.Default.textBox58;
            this.textBox59.Text = Properties.Settings.Default.textBox59;
            this.textBox60.Text = Properties.Settings.Default.textBox60;
            this.textBox61.Text = Properties.Settings.Default.textBox61;
            this.textBox62.Text = Properties.Settings.Default.textBox62;
            this.textBox63.Text = Properties.Settings.Default.textBox63;
            this.textBox64.Text = Properties.Settings.Default.textBox64;
            this.textBox65.Text = Properties.Settings.Default.textBox65;
            this.textBox66.Text = Properties.Settings.Default.textBox66;
            this.textBox67.Text = Properties.Settings.Default.textBox67;
            this.textBox68.Text = Properties.Settings.Default.textBox68;
            this.textBox69.Text = Properties.Settings.Default.textBox69;
            this.textBox70.Text = Properties.Settings.Default.textBox70;
            this.textBox71.Text = Properties.Settings.Default.textBox71;
            this.textBox72.Text = Properties.Settings.Default.textBox72;
            this.textBox73.Text = Properties.Settings.Default.textBox73;
            this.textBox74.Text = Properties.Settings.Default.textBox74;
            this.textBox75.Text = Properties.Settings.Default.textBox75;
            this.textBox76.Text = Properties.Settings.Default.textBox76;
            this.textBox77.Text = Properties.Settings.Default.textBox77;
            this.textBox78.Text = Properties.Settings.Default.textBox78;
            this.textBox79.Text = Properties.Settings.Default.textBox79;
            this.textBox80.Text = Properties.Settings.Default.textBox80;
            this.textBox81.Text = Properties.Settings.Default.textBox81;
            this.textBox82.Text = Properties.Settings.Default.textBox82;
            this.textBox83.Text = Properties.Settings.Default.textBox83;
            this.textBox84.Text = Properties.Settings.Default.textBox84;
            this.textBox85.Text = Properties.Settings.Default.textBox85;
            this.textBox86.Text = Properties.Settings.Default.textBox86;
            this.textBox87.Text = Properties.Settings.Default.textBox87;
            this.textBox88.Text = Properties.Settings.Default.textBox88;
            this.textBox89.Text = Properties.Settings.Default.textBox89;
            this.textBox90.Text = Properties.Settings.Default.textBox90;
            this.textBox91.Text = Properties.Settings.Default.textBox91;
            this.textBox92.Text = Properties.Settings.Default.textBox92;
            this.textBox93.Text = Properties.Settings.Default.textBox93;
            this.textBox94.Text = Properties.Settings.Default.textBox94;
            this.textBox95.Text = Properties.Settings.Default.textBox95;
            this.textBox96.Text = Properties.Settings.Default.textBox96;
            this.textBox97.Text = Properties.Settings.Default.textBox97;
            this.textBox98.Text = Properties.Settings.Default.textBox98;
            this.textBox99.Text = Properties.Settings.Default.textBox99;
            this.textBox100.Text = Properties.Settings.Default.textBox100;
            this.textBox101.Text = Properties.Settings.Default.textBox101;
            this.textBox102.Text = Properties.Settings.Default.textBox102;
            this.textBox103.Text = Properties.Settings.Default.textBox103;
            this.textBox104.Text = Properties.Settings.Default.textBox104;
            this.textBox105.Text = Properties.Settings.Default.textBox105;
            this.textBox106.Text = Properties.Settings.Default.textBox106;
            this.textBox107.Text = Properties.Settings.Default.textBox107;
            this.textBox108.Text = Properties.Settings.Default.textBox108;
            this.textBox109.Text = Properties.Settings.Default.textBox109;
            this.textBox110.Text = Properties.Settings.Default.textBox110;
            this.textBox111.Text = Properties.Settings.Default.textBox111;
            this.textBox112.Text = Properties.Settings.Default.textBox112;
            this.textBox113.Text = Properties.Settings.Default.textBox113;
            this.textBox114.Text = Properties.Settings.Default.textBox114;
            this.textBox115.Text = Properties.Settings.Default.textBox115;
            this.textBox116.Text = Properties.Settings.Default.textBox116;
            this.textBox117.Text = Properties.Settings.Default.textBox117;
            this.textBox118.Text = Properties.Settings.Default.textBox118;
            this.textBox119.Text = Properties.Settings.Default.textBox119;
            this.textBox120.Text = Properties.Settings.Default.textBox120;
            this.textBox121.Text = Properties.Settings.Default.textBox121;
            this.textBox122.Text = Properties.Settings.Default.textBox122;
            this.textBox123.Text = Properties.Settings.Default.textBox123;
            this.textBox124.Text = Properties.Settings.Default.textBox124;
            this.textBox125.Text = Properties.Settings.Default.textBox125;
            this.textBox126.Text = Properties.Settings.Default.textBox126;
            this.textBox127.Text = Properties.Settings.Default.textBox127;
            this.textBox128.Text = Properties.Settings.Default.textBox128;
            this.textBox129.Text = Properties.Settings.Default.textBox129;
            this.textBox130.Text = Properties.Settings.Default.textBox130;
            this.textBox131.Text = Properties.Settings.Default.textBox131;
            this.textBox132.Text = Properties.Settings.Default.textBox132;
            this.textBox133.Text = Properties.Settings.Default.textBox133;
            this.textBox134.Text = Properties.Settings.Default.textBox134;
            this.textBox135.Text = Properties.Settings.Default.textBox135;
            this.textBox136.Text = Properties.Settings.Default.textBox136;
            this.textBox137.Text = Properties.Settings.Default.textBox137;
            this.textBox138.Text = Properties.Settings.Default.textBox138;
            this.textBox139.Text = Properties.Settings.Default.textBox139;
            this.textBox140.Text = Properties.Settings.Default.textBox140;
            this.textBox141.Text = Properties.Settings.Default.textBox141;
            this.textBox142.Text = Properties.Settings.Default.textBox142;
            this.textBox143.Text = Properties.Settings.Default.textBox143;
            this.textBox144.Text = Properties.Settings.Default.textBox144;
            this.textBox145.Text = Properties.Settings.Default.textBox145;
            

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


            if (DateTime.Now.ToString("HH:mm:ss")== scheduleRunTime.ToString("HH:mm:ss")) {
                timer1.Stop();

                try
                {
                    string text = File.ReadAllText(Application.StartupPath + "\\OutputReport" + "\\fullTable.json");
                    JsonDB = JsonConvert.DeserializeObject<List<RootObject>>(text);
                    dtDB = Utilities.ToDataTable(JsonDB);
                }
                catch (FileNotFoundException)
                {
                    dtDB = null;
                }

                fullTable = new DataTable();
                fullTable.Columns.Add("id", typeof(int));
                fullTable.Columns.Add("Keyword", typeof(string));
                fullTable.Columns.Add("Content", typeof(string));
                fullTable.Columns.Add("From", typeof(string));
                fullTable.Columns.Add("Title", typeof(string));


                for (int i = 0; i < 10; i++)
                {
                    selectedTab = tabControl1.TabPages[i];
                    webGPB = (GroupBox)selectedTab.Controls.Find("WebGroupBox" + (i + 1), true)[0];
                    keywordGPB = (GroupBox)selectedTab.Controls.Find("keywordGB" + (i + 1), true)[0];

                    targetCollection = webGPB.Controls.OfType<TextBox>();
                    keywordCollection = keywordGPB.Controls.OfType<TextBox>();

                    crawler = new CrawlWebsite(keywordCollection, targetCollection, this.label41);
                    crawler.SetFilename = selectedTab.Text;
                    crawler.SetDataTableDB(dtDB);
                    crawler.Crawl();

                    if (crawler.getTable != null)
                    {
                        fullTable.Merge(crawler.getTable);
                    }
                }

                if (dtDB != null)
                {
                    if (dtDB.Rows.Count > 0)
                    {
                        string str_json = JsonConvert.SerializeObject(dtDB);
                        File.WriteAllText(Application.StartupPath + "\\OutputReport" + "\\fullTable.json", str_json);
                    }
                }


                Thread.Sleep(3000);
                timer1.Start();
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            Process.Start(Application.StartupPath + "\\OutputReport");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            

            fullTable = new DataTable();
            fullTable.Columns.Add("id", typeof(int));
            fullTable.Columns.Add("Keyword", typeof(string));
            fullTable.Columns.Add("Content", typeof(string));
            fullTable.Columns.Add("From", typeof(string));
            fullTable.Columns.Add("Title", typeof(string));


            for (int i = 0; i < 10; i++)
            {
                selectedTab = tabControl1.TabPages[i];
                webGPB = (GroupBox)selectedTab.Controls.Find("WebGroupBox"+(i+1), true)[0];
                keywordGPB = (GroupBox)selectedTab.Controls.Find("keywordGB" + (i + 1), true)[0];
                mailListGPB = (GroupBox)selectedTab.Controls.Find("mailGPB" + (i + 1), true)[0];

                targetCollection = webGPB.Controls.OfType<TextBox>();
                keywordCollection = keywordGPB.Controls.OfType<TextBox>();
                mailListCollection = mailListGPB.Controls.OfType<TextBox>();
                mailList = Utilities.TextBoxListToList(mailListCollection);
                

                crawler = new CrawlWebsite(keywordCollection, targetCollection, this.label41);
                crawler.SetFilename = selectedTab.Text;
                crawler.Crawl();

                if (crawler.getTable!=null) {

                    Utilities.SendMailByGmail(mailList, Utilities.TextBoxListToList(keywordCollection), Utilities.getHTML(crawler.getTable));
                    fullTable.Merge(crawler.getTable);
                }
            }

            if (fullTable.Rows.Count>0) { 
                string str_json = JsonConvert.SerializeObject(fullTable);
                File.WriteAllText(Application.StartupPath + "\\OutputReport" + "\\fullTable.json", str_json);
            }

            scheduleRunTime = dateTimePicker1.Value.Date.Add(dateTimePicker1.Value.TimeOfDay);

            this.label39.BackColor = Color.LightGreen;
            this.label39.Text = "Task On Going";
            timer1.Start();
        }

        private void Contact_FormClosing(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.textBox8 = this.textBox8.Text;
            Properties.Settings.Default.textBox9 = this.textBox9.Text;
            Properties.Settings.Default.textBox10 = this.textBox10.Text;
            Properties.Settings.Default.textBox4 = this.textBox4.Text;
            Properties.Settings.Default.textBox5 = this.textBox5.Text;
            Properties.Settings.Default.textBox6 = this.textBox6.Text;
            Properties.Settings.Default.textBox1 = this.textBox1.Text;
            Properties.Settings.Default.textBox2 = this.textBox2.Text;
            Properties.Settings.Default.textBox3 = this.textBox3.Text;
            Properties.Settings.Default.keyword3 = this.keyword3.Text;
            Properties.Settings.Default.keyword1 = this.keyword1.Text;
            Properties.Settings.Default.keyword2 = this.keyword2.Text;
            Properties.Settings.Default.webBox3 = this.webBox3.Text;
            Properties.Settings.Default.webBox1 = this.webBox1.Text;
            Properties.Settings.Default.webBox2 = this.webBox2.Text;
            Properties.Settings.Default.textBox14 = this.textBox14.Text;
            Properties.Settings.Default.textBox15 = this.textBox15.Text;
            Properties.Settings.Default.textBox16 = this.textBox16.Text;
            Properties.Settings.Default.textBox38 = this.textBox38.Text;
            Properties.Settings.Default.textBox39 = this.textBox39.Text;
            Properties.Settings.Default.textBox40 = this.textBox40.Text;
            Properties.Settings.Default.textBox41 = this.textBox41.Text;
            Properties.Settings.Default.textBox42 = this.textBox42.Text;
            Properties.Settings.Default.textBox43 = this.textBox43.Text;
            Properties.Settings.Default.textBox11 = this.textBox11.Text;
            Properties.Settings.Default.textBox12 = this.textBox12.Text;
            Properties.Settings.Default.textBox13 = this.textBox13.Text;
            Properties.Settings.Default.textBox17 = this.textBox17.Text;
            Properties.Settings.Default.textBox18 = this.textBox18.Text;
            Properties.Settings.Default.textBox19 = this.textBox19.Text;
            Properties.Settings.Default.textBox23 = this.textBox23.Text;
            Properties.Settings.Default.textBox24 = this.textBox24.Text;
            Properties.Settings.Default.textBox25 = this.textBox25.Text;
            Properties.Settings.Default.textBox44 = this.textBox44.Text;
            Properties.Settings.Default.textBox45 = this.textBox45.Text;
            Properties.Settings.Default.textBox46 = this.textBox46.Text;
            Properties.Settings.Default.textBox47 = this.textBox47.Text;
            Properties.Settings.Default.textBox48 = this.textBox48.Text;
            Properties.Settings.Default.textBox49 = this.textBox49.Text;
            Properties.Settings.Default.textBox20 = this.textBox20.Text;
            Properties.Settings.Default.textBox21 = this.textBox21.Text;
            Properties.Settings.Default.textBox22 = this.textBox22.Text;
            Properties.Settings.Default.textBox26 = this.textBox26.Text;
            Properties.Settings.Default.textBox27 = this.textBox27.Text;
            Properties.Settings.Default.textBox28 = this.textBox28.Text;
            Properties.Settings.Default.textBox32 = this.textBox32.Text;
            Properties.Settings.Default.textBox33 = this.textBox33.Text;
            Properties.Settings.Default.textBox34 = this.textBox34.Text;
            Properties.Settings.Default.textBox50 = this.textBox50.Text;
            Properties.Settings.Default.textBox51 = this.textBox51.Text;
            Properties.Settings.Default.textBox52 = this.textBox52.Text;
            Properties.Settings.Default.textBox53 = this.textBox53.Text;
            Properties.Settings.Default.textBox54 = this.textBox54.Text;
            Properties.Settings.Default.textBox55 = this.textBox55.Text;
            Properties.Settings.Default.textBox29 = this.textBox29.Text;
            Properties.Settings.Default.textBox30 = this.textBox30.Text;
            Properties.Settings.Default.textBox31 = this.textBox31.Text;
            Properties.Settings.Default.textBox35 = this.textBox35.Text;
            Properties.Settings.Default.textBox36 = this.textBox36.Text;
            Properties.Settings.Default.textBox37 = this.textBox37.Text;
            Properties.Settings.Default.textBox56 = this.textBox56.Text;
            Properties.Settings.Default.textBox57 = this.textBox57.Text;
            Properties.Settings.Default.textBox58 = this.textBox58.Text;
            Properties.Settings.Default.textBox59 = this.textBox59.Text;
            Properties.Settings.Default.textBox60 = this.textBox60.Text;
            Properties.Settings.Default.textBox61 = this.textBox61.Text;
            Properties.Settings.Default.textBox62 = this.textBox62.Text;
            Properties.Settings.Default.textBox63 = this.textBox63.Text;
            Properties.Settings.Default.textBox64 = this.textBox64.Text;
            Properties.Settings.Default.textBox65 = this.textBox65.Text;
            Properties.Settings.Default.textBox66 = this.textBox66.Text;
            Properties.Settings.Default.textBox67 = this.textBox67.Text;
            Properties.Settings.Default.textBox68 = this.textBox68.Text;
            Properties.Settings.Default.textBox69 = this.textBox69.Text;
            Properties.Settings.Default.textBox70 = this.textBox70.Text;
            Properties.Settings.Default.textBox71 = this.textBox71.Text;
            Properties.Settings.Default.textBox72 = this.textBox72.Text;
            Properties.Settings.Default.textBox73 = this.textBox73.Text;
            Properties.Settings.Default.textBox74 = this.textBox74.Text;
            Properties.Settings.Default.textBox75 = this.textBox75.Text;
            Properties.Settings.Default.textBox76 = this.textBox76.Text;
            Properties.Settings.Default.textBox77 = this.textBox77.Text;
            Properties.Settings.Default.textBox78 = this.textBox78.Text;
            Properties.Settings.Default.textBox79 = this.textBox79.Text;
            Properties.Settings.Default.textBox80 = this.textBox80.Text;
            Properties.Settings.Default.textBox81 = this.textBox81.Text;
            Properties.Settings.Default.textBox82 = this.textBox82.Text;
            Properties.Settings.Default.textBox83 = this.textBox83.Text;
            Properties.Settings.Default.textBox84 = this.textBox84.Text;
            Properties.Settings.Default.textBox85 = this.textBox85.Text;
            Properties.Settings.Default.textBox86 = this.textBox86.Text;
            Properties.Settings.Default.textBox87 = this.textBox87.Text;
            Properties.Settings.Default.textBox88 = this.textBox88.Text;
            Properties.Settings.Default.textBox89 = this.textBox89.Text;
            Properties.Settings.Default.textBox90 = this.textBox90.Text;
            Properties.Settings.Default.textBox91 = this.textBox91.Text;
            Properties.Settings.Default.textBox92 = this.textBox92.Text;
            Properties.Settings.Default.textBox93 = this.textBox93.Text;
            Properties.Settings.Default.textBox94 = this.textBox94.Text;
            Properties.Settings.Default.textBox95 = this.textBox95.Text;
            Properties.Settings.Default.textBox96 = this.textBox96.Text;
            Properties.Settings.Default.textBox97 = this.textBox97.Text;
            Properties.Settings.Default.textBox98 = this.textBox98.Text;
            Properties.Settings.Default.textBox99 = this.textBox99.Text;
            Properties.Settings.Default.textBox100 = this.textBox100.Text;
            Properties.Settings.Default.textBox101 = this.textBox101.Text;
            Properties.Settings.Default.textBox102 = this.textBox102.Text;
            Properties.Settings.Default.textBox103 = this.textBox103.Text;
            Properties.Settings.Default.textBox104 = this.textBox104.Text;
            Properties.Settings.Default.textBox105 = this.textBox105.Text;
            Properties.Settings.Default.textBox106 = this.textBox106.Text;
            Properties.Settings.Default.textBox107 = this.textBox107.Text;
            Properties.Settings.Default.textBox108 = this.textBox108.Text;
            Properties.Settings.Default.textBox109 = this.textBox109.Text;
            Properties.Settings.Default.textBox110 = this.textBox110.Text;
            Properties.Settings.Default.textBox111 = this.textBox111.Text;
            Properties.Settings.Default.textBox112 = this.textBox112.Text;
            Properties.Settings.Default.textBox113 = this.textBox113.Text;
            Properties.Settings.Default.textBox114 = this.textBox114.Text;
            Properties.Settings.Default.textBox115 = this.textBox115.Text;
            Properties.Settings.Default.textBox116 = this.textBox116.Text;
            Properties.Settings.Default.textBox117 = this.textBox117.Text;
            Properties.Settings.Default.textBox118 = this.textBox118.Text;
            Properties.Settings.Default.textBox119 = this.textBox119.Text;
            Properties.Settings.Default.textBox120 = this.textBox120.Text;
            Properties.Settings.Default.textBox121 = this.textBox121.Text;
            Properties.Settings.Default.textBox122 = this.textBox122.Text;
            Properties.Settings.Default.textBox123 = this.textBox123.Text;
            Properties.Settings.Default.textBox124 = this.textBox124.Text;
            Properties.Settings.Default.textBox125 = this.textBox125.Text;
            Properties.Settings.Default.textBox126 = this.textBox126.Text;
            Properties.Settings.Default.textBox127 = this.textBox127.Text;
            Properties.Settings.Default.textBox128 = this.textBox128.Text;
            Properties.Settings.Default.textBox129 = this.textBox129.Text;
            Properties.Settings.Default.textBox130 = this.textBox130.Text;
            Properties.Settings.Default.textBox131 = this.textBox131.Text;
            Properties.Settings.Default.textBox132 = this.textBox132.Text;
            Properties.Settings.Default.textBox133 = this.textBox133.Text;
            Properties.Settings.Default.textBox134 = this.textBox134.Text;
            Properties.Settings.Default.textBox135 = this.textBox135.Text;
            Properties.Settings.Default.textBox136 = this.textBox136.Text;
            Properties.Settings.Default.textBox137 = this.textBox137.Text;
            Properties.Settings.Default.textBox138 = this.textBox138.Text;
            Properties.Settings.Default.textBox139 = this.textBox139.Text;
            Properties.Settings.Default.textBox140 = this.textBox140.Text;
            Properties.Settings.Default.textBox141 = this.textBox141.Text;
            Properties.Settings.Default.textBox142 = this.textBox142.Text;
            Properties.Settings.Default.textBox143 = this.textBox143.Text;
            Properties.Settings.Default.textBox144 = this.textBox144.Text;
            Properties.Settings.Default.textBox145 = this.textBox145.Text;
            Properties.Settings.Default.Save();

        }

        private void groupBox23_Enter(object sender, EventArgs e){}

        private void button1_Click_1(object sender, EventArgs e)
        {
           
        }
    }

    
}
