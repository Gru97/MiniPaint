using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniPaint
{
    public partial class Form1 : Form
    {

        Graphics g;
        string FileAddress = @"..\..\Test\test.txt";
        public List<Drawable> DrawingList;
        public Form1()
        {
            InitializeComponent();
            g = panel1.CreateGraphics();
            DrawingList = new List<Drawable>();
            comboBox1.DisplayMember = "Name";
        }

        public void RefreshCanvas()
        {
            g.Clear(Color.White);
            foreach (var item in DrawingList)
            {
                item.Draw(g);
            }   
        }

        public Drawable SelectShape(int x, int y)
        {
            Drawable near = null;
            double minDist = double.MaxValue;
            foreach (var item in DrawingList)
            {
                item.Selected = false;
                double distance = item.DistanceTo(x, y);
                if (distance < minDist)
                {
                    near = item;
                    minDist = distance;
                }
            }
            near.Selected = true;
            propertyGrid1.SelectedObject = near;        //syncing
            comboBox1.SelectedItem=near;
            return near;
        }

        private void btnRectangle_Click(object sender, EventArgs e)
        {
            Rectangle r = new Rectangle();
            propertyGrid1.SelectedObject = r;
            r.Draw(g);
            DrawingList.Add(r);
            comboBox1.Items.Add(r);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            RefreshCanvas();
        }

        private void btnCircle_Click(object sender, EventArgs e)
        {
            Circle circle = new Circle();
            propertyGrid1.SelectedObject = circle;
            circle.Draw(g);
            DrawingList.Add(circle);
            comboBox1.Items.Add(circle);

        }

        private void btnLine_Click(object sender, EventArgs e)
        {  
            Line line=new Line(); 
            propertyGrid1.SelectedObject= line;
            line.Draw(g);
            DrawingList.Add(line);
            comboBox1.Items.Add(line);

        }

        private void btnPolygon_Click(object sender, EventArgs e)
        {
            
            Polygon p = new Polygon();
            propertyGrid1.SelectedObject = p;
            p.Draw(g);
            DrawingList.Add(p);
            comboBox1.Items.Add(p);
        }

        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            RefreshCanvas();
        }

        private void btnText_Click(object sender, EventArgs e)
        {
            
            Text t = new Text();
            t.Name = "Text";
            propertyGrid1.SelectedObject = t;
            t.IsDraw = true;
            t.Draw(g);
            DrawingList.Add(t);
            comboBox1.Items.Add(t);

        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            propertyGrid1.SelectedObject = comboBox1.SelectedItem;

        }

        private void btnPolyline_Click(object sender, EventArgs e)
        {
            Polyline p = new Polyline();
            propertyGrid1.SelectedObject = p;
            p.Draw(g);
            DrawingList.Add(p);
            comboBox1.Items.Add(p);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (StreamWriter sw = new StreamWriter(FileAddress))
            {
                foreach (var item in DrawingList)
                {

                    string s = item.txtSerialize();           
                    sw.WriteLine(s);
                    
                }
            } 
                
       }
        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (StreamReader sr=new StreamReader(FileAddress))
            {
                while (!sr.EndOfStream)
                {
                    string s = sr.ReadLine();
                    
                    if (s.Contains("Rectangle"))                //A better way?????
                    {
                        Rectangle r = new Rectangle();
                        r.txtDeserialize(s);
                        DrawingList.Add(r);

                    }
                    if (s.Contains("Circle"))                //A better way?????
                    {
                        Circle c = new Circle();
                        c.txtDeserialize(s);
                        DrawingList.Add(c);
                    }
                    if (s.Contains("Line"))                //A better way?????
                    {
                        Line l = new Line();
                        l.txtDeserialize(s);
                        DrawingList.Add(l);

                    }
                    if (s.Contains("Polygon"))                //A better way?????
                    {
                        Polygon p = new Polygon();
                        p.txtDeserialize(s);
                        DrawingList.Add(p);

                    }
                    if (s.Contains("Polyline"))                //A better way?????
                    {
                        Polyline p = new Polyline();
                        p.txtDeserialize(s);
                        DrawingList.Add(p);

                    }
                    if (s.Contains("Text"))                //A better way?????
                    {
                        Text t = new Text();
                        t.txtDeserialize(s);
                        DrawingList.Add(t);

                    }
                    Refresh();
                }
            }
        }

        private void clearCanvusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            DrawingList.Clear();
            comboBox1.Items.Clear();
            propertyGrid1.ResetSelectedProperty();
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            
            propertyGrid1.SelectedObject = SelectShape(e.X,e.Y);
            RefreshCanvas();

        }

        Drawable d = null;
        int ClickedPointX;
        int ClickedPointY;
        Point iPoint, ePoint;
        Point[] iPointlist;         //initial points
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            d = SelectShape(e.X, e.Y);
            ClickedPointX = e.X;
            ClickedPointY = e.Y;
            iPoint = d.StartPoint;                  //ipoint is to store initial value of startPoint
            if (d is Line)
                ePoint = ((Line)d).EndPoint;                    //we need to refresh x,y of the end point at every move based on the first value of it.
            else if(d is Polygon)
            {
                int iPointlistLength = ((Polygon)d).pointList.Length;
                iPointlist = new Point[iPointlistLength];
                for (int i = 0; i < iPointlistLength; i++)
                {
                    iPointlist[i] = ((Polygon)d).pointList[i];
                }
            }
            else if (d is Polyline)
            {
                int iPointlistLength = ((Polyline)d).pointList.Length;
                iPointlist = new Point[iPointlistLength];
                for (int i = 0; i < iPointlistLength; i++)
                {
                    iPointlist[i] = ((Polyline)d).pointList[i];
                }
            }
            RefreshCanvas();                   
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (d != null)
            {
                int dx = e.X - ClickedPointX;
                int dy = e.Y - ClickedPointY;
                d.StartPoint = new Point(iPoint.X + dx, iPoint.Y + dy);
                if (d is Line)
                {        
                    ((Line)d).EndPoint = new Point(ePoint.X + dx, ePoint.Y + dy);
                }
                if (d is Polygon)
                {                  
                    for (int i = 0; i < ((Polygon)d).pointList.Length; i++)
                    {
                        ((Polygon)d).pointList[i].X = iPointlist[i].X + dx;
                        ((Polygon)d).pointList[i].Y=iPointlist[i].Y + dy;
                    }
                }

                if (d is Polyline)
                {
                    for (int i = 0; i < ((Polyline)d).pointList.Length; i++)
                    {
                        ((Polyline)d).pointList[i].X = iPointlist[i].X + dx;
                        ((Polyline)d).pointList[i].Y = iPointlist[i].Y + dy;

                    }
                }

                RefreshCanvas();
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            d.Selected = false;
            if (d!=  null)
                d = null;
            
            RefreshCanvas();
        }
    }
}
