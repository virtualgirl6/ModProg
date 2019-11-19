using System;
using System.Drawing;
using System.Windows.Forms;

namespace Fitts
{
    class Fitts : Form
    {
        Button startknop = new Button();
        Button reactieknop = new Button();


        public Fitts()
        {

            this.Text = "Fitts' Law";
            this.BackColor = Color.White;
            this.WindowState = FormWindowState.Maximized;
            this.SizeChanged += new EventHandler(SchermEventHandler);


            
            this.Controls.Add(reactieknop);

    
        }

        


        void StartKnopClick(Object obj, EventArgs e)
        {
            startknop.Hide();
            reactieknop.Show();
        }


        void ReactieKnopClick(Object obj, EventArgs e)
        {
            reactieknop.Hide();
            startknop.Show();
        }



        void SchermEventHandler(object obj, EventArgs e)
        {
            int breedte = this.Width; int hoogte = this.Height;
            int startgrootte = 50;

            startknop.Size = new Size(startgrootte, startgrootte);
            startknop.Location = new Point(breedte / 2 - (startgrootte / 2), hoogte / 2 - (startgrootte / 2));
            //startknop text / kleur toevoegen

            startknop.Click += new EventHandler(StartKnopClick);
            reactieknop.Click += new EventHandler(ReactieKnopClick);


            this.Controls.Add(startknop);
            reactieknop.Hide();
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
