using System;
using System.Windows.Forms;
using System.Drawing;

namespace Reversi
{
    class Reversi : Form
    {
        public Reversi()
        {
            this.Size = new Size(500, 500);
            this.Text = "Reversi";
            this.BackColor = Color.DarkGray;


        }

    }
        

    class MainClass
    {
        public static void Main(string[] args)
        {
            Application.Run(new Reversi());
        }
    }
}
