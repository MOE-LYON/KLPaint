using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLPaint.Shapes
{
    [Serializable]
    public class Polygon : Shape
    {
        private LinkedList<PointF> points;

        public Polygon(Shape shape, LinkedList<PointF> points) : base(shape)
        {
            this.points = points;
        }

        public Polygon(Point startPoint, LinkedList<PointF> points) : base(startPoint)
        {
            this.points = points;
        }

        public override void Draw(Graphics graphics,Point offset = default(Point))
        {
            Pen pen = GetPen();
            
            graphics.DrawPolygon(pen, points.ToArray());
        }
    }
}
