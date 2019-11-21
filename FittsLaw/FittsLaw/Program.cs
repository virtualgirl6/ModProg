using System;
using System.Drawing;
using System.Windows.Forms;

namespace Fitts
{
    class Fitts : Form
    {
        Button startknop = new Button();
        Button reactieknop = new Button();
        Random randomgetal = new Random();

        long begintijd = new long();
        long totaaltijd = new long();
        
        public Fitts()
        {
            this.Text = "Fitts' Law";
            this.BackColor = Color.White;
            
        
            this.WindowState = FormWindowState.Maximized;
            this.SizeChanged += SchermStart;


            this.Controls.Add(startknop);
            startknop.Show();

            this.Controls.Add(reactieknop);
            reactieknop.Hide();

        }


        void MaakReactieknop()
        {
            /* met behulp van de random functie wordt het formaat en de locatie van de reactieknop opnieuw bepaald
             * We hebben hoe groot en klein de knop kan worden ingeperkt. 
            Om te zorgen dat de knop niet buiten het scherm valt is trekken we de grootte van de knop af van de hoogte/breedte van het scherm */

            int randomGrootte = randomgetal.Next(20, this.Width / 8);
            int randomBreedte = randomgetal.Next(0, this.Width - randomGrootte);
            int randomHoogte = randomgetal.Next(0, this.Height - randomGrootte);


            reactieknop.Location = new Point(randomBreedte, randomHoogte);
            reactieknop.Size = new Size(randomGrootte, randomGrootte);
            reactieknop.BackColor = Color.Red; 
        }


        void StartKnopClick(Object obj, EventArgs e)
        {
            startknop.Hide();
            MaakReactieknop();
            reactieknop.Show();
            StartTijd();
        }


        void ReactieKnopClick(Object obj, EventArgs e)
        {
            EindTijd();
            reactieknop.Hide();
            startknop.Show();

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
            Aan de hand van de afstanden tussen de start- en reactieknop, en de breedte van de reactieknop,
            berekenen we de Difficulty Index (DI). Alle data (reactietijd, DI, D en W) worden naar de Console geschreven,
            zodat we deze naar Excel kunnen exporteren. 
             */

            int W = reactieknop.Width;

            int Xafstand = Math.Abs(startknop.Location.X - reactieknop.Location.X);     
            int Yafstand = Math.Abs(startknop.Location.Y - reactieknop.Location.Y);
            double D = Math.Sqrt(Xafstand * Xafstand + Yafstand * Yafstand);           // pythagoras om de afstand te berekenen

            double DI = Math.Log(D / W + 1, 2);                                        // DI = log2(1+D/W)
            
            Console.WriteLine("T: " + totaaltijd + "\t DI: " + DI + "\t D: " + D + "\t W: " + W);
        }



        void SchermStart(object obj, EventArgs e)
        {
            
            //aanmaken van de startknop

            int breedte = this.Width; int hoogte = this.Height;    //formaat van gemaximaliseerd scherm ophalen
            int startgrootte = 50;

            startknop.Size = new Size(startgrootte, startgrootte);
            startknop.Location = new Point(breedte / 2 - (startgrootte / 2), hoogte / 2 - (startgrootte / 2));
            startknop.BackColor = Color.DarkGreen;
            startknop.Text = "Start";
            

            //klikevents ????
            startknop.Click += StartKnopClick;
            reactieknop.Click += ReactieKnopClick;
        }
        
    }



    class MainClass
    {
        public static void Main()
        {
            Fitts scherm = new Fitts();
            Application.Run(scherm);
        }
    }
}
