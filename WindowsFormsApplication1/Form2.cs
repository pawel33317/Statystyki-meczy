using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace zaklady
{
    public partial class Form2 : Form
    {
        Form1 form1;
        public Form2(Form1 form1)
        {
            this.form1 = form1;
            InitializeComponent();

        }
        String srodeklinku;
        List<linkl> l1 = new List<linkl>();
        List<linkl> l2 = new List<linkl>();
        private void Form2_Load(object sender, EventArgs e)
        {
            this.showcountry();
        }

        public void showcountry()
        {
            try
            {
                WebClient client = new WebClient();
                Byte[] pageData = client.DownloadData(@"http://www.livescore.net/");
                string pageHtml = Encoding.UTF8.GetString(pageData);
                String str = pageHtml;
                
                listBox2.Items.Clear();
                foreach (Match match in Regex.Matches(str, "<li> <a href=\"(.*?)\" class=\"cat\">(.*?)</a>"))
                {
                    listBox1.Items.Add(match.Groups[2].Value);
                    l1.Add(new linkl(match.Groups[2].Value, match.Groups[1].Value));
                }
            }
            catch { }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                String ln="";
                foreach (linkl tmp in l1)
                {
                    if (tmp.nazwa == listBox1.SelectedItem.ToString())
                        ln = tmp.link;
                }
               string[] split = ln.Split("/".ToCharArray());
               ln = "/"+split[1] + "/" + split[2] + "/";
               srodeklinku = ln;
                try
                {
                    String llll = "http://www.livescore.net" + ln;
                    WebClient client = new WebClient();
                    Byte[] pageData = client.DownloadData(llll);
                    string pageHtml = Encoding.UTF8.GetString(pageData);
                    String str = pageHtml;
                    listBox2.Items.Clear();
                    foreach (Match match in Regex.Matches(str, "<li><a href=\""+ln+"(.*?)\" title=\"(.*?)\">(.*?)</a></li> "))
                    {
                        listBox2.Items.Add(match.Groups[3].Value);
                        l2.Add(new linkl(match.Groups[3].Value, match.Groups[1].Value));
                    }
                }
                catch { }
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        class linkl {
            public linkl(String a, String b)
            {
                this.nazwa = a;
                this.link = b;

            }
            public String nazwa;
            public String link;
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            String ln="";
            foreach (linkl tmp in l2)
            {
                if (tmp.nazwa == listBox2.SelectedItem.ToString())
                    ln = tmp.link;
            }
            form1.ustawlink("http://www.livescore.net" + srodeklinku + ln);
            //label2.Text = "http://www.livescore.net" + srodeklinku + ln;
            //textBox1.Text = "http://www.livescore.net" + srodeklinku + ln;
            this.Close();
        }
    }
}

