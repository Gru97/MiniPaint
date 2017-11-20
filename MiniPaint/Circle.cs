using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniPaint
{
    class Circle:FillableShape
    {
        public float radius { get; set; }
        public Circle()
        {
            Name = "Circle";
            Point p = new Point(50, 50);
            this.StartPoint = p;
            this.radius = 50;                   //in circle height and width of an eclipse is identical
            FillColor = Color.Red;
        }
        public override void Draw(Graphics g)
        {
            if (Selected)
            {
                g.FillEllipse(Brushes.Yellow, StartPoint.X, StartPoint.Y,radius,radius);
            }
            IsDraw = true;
            if (FillStyle.ToString() == "None")
            {
                Pen pen = new Pen(PenColor, PenWidth);
                g.DrawEllipse(pen, StartPoint.X, StartPoint.Y, radius, radius);
            }
            else
            {
                SolidBrush br = new SolidBrush(FillColor);
                g.FillEllipse(br, StartPoint.X, StartPoint.Y, radius, radius);
            }
        }

        public override string txtSerialize()
        {
            return string.Format("{0},{1},{2},{3},{4},{5},{6},{7}", Name, StartPoint.X, StartPoint.Y, radius, FillColor.ToKnownColor(), FillStyle, PenColor.ToKnownColor(), PenWidth);
        }

        public override void txtDeserialize(string s)
        {
            string[] props = s.Split(',');
            this.Name = props[0];
            Point p = new Point(int.Parse(props[1]), int.Parse(props[2]));
            StartPoint = p;
            radius = int.Parse(props[3]);
            FillColor = Color.FromName(props[4]);               //changing string to color
            FillStyle = (EnumFillStyle)Enum.Parse(typeof(EnumFillStyle), props[5]);         //converting string to enum
            PenColor = Color.FromName(props[6]);
            PenWidth = int.Parse(props[7]);
        }

        public override double DistanceTo(int x, int y)
        {
            return Math.Sqrt(Math.Pow(StartPoint.X - x, 2) + Math.Pow(StartPoint.Y - y, 2));
        }
    }
}
