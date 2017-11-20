using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniPaint
{
    abstract class Shape:Drawable
    {
        public Color PenColor { get; set; }
        public float PenWidth { get; set; }
       
        public Shape()
        {
            PenColor = Color.Red;
            PenWidth = 3;
        }
    }
}
