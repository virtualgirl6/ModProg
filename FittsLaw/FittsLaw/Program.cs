using System;
using System.Drawing;
using System.Windows.Forms;

namespace FittsLaw
{
    class Fitts : Form
    {

        Button startKnop = new Button();
        Button reactieKnop = new Button();
        Random randomGetal = new Random();
        long begintijd = new long();
        long totaaltijd = new long();


        public Fitts()
        {
            this.Text = "Fitts' Law";
            this.BackColor = Color.White;
            this.WindowState = FormWindowState.Maximized;
            this.SizeChanged += SchermStart;

            this.Controls.Add(startKnop);
            startKnop.Show();

            this.Controls.Add(reactieKnop);
            reactieKnop.Hide();
        }


        void SchermStart(object obj, EventArgs e)
        {
            //formaat van gemaximaliseerd scherm ophalen, om startKnop in het midden te kunnen plaatsen
            int breedte = this.Width;
            int hoogte = this.Height;   

            //aanmaken van de startknop
            int startGrootte = 50;
            startKnop.Size = new Size(startGrootte, startGrootte);
            startKnop.Location = new Point(breedte / 2 - (startGrootte / 2), hoogte / 2 - (startGrootte / 2));
            startKnop.BackColor = Color.DarkGreen;
            startKnop.Text = "Start";


            //"abboneren" op de clickevents 
            startKnop.Click += StartKnopKlik;
            reactieKnop.Click += ReactieKnopKlik;
        }


        void MaakReactieknop()
        {
            /* Met behulp van de randomfunctie wordt het formaat en de locatie van de reactieknop telkens
              opnieuw bepaald als deze methode gebruikt wordt. Om (te) moeilijke testen te voorkomen en absurde
              groottes te voorkokmen, is de randomfunctie die de grootte van de knop aanmaakt ingeperkt. 
              Daarbij zorgen we dat de knop niet buiten het scherm kan vallen, door de grootte van de knop van
              de hoogte/breedte van het scherm af te trekken. */

            
            int randomGrootte = randomGetal.Next(20, this.Width / 8);
            int randomBreedte = randomGetal.Next(0, this.Width - randomGrootte);
            int randomHoogte = randomGetal.Next(0, this.Height - randomGrootte);


            reactieKnop.Location = new Point(randomBreedte, randomHoogte);
            reactieKnop.Size = new Size(randomGrootte, randomGrootte);
            reactieKnop.BackColor = Color.Red; 
        }


        void StartKnopKlik(Object obj, EventArgs e)
        {
            startKnop.Hide();
            MaakReactieknop();
            reactieKnop.Show();
            StartTijd();
        }


        void ReactieKnopKlik(Object obj, EventArgs e)
        {
            EindTijd();
            reactieKnop.Hide();
            startKnop.Show();

            Bereken();
        }


        void StartTijd()
        {
            begintijd = DateTime.Now.Ticks;
        }


        private long EindTijd()
        {
            totaaltijd = DateTime.Now.Ticks - begintijd;
            return totaaltijd;
        }


        void Bereken()
        {
            /* 
            In deze methode genereren we de data waarmee we Fitts' Law kunnen aantonen (of weerleggen).
            Aan de hand van de afstand tussen de start- en reactieknop (D) en de breedte van de reactieknop (W),
            berekenen we de Difficulty Index (DI). Alle data (reactietijd T, DI, D en W) worden naar de Console geschreven,
            zodat we deze naar Excel kunnen exporteren. */

            int W = reactieKnop.Width;

            // adhv pythagoras de afstand tussen de knoppen berekenen
            int xAfstand = startKnop.Location.X - reactieKnop.Location.X;     
            int yAfstand = startKnop.Location.Y - reactieKnop.Location.Y;
            double D = Math.Sqrt(xAfstand * xAfstand + yAfstand * yAfstand);

            double DI = Math.Log(D / W + 1, 2);                            

            Console.WriteLine("T: " + totaaltijd + "\t DI: " + DI + "\t D: " + D + "\t W: " + W);
        }
    }


    class MainClass
    {
        public static void Main()
        {
            Application.Run(new Fitts());
        }
    }
}