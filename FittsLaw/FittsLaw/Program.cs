using System;
using System.Drawing;
using System.Windows.Forms;

namespace Fitts
{
    class Fitts : Form
    {
        public Fitts()
        {

            this.Text = "Fitts' Law";
            this.BackColor = Color.White;
            this.WindowState = FormWindowState.Maximized;

            Button startknop = new Button();
            Button reactieknop = new Button();

            this.Controls.Add(startknop);
            this.Controls.Add(reactieknop);
        }

    }



    class MainClass
    {

        public static void Main()
        {

            Fitts scherm;
            scherm = new Fitts();
            Application.Run(scherm);
        }
    }
}
