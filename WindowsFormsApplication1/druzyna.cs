using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace zaklady
{
    class druzyna
    {
        public String nazwa;
        public String link;
        public int iloscMeczy;
        public int wygrane;
        public int remisy;
        public int przegrane;
        public int goleZdobyte;
        public int goleStracone;
        public int czywaznymecz = 1;

        public int giloscMeczy;
        public int gwygrane;
        public int gremisy;
        public int gprzegrane;
        public int ggoleZdobyte;
        public int ggoleStracone;

        public druzyna(String plink, String pnazwa)
        {
            this.nazwa = pnazwa;
            this.link = plink;
            WebClient client = new WebClient();
            Byte[] pageData = client.DownloadData(plink);
            string pageHtml = Encoding.UTF8.GetString(pageData);

            Match match = Regex.Match(pageHtml, "<td class=\"cty\">" + pnazwa + "</td> <td>(.*?)<\\/td> <td>(.*?)<\\/td> <td>(.*?)<\\/td> <td>(.*?)<\\/td> <td>(.*?)<\\/td> <td>(.*?)<\\/td> <td>(.*?)<\\/td>");
            iloscMeczy = int.Parse(match.Groups[1].Value);
            wygrane = int.Parse(match.Groups[2].Value);
            remisy = int.Parse(match.Groups[3].Value);
            przegrane = int.Parse(match.Groups[4].Value);
            goleZdobyte = int.Parse(match.Groups[5].Value);
            goleStracone = int.Parse(match.Groups[6].Value);
        }

        public String roznicabramek()
        {
            int x = goleZdobyte - goleStracone;
            return x.ToString();
        }
        public String groznicabramek()
        {
            int x = ggoleZdobyte - ggoleStracone;
            return x.ToString();
        }
        public double wskzanikogolny()
        {
            double punktyZaMecze, gpunktyZaMecze;
            double punktyZaGole = 0.0;
            double gpunktyZaGole;

            if (iloscMeczy != 0)
                punktyZaMecze = ((double)wygrane * 2 + (double)remisy) / (double)iloscMeczy;
            else 
                punktyZaMecze = 0;

            if (goleZdobyte - goleStracone != 0)
                punktyZaGole = (double)goleZdobyte - (double)goleStracone;
            else
                gpunktyZaGole = 0;



            if (giloscMeczy != 0)
                gpunktyZaMecze = (((double)gwygrane * 2 + (double)gremisy) / (double)giloscMeczy) / 2;
            else
                gpunktyZaMecze = 0;

            if (ggoleZdobyte - ggoleStracone != 0)
                gpunktyZaGole = ((double)ggoleZdobyte - (double)ggoleStracone) / 2;
            else
                gpunktyZaGole = 0;

            double z = (double)punktyZaGole / 100;
            double zz = (double)gpunktyZaGole / 100;
            double wynik = (double)punktyZaMecze + (double)punktyZaMecze * z + (double)gpunktyZaMecze + (double)gpunktyZaMecze * zz;
            //double wynik = ((double)punktyZaMecze + (double)gpunktyZaMecze) + (((double)punktyZaMecze + (double)gpunktyZaMecze)*z);
            return wynik;
        }
        public String punktyglowne()
        {
            int x = remisy + wygrane * 3;
            return x.ToString();
        }
        public String gpunktyglowne()
        {
            int x = gremisy + gwygrane * 3;
            return x.ToString();
        }
        public void ladujgospodarza()
        {
            WebClient client = new WebClient();
            Byte[] pageData = client.DownloadData(link + "table-home/");
            string pageHtml = Encoding.UTF8.GetString(pageData);
            Match match = Regex.Match(pageHtml, "<td class=\"cty\">" + nazwa + "</td> <td>(.*?)<\\/td> <td>(.*?)<\\/td> <td>(.*?)<\\/td> <td>(.*?)<\\/td> <td>(.*?)<\\/td> <td>(.*?)<\\/td> <td>(.*?)<\\/td>");
            giloscMeczy = int.Parse(match.Groups[1].Value);
            gwygrane = int.Parse(match.Groups[2].Value);
            gremisy = int.Parse(match.Groups[3].Value);
            gprzegrane = int.Parse(match.Groups[4].Value);
            ggoleZdobyte = int.Parse(match.Groups[5].Value);
            ggoleStracone = int.Parse(match.Groups[6].Value);
        }
        public void ladujgoscia()
        {
            WebClient client = new WebClient();
            Byte[] pageData = client.DownloadData(link + "table-away/");
            string pageHtml = Encoding.UTF8.GetString(pageData);
            Match match = Regex.Match(pageHtml, "<td class=\"cty\">" + nazwa + "</td> <td>(.*?)<\\/td> <td>(.*?)<\\/td> <td>(.*?)<\\/td> <td>(.*?)<\\/td> <td>(.*?)<\\/td> <td>(.*?)<\\/td> <td>(.*?)<\\/td>");
            giloscMeczy = int.Parse(match.Groups[1].Value);
            gwygrane = int.Parse(match.Groups[2].Value);
            gremisy = int.Parse(match.Groups[3].Value);
            gprzegrane = int.Parse(match.Groups[4].Value);
            ggoleZdobyte = int.Parse(match.Groups[5].Value);
            ggoleStracone = int.Parse(match.Groups[6].Value);
        }

        public double atak()
        {
            double punktyZaGole;
           
            if (this.goleZdobyte + this.ggoleZdobyte !=0)
            {
                punktyZaGole = ((double)this.goleZdobyte + (double)this.ggoleZdobyte) / ((double)this.iloscMeczy + (double)this.giloscMeczy);
            }
            else
            {
                punktyZaGole = 0;
            }

            return punktyZaGole;
        }
        public double obrona()
        {
            double punktyZaGoleStracone;

            if (this.goleZdobyte + this.ggoleZdobyte != 0)
            {
                punktyZaGoleStracone = ((double)this.goleStracone + (double)this.ggoleStracone) / ((double)this.iloscMeczy + (double)this.giloscMeczy);
            }
            else
            {
                punktyZaGoleStracone = 0;
            }

            return punktyZaGoleStracone;
        }

    }
}
