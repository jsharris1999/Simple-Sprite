using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public class Sprite
    {
        private Sprite parent = null;

        //instance variable
        private float x = 0;

        public float TargetX
        {
            get { return x; }
            set { x = value; }
        }

        private float y = 0;

        public float TargetY
        {
            get { return y; }
            set { y = value; }
        }

        private float scale = 1;

        public float Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        private float rotation = 0;

        /// <summary>
        /// The rotation in degrees.
        /// </summary>
        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }


        public List<Sprite> children = new List<Sprite>();


        public void Kill()
        {
            parent.children.Remove(this);
        }

        //methods
        public void render(Graphics g)
        {
            Matrix original = g.Transform.Clone();
            g.TranslateTransform(x, y);
            g.ScaleTransform(scale, scale);
            g.RotateTransform(rotation);
            paint(g);
            foreach (Sprite s in children)
            {
                s.render(g);
            }
            g.Transform = original;
        }

        public void update()
        {
            act();
            foreach (Sprite s in children)
            {
                s.update();
            }
        }

        public virtual void paint(Graphics g)
        {

        }

        public virtual void act()
        {

        }

        public void add(Sprite s)
        {
            s.parent = this;
            children.Add(s);
        }

        public class Picture : Sprite
        {
            Image mutalisk = Image.FromFile("Mutalisk.png", true);
            public int X = 0;
            public int Y = 0;
            public int velocity = 5;
            public int s = 0;
            public int width;
            public int height;

            public Picture(int h, int w) : base()
            {
                Random rand = new Random();
                width = w;
                height = h;
            }

            public override void act()
            {
                if (X + velocity < TargetX) X += velocity;
                else if (X - velocity > TargetX) X -= velocity;
                else X = velocity;
                if (Y + velocity < TargetX) Y += velocity;
                else if (Y - velocity > TargetX) Y -= velocity;
                else Y = velocity;
            }

            public override void paint(Graphics g)
            {
                g.DrawString("Hey", new Font("Comic Sans MS", 10), Brushes.Crimson, TargetX, TargetY);
                g.DrawImage(mutalisk, X, Y);
                
            }

        }
    }
}