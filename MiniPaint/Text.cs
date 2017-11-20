using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniPaint
{
    class Text:Drawable
    {
        public Font font { get; set; }
        public Color FontColor { get; set; }
        public string text { get; set; }

        public  Text()
        {
            text = "Tested String";
            FontColor = Color.Red;
            font = new Font("Arials",12);
        }
        public override void Draw(Graphics g)
        {          
            SolidBrush br = new SolidBrush(FontColor);
            g.DrawString(text, font, br,StartPoint.X,StartPoint.Y);            
        }

        public override string txtSerialize()
        {
            return string.Format("{0},{1},{2},{3},{4},{5},{6}", Name,text, StartPoint.X,StartPoint.Y, FontColor.ToKnownColor(),font.Name,font.Size);                  
        }

        public override void txtDeserialize(string s)
        {
            string[] props = s.Trim().Split(',');
            Name = props[0];
            text = props[1];
            Point p = new Point(int.Parse(props[2]), int.Parse(props[3]));
            StartPoint = p;
            FontColor = Color.FromName(props[4]);               //changing string to color
            Font f = new Font(props[5], int.Parse(props[6]));
            font = f;
           
        }

        public override double DistanceTo(int x, int y)
        {
            return Math.Sqrt(Math.Pow(x - StartPoint.X, 2) + Math.Pow(y - StartPoint.Y, 2));
        }
    }
}
