using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniPaint
{
    class Line:Shape
    {
        public Point EndPoint { get; set; }     
        public Line()
        {
            Name = "Line";
            Point p1 = new Point(1, 2);
            Point p2 = new Point(100, 200);
            StartPoint = p1;
            EndPoint = p2;     
        }
        public override void Draw(Graphics g)
        {
            if (Selected)
            {               
                Pen p = new Pen(Color.Yellow, PenWidth+2);           
                g.DrawLine(p, StartPoint.X, StartPoint.Y,EndPoint.X,EndPoint.Y);                                    
            }
            IsDraw = true;
            Pen pen = new Pen(PenColor, PenWidth);
            g.DrawLine(pen, StartPoint, EndPoint);           
        }

        public override string txtSerialize()
        {
            return string.Format("{0},{1},{2},{3},{4},{5},{6}", Name, StartPoint.X, StartPoint.Y, EndPoint.X, EndPoint.Y, PenColor.ToKnownColor(), PenWidth);

        }

        public override void txtDeserialize(string s)
        {
            string[] props = s.Split(',');
            this.Name = props[0];
            Point sp = new Point(int.Parse(props[1]), int.Parse(props[2]));
            StartPoint = sp;
            Point ep = new Point(int.Parse(props[3]), int.Parse(props[4]));
            StartPoint = ep;
            PenColor = Color.FromName(props[5]);               
            PenWidth =int.Parse(props[6]);        
        }

        public override double DistanceTo(int x, int y)
        {
            double x1 = StartPoint.X;
            double x2 = EndPoint.X;
            double y1 = StartPoint.Y;
            double y2 = EndPoint.Y;

            double numerator = Math.Abs(((y2 - y1) * x) - ((x2 - x1) * y) + (x2 * y1) - (y2 * x1));
            double denominator = Math.Sqrt(Math.Pow(y2 - y1, 2) + Math.Pow(x2 - x1, 2));
            return (numerator / denominator);
        }
    }
}
