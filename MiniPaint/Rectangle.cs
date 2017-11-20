using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniPaint
{
    class Rectangle:FillableShape
    {
        public int width { get; set; }
        public int height { get; set; }
        public Rectangle()
        {
            Name = "Rectangle";
            this.StartPoint = new Point(300, 300);
            this.width = 30;
            this.height = 40;
            FillColor = Color.Red;
        }
        
        public override void Draw(Graphics g)
        {
            if(Selected)
            {
                g.FillRectangle(Brushes.Yellow , StartPoint.X - 2, StartPoint.Y - 2, width + 4, height + 4);
            }
            IsDraw = true;
            if (FillStyle.ToString() == "None")
            {
                Pen pen = new Pen(PenColor, PenWidth);
                g.DrawRectangle(pen, StartPoint.X, StartPoint.Y, width, height);
            }
            else
            {
                SolidBrush br = new SolidBrush(FillColor);
                g.FillRectangle(br, StartPoint.X, StartPoint.Y, width, height);
            }                
        }

        public override string txtSerialize()
        {
            return string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8}", Name, StartPoint.X,StartPoint.Y, width, height, FillColor.ToKnownColor(),FillStyle, PenColor.ToKnownColor(),PenWidth);       
            
        }

        public override void txtDeserialize(string s)
        {
            string[] props = s.Split(',');
            this.Name = props[0];
            Point p = new Point(int.Parse(props[1]), int.Parse(props[2]));
            StartPoint = p;
            width = int.Parse(props[3]);
            height =int.Parse( props[4]);
            FillColor = Color.FromName(props[5]);               //changing string to color

            FillStyle = (EnumFillStyle)Enum.Parse(typeof(EnumFillStyle), props[6]);         //converting string to enum
            PenColor = Color.FromName(props[7]);
            PenWidth = int.Parse(props[8]);                                     
        }

        public override double DistanceTo(int x, int y)
        {
            return Math.Sqrt(Math.Pow(x-StartPoint.X,2)+Math.Pow(y-StartPoint.Y,2));
        }
    }
}
