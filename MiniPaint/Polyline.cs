using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniPaint
{
    class Polyline : Shape
    {
        public string Points { get; set; }
        [Browsable(false)]
        public Point[] pointList;
        public Polyline()
        {
            Points = "200,200 400,250 260,203 220,300";
            Name = "Polyline";
            stringToPoint();

        }
        public override void Draw(Graphics g)
        {
            IsDraw = true;
            if (Selected)
            {
                g.DrawLines(new Pen(Color.Yellow, PenWidth), pointList);
            }
            else
            {
                StartPoint = pointList[0];
                Pen pen = new Pen(PenColor, PenWidth);
                g.DrawLines(pen, pointList);
            }
            

        }

        void stringToPoint()
        {
            Points = Points.Trim();
            string[] pairs = Points.Split(' ');

            pointList = new Point[pairs.Length];
            for (int i = 0; i < pairs.Length; i++)
            {
                string[] xy = pairs[i].Split(',');
                int x = int.Parse(xy[0]);
                int y = int.Parse(xy[1]);
                Point point = new Point(x, y);
                pointList[i] = point;
            }
        }

        public override string txtSerialize()
        {
            return string.Format("{0}:{1}:{2}:{3}:{4}:{5}", Name, StartPoint.X, StartPoint.Y, PenColor.ToKnownColor(), PenWidth, Points);

        }

        public override void txtDeserialize(string s)
        {
            string[] props = s.Split(':');
            this.Name = props[0];
            Point p = new Point(int.Parse(props[1]), int.Parse(props[2]));
            StartPoint = p;
            PenColor = Color.FromName(props[3]);
            PenWidth = int.Parse(props[4]);
            Points = props[5];
        }

        public override double DistanceTo(int x, int y)
        {
            int a = (pointList.Length);
            double[] DistanceToLine = new double[a];
            for (int i = 0; i < a; i++)
            {
                double x1 = pointList[i].X;
                double x2 = pointList[(i + 1) % a].X;
                double y1 = pointList[i].Y;
                double y2 = pointList[(i + 1) % a].Y;

                double numerator = Math.Abs(((y2 - y1) * x) - ((x2 - x1) * y) + (x2 * y1) - (y2 * x1));
                double denominator = Math.Sqrt(Math.Pow(y2 - y1, 2) + Math.Pow(x2 - x1, 2));
                DistanceToLine[i] = (numerator / denominator);
            }
            double min = double.MaxValue;
            for (int i = 0; i < a; i++)
            {
                if (DistanceToLine[i] < min)
                    min = DistanceToLine[i];
            }
            return min;
        }
    }
}
