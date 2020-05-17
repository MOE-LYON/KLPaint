using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLPaint.Shapes
{
    [Serializable]
    /// <summary>
    /// 椭圆
    /// </summary>
    public class Ellipse : Shape
    {
        public Ellipse(Point startPoint) : base(startPoint)
        {
        }

        public Ellipse(Shape shape) : base(shape)
        {
        }

        public override void Draw(Graphics graphics,Point offset=default(Point))
        {
            Pen pen = GetPen();
            //graphics.DrawEllipse(pen, startPoint.X, startPoint.X, Math.Abs(size.Width), Math.Abs(size.Height));
            Point start = ShapeHelper.ConvertToPoints(startPoint, EndPoint)[0];
            //Debug.WriteLine(size);
            graphics.DrawEllipse(pen, start.X, start.Y, Math.Abs(size.Width), Math.Abs(size.Height));
            if (IsFill)
            {
                using(Brush brush =new SolidBrush(this.BackColor))
                {
                    graphics.FillEllipse(brush, start.X, start.Y, Math.Abs(size.Width), Math.Abs(size.Height));
                }
            }
        }
    }
}
