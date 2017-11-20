using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniPaint
{
    public abstract class Drawable:ISerializer
    {
        public string Name { get; set; }
        public Point StartPoint { get; set; }
        public bool IsDraw { get; set; }
        public abstract void Draw(Graphics g);
        public abstract string txtSerialize();
        public abstract void txtDeserialize(string s);
        
        public abstract double DistanceTo(int x, int y);

        [Browsable(false)]
        public bool Selected { get; set; }
    }
}
