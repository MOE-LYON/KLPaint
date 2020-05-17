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
    /// useless
    /// </summary>
    public class Brushes : Shape
    {
        private LinkedList<PointF> points;

        public Brushes(Shape shape, LinkedList<PointF> points) : base(shape)
        {
            this.points = points;
        }

        public Brushes(Point startPoint, LinkedList<PointF> points) : base(startPoint)
        {
            this.points = points;
        }

        public override void Draw(Graphics graphics, Point offset = default(Point))
        {
            Pen pen = GetPen();
            if (points.Count > 0)
            {
                points.Aggregate((pa, pb) =>
                {
                    graphics.DrawLine(pen, pa, pb);
                    return pb;
                });
            }
            // graphics.DrawPolygon(pen,)

        }
    }
}
