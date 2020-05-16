using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLPaint.Shapes
{
    /// <summary>
    /// 图形基类
    /// </summary>
    public abstract class Shape
    {
        protected Point startPoint;

        protected Size size;

        protected Color frontColor;

        protected Color backColor;

        protected static Boolean isFill = true;

        public abstract void Draw(Graphics graphics);
        
    }
}
