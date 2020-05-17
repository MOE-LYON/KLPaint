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
    /// 直线
    /// </summary>
    public class Beeline : Shape
    {
        public Beeline(Point startPoint) : base(startPoint)
        {
        }
        

        public Beeline(Shape shape) : base(shape)
        {
        }

        public override void Draw(Graphics graphic,Point offset=default(Point))
        {
            Pen pen = GetPen();

            graphic.DrawLine(pen, startPoint, EndPoint);
        }
    }
}
