using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLPaint.Shapes
{
    public class ShapeBuilder : Shape
    {
        public ShapeBuilder(Point startPoint) : base(startPoint)
        {
        }

        public override void Draw(Graphics graphics, Point offset = default(Point))
        {
            throw new NotImplementedException();
        }
    }
}
