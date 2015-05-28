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
    public partial class Form1 : Form
    {
        String txt;
        int b1b2;
        druzyna d1, d2;
        public void po()
        {            
            if (d2 != null && d1 != null)
            {
                pictureBox1.Visible = false;
            }
            this.stats();
        }
        public void pp()
        {
            
            pictureBox1.Visible = true;
        }
        public Form1()
        {
            InitializeComponent();
            label38.AutoSize = false;
            label38.Height = 2;
            label38.Width = 506;
            label38.BorderStyle = BorderStyle.Fixed3D;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public void stats()
        {
            
            if (d2 != null && d1 != null)
            {
                double s1 = 100 * d1.wskzanikogolny() / (d1.wskzanikogolny() + d2.wskzanikogolny());
                double s2 = 100 * d2.wskzanikogolny() / (d1.wskzanikogolny() + d2.wskzanikogolny());
                label40.Text = String.Format("{0:F2}", s1);
                label41.Text = String.Format("{0:F2}", s2);

                double s3 = 0 ;
                if (s1 <= s2)
                    s3 = s2 - s1;
                else
                    s3 = s1 - s2;
                label45.Text = String.Format("{0:F2}", s3);


                progressBar1.Value = (int)s1;
                String co = "";
                double ss1,ss2;
                if (s1>=s2){
                    ss1=s1;
                    ss2=s2;
                }

                else{
                    ss1=s2;
                    ss2=s1;
                }
                if (ss1 - ss2 < 4)
                {
                    co = "Nie obstawiaj bardzo możliwy remis";
                }
                else if (ss1 - ss2 < 20)
                {
                    co = "Nie obstawiaj chuj wie";
                }
                else if (ss1 - ss2 < 30)
                {
                    co = "możesz obstawiać ale bardzo delikatnie";
                }
                else if (ss1 - ss2 < 40)
                {
                    co = "obstawiaj ale nie szalej";
                }
                else if (ss1 - ss2 < 50)
                {
                    co = "obstawiaj powinno się udać";
                }
                else 
                {
                    co = "obstawiaj !!!!!!!!!!";
                };


                label3.Text = co;

                double atakobrona, atakobrona2,aq1,aq2,aw1,aw2,as1,as2,aos;

                //atakobrona = (d1.atak() + d2.obrona()) / 2;
                //atakobrona2 = (d2.atak() + d1.obrona()) / 2;

                aq1 = (d1.atak() + d2.obrona()) / 2; //średnia możliwa na mecz
                aq2 = (d2.atak() + d1.obrona()) / 2;

                aw1 = aq1*(d1.wskzanikogolny()/10);
                aw2 = aq2*(d2.wskzanikogolny()/10);

                as1 = aq1*(d2.wskzanikogolny()/10)*-1;
                as2 = aq2*(d1.wskzanikogolny()/10)*-1;
                atakobrona = aq1 + aw1 + as1;
                atakobrona2 = aq2 + aw2 + as2;
                label43.Text = String.Format("{0:F2}", atakobrona);
                label44.Text = String.Format("{0:F2}", atakobrona2);

                aos = atakobrona + atakobrona2;

                label46.Text = String.Format("{0:F2}", aos);


            }
              
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            pp(); 
            if (listBox1.SelectedIndex != -1)
            {
                d1 = new druzyna(textBox1.Text.ToString(),listBox1.SelectedItem.ToString());
            }
            label1.Text = d1.nazwa;
            label12.Text = d1.przegrane.ToString();
            label5.Text = d1.remisy.ToString();
            label6.Text = d1.wygrane.ToString();
            label23.Text = d1.roznicabramek();
            label22.Text = d1.goleStracone.ToString();
            label24.Text = d1.goleZdobyte.ToString();
            label14.Text = d1.punktyglowne();
            if (radioButton1.Checked == true)
                d1.ladujgospodarza();
            if (radioButton2.Checked == true)
                d1.ladujgoscia();
            gwypiszdane();
            po(); 
        }
        public void gwypiszdane()
        {
            label36.Text = d1.gprzegrane.ToString();
            label34.Text = d1.gremisy.ToString();
            label33.Text = d1.gwygrane.ToString();
            label25.Text = d1.groznicabramek();
            label18.Text = d1.ggoleStracone.ToString();
            label26.Text = d1.ggoleZdobyte.ToString();
            label17.Text = d1.gpunktyglowne();
        }
        public void gwypiszdane2()
        {
            label32.Text = d2.gprzegrane.ToString();
            label30.Text = d2.gremisy.ToString();
            label31.Text = d2.gwygrane.ToString();
            label29.Text = d2.groznicabramek();
            label27.Text = d2.ggoleStracone.ToString();
            label28.Text = d2.ggoleZdobyte.ToString();
            label16.Text = d2.gpunktyglowne();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            pp(); 
            label19.Text = "Jako gospodarz";
            d1.ladujgospodarza();
            this.gwypiszdane();
            po(); 
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            pp(); 
            WebClient client = new WebClient();
            try
            {
                Byte[] pageData = client.DownloadData(textBox1.Text.ToString());
                string pageHtml = Encoding.UTF8.GetString(pageData);
                txt = pageHtml;
                listBox1.Items.Clear();
                listBox2.Items.Clear();
                foreach (Match match in Regex.Matches(txt, "<td class=\"cty\">(.*?)<\\/td>"))
                {
                    listBox1.Items.Add(match.Groups[1].Value);
                    listBox2.Items.Add(match.Groups[1].Value);
                }
            }
            catch
            {

            }
            po(); 
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            pp(); 
            if (listBox2.SelectedIndex != -1)
            {
                if (textBox2.Text != "")
                    d2 = new druzyna(textBox2.Text.ToString(), listBox2.SelectedItem.ToString());
                else
                    d2 = new druzyna(textBox1.Text.ToString(), listBox2.SelectedItem.ToString());
            }
            label2.Text = d2.nazwa;
            label7.Text = d2.przegrane.ToString();
            label9.Text = d2.remisy.ToString();
            label8.Text = d2.wygrane.ToString();
            label21.Text = d2.roznicabramek();
            label10.Text = d2.goleStracone.ToString();
            label11.Text = d2.goleZdobyte.ToString();
            label15.Text = d2.punktyglowne();
            if (radioButton3.Checked == true)
                d2.ladujgospodarza();
            if (radioButton4.Checked == true)
                d2.ladujgoscia();
            gwypiszdane2();
            po(); 
        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {
            pp(); 
            try
            {
                WebClient client = new WebClient();
                Byte[] pageData = client.DownloadData(textBox2.Text.ToString());
                string pageHtml = Encoding.UTF8.GetString(pageData);
                txt = pageHtml;
                listBox2.Items.Clear();
                foreach (Match match in Regex.Matches(txt, "<td class=\"cty\">(.*?)<\\/td>"))
                {
                    listBox2.Items.Add(match.Groups[1].Value);
                }            
            }
            catch
            {

            }
            po();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            pp();
            label20.Text = "Jako gospodarz";
            d2.ladujgospodarza();
            this.gwypiszdane2();
            po();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            pp();
            label19.Text = "Jako gość";
            d1.ladujgoscia();
            this.gwypiszdane();
            po();
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            pp();
            label20.Text = "Jako gość";
            d2.ladujgoscia();
            this.gwypiszdane2();
            po();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pp();
            this.stats();
            po();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            b1b2 = 1;
            Form2 form2 = new Form2(this);
            form2.Owner = this;
            form2.ShowDialog();
        }
        public void ustawlink(String z)
        {
            if (b1b2 == 1)
                textBox1.Text = z;
            else
                textBox2.Text = z;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            b1b2 = 0;
            Form2 form2 = new Form2(this);
            form2.Owner = this;
            form2.ShowDialog();
        }
    }
}
