using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

enum EnumFillStyle
{
    None,Solid
}
namespace MiniPaint
{
    abstract class FillableShape:Shape
    {
        public EnumFillStyle FillStyle { get; set; }
        public Color FillColor { get; set; }

        
        public FillableShape()
        {
            FillColor = Color.Red;
            FillStyle = EnumFillStyle.None;             //NOTICE How to assign an enum
        }


    }
}
