using System;
using System.Drawing;
using System.Windows.Forms;


namespace Mandelbrot
{

    class Mandel : Form
    {
        
        TextBox txtMiddenX = new TextBox();
        TextBox txtMiddenY = new TextBox();
        TextBox txtSchaal = new TextBox();
        TextBox txtMax = new TextBox();

        Label midXLabel = new Label();
        Label midYLabel = new Label();
        Label schaalLabel = new Label();
        Label maxLabel = new Label();
        Button okButton = new Button();

        //Panel plaatje = new Panel();



        int maxiteratie;
        double xmidden, ymidden;


        public Mandel()
        {
            this.Text = "Mandelbrot";
            this.Size = new Size(700, 700);
            this.BackColor = Color.AliceBlue;

            //Opmaak OK knop
            int txtHoogte = 20;  //wat is deze variabele ook alweer
            okButton.Location = new Point(600, 100);
            okButton.Size = new Size(30, txtHoogte);
            okButton.Text = "OK";

            //Opmaak Midden X
            midXLabel.Location = new Point(10, 30);
            midXLabel.Size = new Size(70, 30);
            midXLabel.Text = "Midden X:";

            txtMiddenX.Location = new Point(80, 30);
            txtMiddenX.Size = new Size(200, 30);

            //Opmaal Midden Y
            midYLabel.Location = new Point(10, 70);
            midYLabel.Size = new Size(70, 30);
            midYLabel.Text = "Midden Y:";

            txtMiddenY.Location = new Point(80, 70);
            txtMiddenY.Size = new Size(200, 30);

            //Opmaak Schaal
            schaalLabel.Location = new Point(300, 30);
            schaalLabel.Size = new Size(70, 30);
            schaalLabel.Text = "Schaal:";

            txtSchaal.Location = new Point(370, 30);
            txtSchaal.Size = new Size(200, 30);

            //Opmaak max
            maxLabel.Location = new Point(300, 70);
            maxLabel.Size = new Size(70, 30);
            maxLabel.Text = "Max:";

            txtMax.Location = new Point(370, 70);
            txtMax.Size = new Size(200, 30);


            Controls.Add(midXLabel);
            Controls.Add(txtMiddenX);
            Controls.Add(midYLabel);
            Controls.Add(txtMiddenY);
            Controls.Add(schaalLabel);
            Controls.Add(txtSchaal);
            Controls.Add(maxLabel);
            Controls.Add(txtMax);
            Controls.Add(okButton);

            maxiteratie = 50;
            xmidden = 250.0;
            ymidden = 250.0;

            this.Paint += Tekenmap;
            okButton.Click += KlikOK;

            //eventhandlers


        }

        public void KlikOK(object o, EventArgs e)
        {


            try
            {
                    //zoom
                    xmidden = double.Parse(txtMiddenX.Text);
                    ymidden = double.Parse(txtMiddenY.Text);
                    maxiteratie = int.Parse(txtMax.Text);
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            this.Invalidate();
        }


        public void Tekenmap(Object obj, PaintEventArgs pea)
        {
            for (double x = 0; x < 500; x++)
            {
                for (double y = 0; y < 500; y++)
                {
                    double a = 0;
                    double b = 0;
                    double aa = 0;
                    double bb = 0;

                    double xzoom = ((x - xmidden) / 100.0);
                    double yzoom = ((y - ymidden) / 100.0);
                    
                    for (int iteratie = 0;  iteratie <= maxiteratie; iteratie ++)
                    {
                        
                        aa = a * a - b * b + xzoom;
                        bb = 2 * a * b + yzoom;
                        

                        double pythagoras = a * a + b * b;
                        double afstand = Math.Sqrt(pythagoras);
                        //Console.WriteLine("aa " + aa + " bb " + bb + " a " + a + " b " + b + " afstand " + afstand);
                        a = aa;
                        b = bb;


                        if (afstand > 2)
                        {
                            if (iteratie % 2 == 0)
                                pea.Graphics.FillRectangle(Brushes.DarkRed, (int)x + 100, (int)y + 150, 1, 1);
                                
                            if (iteratie%2 != 0)
                                pea.Graphics.FillRectangle(Brushes.Black, (int)x + 100, (int)y + 150, 1, 1);
                            break;
                        }
                        

                        

                    }
                }
            }
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