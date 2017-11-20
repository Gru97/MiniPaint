using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniPaint
{
    class Polygon : FillableShape
    {
        public string Points { get; set; }
        [Browsable(false)]
        public Point[] pointList;
        public Polygon()
        {
            Points = "100,100 200,100 200,200 150,250 100,200";
            Name = "Polygon";
            stringToPoint();
        }
        public override void Draw(Graphics g)
        {
            IsDraw = true;
            if (Selected)
            {
                g.FillPolygon(Brushes.Yellow, pointList);
            }
            else
            {
                StartPoint = pointList[0];

                if (FillStyle.ToString() == "None")
                {
                    Pen pen = new Pen(PenColor, PenWidth);
                    g.DrawPolygon(pen, pointList);
                }
                else
                {
                    SolidBrush br = new SolidBrush(FillColor);
                    g.FillPolygon(br, pointList);
                }
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
            return string.Format("{0}:{1}:{2}:{3}:{4}:{5}:{6}:{7}", Name, StartPoint.X, StartPoint.Y, FillColor.ToKnownColor(), FillStyle, PenColor.ToKnownColor(), PenWidth,Points);

        }
        public override void txtDeserialize(string s)
        {
            string[] props = s.Split(':');
            this.Name = props[0];
            Point p = new Point(int.Parse(props[1]), int.Parse(props[2]));
            StartPoint = p;
            FillColor = Color.FromName(props[3]);               //changing string to color
            FillStyle = (EnumFillStyle)Enum.Parse(typeof(EnumFillStyle), props[4]);         //converting string to enum
            PenColor = Color.FromName(props[5]);
            PenWidth = int.Parse(props[6]);
            Points = props[7];
        }
        public override double DistanceTo(int x, int y)
        {
            int a = (pointList.Length);
            double[] DistanceToLine=new double[a];
            for (int i = 0; i <a ; i++)
            {
                double x1 = pointList[i].X;
                double x2 = pointList[(i + 1) % a].X;
                double y1 = pointList[i].Y;
                double y2 = pointList[(i + 1) % a].Y;
                
                double numerator = Math.Abs(((y2 - y1) * x) - ((x2 - x1) * y) + (x2 * y1) - (y2 * x1));
                double denominator = Math.Sqrt(Math.Pow(y2 - y1, 2) + Math.Pow(x2 - x1, 2));
                DistanceToLine[i]=(numerator / denominator);
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


