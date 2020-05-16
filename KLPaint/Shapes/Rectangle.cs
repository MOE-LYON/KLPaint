using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLPaint.Shapes
{
    /// <summary>
    /// 长方形
    /// </summary>
    public class Rectangle : Shape
    {
        static Rectangle()
        {
            isFill = true;
        }
        
        public override void Draw(Graphics graphics)
        {
            Pen pen = new Pen(this.frontColor);
            
            graphics.DrawRectangle(pen, startPoint.X, startPoint.Y, size.Width, size.Height);
        }
    }
}
