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
            this.Size = new Size(600, 700);
            this.Text = "Reversi";
            this.BackColor = Color.DarkGray;

            //knopjes enzo

            Panel bord = new Panel();
            bord.Size = new Size(501, 501); //+1 om de laatste zwarte lijn van bord ook te weergeven
            bord.Location = new Point(50, 150);


            //beginwaarden
            columns = 10; //min 3, max 10? waarden geven, zie NieuwSpel()
            rows = 10;

            //eventhandlers
            bord.Paint += TekenForm;

            //toevoegen
            Controls.Add(bord);
        }


        void TekenForm(object o, PaintEventArgs pea)
        {
            // teken veld 
            Pen p = Pens.Black;
            for (int x = 0; x < rows; x++)
                for (int y = 0; y < columns; y++)
                    pea.Graphics.DrawRectangle(p, 50*x, 50*y, 50, 50);
                    
        }

        void KlikVakje(object o, MouseEventArgs mea)
        {
            for (int x = 0; x < rows; x++)
                for (int y = 0; y < columns; y++)
                    ZetSteen(x, y);


        }

        void ZetSteen(int r, int c)
        {
            //if blauwbeurt = true
            //teken blauw rondje op 50x 50y
            //if roodbeurt = true
            // 
        }

        void LegaalCheck() //locatie als parameter?
        {


        }

        void Nieuwspel(object o, MouseEventArgs mea)
        {
            //geef waardes mee aan rows en columns. popup textbox?
            //bord moet dus ook kleiner worden in het begin. (6x6)
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