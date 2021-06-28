using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pictures
{
    public partial class Form1 : Form
    {
        MyList L1, L2, SelList;
        int x0, y0;
        public Form1()
        {
            InitializeComponent();
            L1 = new MyList(10, 10, 100, 15, this);
            L1.ReadFile();

            L2 = new MyList(400, 10, 50, 10, this);
            L2.ReadFile();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = CreateGraphics();
            L1.Draw(g);
            L2.Draw(g);
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (L1.isOpnen(e.X, e.Y))
                this.Invalidate();
            else if (L2.isOpnen(e.X, e.Y))
                this.Invalidate();

        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            SelList = null;
            x0 = y0 = 0;
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (L1.isIside(e.X, e.Y))
            {
                SelList = L1;
                x0 = e.X; y0 = e.Y;
            }
            else if (L2.isIside(e.X, e.Y))
            {
                SelList = L2;
                x0 = e.X; y0 = e.Y;
            }
            else
            {
                SelList = null;
                x0 = y0 = 0;
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (SelList != null)
            {
                SelList.Move(e.Y - y0);
                x0 = e.X; y0 = e.Y;
                this.Invalidate();
            }
                
            
            
        }
    }
}
