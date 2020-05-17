using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLPaint.Shapes
{
    [Serializable]
    /// <summary>
    /// 长方形
    /// </summary>
    public class Rectangle : Shape
    {
        public Rectangle(Point startPoint) : base(startPoint)
        {
        }

        public Rectangle(Shape shape) : base(shape)
        {
        }

        public override void Draw(Graphics graphics,Point offset= default(Point))
        {
            Pen pen = GetPen();
            Point start = ShapeHelper.ConvertToPoints(startPoint, EndPoint)[0];
            //Debug.WriteLine(size);
            graphics.DrawRectangle(pen, start.X,start.Y, Math.Abs(size.Width), Math.Abs(size.Height));

            if (IsFill)
            {
                using(Brush brush =new SolidBrush(this.BackColor))
                {
                    graphics.FillRectangle(brush, start.X, start.Y, Math.Abs(size.Width), Math.Abs(size.Height));
                }
                
            }
        }
    }
}
