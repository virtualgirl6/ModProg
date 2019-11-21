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
        }

        void MaakReactieknop()
        {
            int randomGrootte = randomgetal.Next(20, this.Width / 8);
            int randomBreedte = randomgetal.Next(0, this.Width - randomGrootte);
            int randomHoogte = randomgetal.Next(0, this.Height - randomGrootte);


            reactieknop.Location = new Point(randomBreedte, randomHoogte);
            reactieknop.Size = new Size(randomGrootte, randomGrootte);
            reactieknop.BackColor = Color.CornflowerBlue; //kleur??
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
            int Xafstand = Math.Abs(startknop.Location.X - reactieknop.Location.X);
            int Yafstand = Math.Abs(startknop.Location.Y - reactieknop.Location.Y);

            int W = reactieknop.Width;
            double D = Math.Sqrt(Xafstand * Xafstand + Yafstand * Yafstand);   // pythagoras
            double ID = Math.Log(D / W + 1, 2);                // ID = log2(1+D/W)
            
            

            Console.WriteLine("reactietijd: " + totaaltijd + "\t ID: " + ID + "\t D: " + D + "\t W: " + W);

        }



        void SchermStart(object obj, EventArgs e)
        {
            int breedte = this.Width; int hoogte = this.Height;
            int startgrootte = 50;

            startknop.Size = new Size(startgrootte, startgrootte);
            startknop.Location = new Point(breedte / 2 - (startgrootte / 2), hoogte / 2 - (startgrootte / 2));
            startknop.BackColor = Color.Red;
            startknop.Text = "Start";

            this.Controls.Add(startknop);
            this.Controls.Add(reactieknop);
            reactieknop.Hide();

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
