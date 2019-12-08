using System;
using System.Windows.Forms;
using System.Drawing;

namespace Reversi
{
    class Reversi : Form
    {
        //declaraties
        int columns, rows;

        public Reversi() //constructor
        {
            this.Size = new Size(500, 500);
            this.Text = "Reversi";
            this.BackColor = Color.DarkGray;

            //knopjes 


            //beginwaarden
            columns = 6;
            rows = 6;

            //eventhandlers
            this.Paint += TekenForm;

        }


        void TekenForm(object o, PaintEventArgs pea)
        {
            // teken veld 
            Pen p = Pens.Black;
            for (int x = 0; x < rows; x++)
                for (int y = 0; y < columns; y++)
                    pea.Graphics.DrawRectangle(p, (50*x) +100, (50*y) + 100, 50, 50);
        }
    }

   

    class MainClass
    {
        public static void Main()
        {
            Application.Run(new Reversi());
        }
    }
}
