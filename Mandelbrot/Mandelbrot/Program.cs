using System;
using System.Drawing;
using System.Windows.Forms;


namespace Mandelbrot
{

    class Mandel : Form
    {
        // 4 x textbox + tekst
        TextBox txtMiddenX = new TextBox();


        Button okButton = new Button();
       



        public Mandel()
        {
            this.Text = "Mandelbrot";
            this.Size = new Size(400, 400);
            this.BackColor = Color.AliceBlue;

            int txtHoogte = 20;
            okButton.Location = new Point(50, 50);
            okButton.Size = new Size(30, txtHoogte);
            okButton.Text = "OK";


            //eventhandlers


        }


    }


    class MainClass
    {
        public static void Main()
        {
            Application.Run(new Mandel());
        }
    }
}
