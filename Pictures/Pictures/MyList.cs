using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Pictures
{
    class MyList
    {
        int x, y;
        int w, h;
        int xSlider, ySlider;
        int xBar, yBar;
        int wSlider, hSlider;
        int wBar, hBar;
        int startSlider;

        bool open;

        Graphics g;
        private Bitmap MyImage;
        private PictureBox PictureBox1;
        private Form1 f;
        private List<string> photo;
        private string FileName = "Images.txt";
        int lastId = -1;

        public MyList(int _x, int _y, int _w, int _h, Form1 _f)
        {
            x = _x; y = _y;
            w = _w; h = _h;

            xSlider = x + (w * 3);
            ySlider = y + h;
            startSlider = ySlider;

            xBar = xSlider;
            yBar = ySlider;

            wSlider = 15;
            hSlider = 40;

            wBar = wSlider;
            hBar = h * 20;

            open = true;
            f = _f;

            InitializePictureBox();

        }

        internal void Draw(Graphics gr)
        {
            g = gr;

            TextFormatFlags flags = TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter;

            g.FillRectangle(Brushes.Gray, x, y, w, h);
            g.DrawRectangle(Pens.Black, x, y, w, h);

            TextRenderer.DrawText(g, "^", new Font("Arial", 14),
                    new Rectangle(x + w - 10, y + 1, 10, 10), Color.White, Color.Gray, flags);

            if (open)
            {
                g.DrawRectangle(Pens.Black, x, y + h, xBar - x, hBar);
                g.DrawRectangle(Pens.Black, xBar, yBar, wBar, hBar);
                g.FillRectangle(Brushes.Gray, xSlider, ySlider, wSlider, hSlider);        
            }
        }
        internal bool isOpnen(int xm, int ym)
        {
            if (xm > x && xm < x + w &&
               ym > y && ym < y + h)
            {
                
                if (open)
                {
                    PictureBox1.Hide();
                }
                else
                {
                    PictureBox1.Show();
                }
                open = !open;

                return true;
            }
            return false;
               
        }

        internal void Move(int dy)
        {
           if (ySlider + dy > yBar &&
                ySlider + dy < yBar + hBar - hSlider)
            {
                ySlider += dy;
                choicePicture();
            }
            
        }

        private void choicePicture()
        {
            int idPict = (ySlider - startSlider) / 20;

            if (idPict < photo.Count && idPict != lastId)
            {              
                lastId = idPict;
                loadPicture(photo[idPict]);
            }        
            
        }

        private void loadPicture(string pict)
        {
            if (MyImage != null)
            {
                MyImage.Dispose();
            }

            MyImage = new Bitmap(pict);
            PictureBox1.Image = (Image)MyImage;

        }

        internal bool isIside(int xm, int ym)
        {
           
            if (xm > xSlider && xm < xSlider + wBar &&
                ym > ySlider && ym < ySlider + hBar)
                return true;

            return false;
        }

        private void InitializePictureBox()
        {
            PictureBox1 = new PictureBox();

            this.PictureBox1.Location = new System.Drawing.Point(x + 20, y + h + 20);
            this.PictureBox1.Size = new System.Drawing.Size(xBar - x - 40, hBar - 40);

            this.PictureBox1.TabStop = false;
            this.PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            this.PictureBox1.BorderStyle = BorderStyle.Fixed3D;

            f.Controls.Add(PictureBox1);
        }

        internal void ReadFile()
        {
            photo = new List<string>();
            StreamReader reader = File.OpenText(FileName);
            string str = reader.ReadLine();
            while (str != null)
            {
                photo.Add(str);
                str = reader.ReadLine();
            }
            reader.Close();
        }
    }
}
