using System;
using System.Windows.Forms;
using System.Drawing;

namespace Reversi
{
    class Reversi : Form
    {
        //declaraties
        int columns, rows;
        Button helpButton = new Button();
        Button nieuwSpelButton = new Button();
        Label aanZetLabel = new Label();
        Panel bord = new Panel();
        TextBox rijTxtBox = new TextBox();
        TextBox kolomTxtBox = new TextBox();

        public Reversi() //constructor
        {
            this.Size = new Size(600, 700);
            this.Text = "Reversi";
            this.BackColor = Color.DarkGray;

            //opmaak knoppen
            helpButton.Location = new Point(50, 20);
            helpButton.Size = new Size(60, 35);
            helpButton.Text = "help";
            helpButton.BackColor = Color.AliceBlue;

            nieuwSpelButton.Location = new Point(125, 20);
            nieuwSpelButton.Size = new Size(80, 35);
            nieuwSpelButton.Text = "nieuw spel";
            nieuwSpelButton.BackColor = Color.AliceBlue;

            //txtboxen

            aanZetLabel.Location = new Point(50, 80);
            aanZetLabel.Text = "aan zet: xxx ";  //font moet groter!


            bord.Size = new Size(501, 501); //+1 om de laatste zwarte lijn van bord ook te weergeven
            bord.Location = new Point(50, 150);
            bord.BackColor = Color.GhostWhite;

            //beginwaarden
            columns = 10; //min 3, max 10? waarden geven, zie NieuwSpel()
            rows = 10;

            //eventhandlers
            bord.Paint += TekenForm;

            //toevoegen
            
            Controls.Add(bord);
            Controls.Add(nieuwSpelButton);
            Controls.Add(helpButton);
            Controls.Add(aanZetLabel);
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