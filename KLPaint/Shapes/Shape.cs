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
    /// 图形基类
    /// </summary>
    public abstract class Shape
    {
        protected Point startPoint;
        public Point EndPoint { get; set; }
        [NonSerialized]
        protected Pen pen;
        protected Object locked = new Object();
        protected Pen GetPen()
        {
            if(pen == null)
            {
                lock (locked)
                {
                    if(pen == null)
                    {
                        pen = new Pen(this.FrontColor,this.PenWidth);
                        if (IsDash)
                        {
                            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDot;
                        }
                        
                    }
                }
            }
            return pen;
            
        }
        public float PenWidth { get; set; } = 1f;
        protected Shape(Point startPoint)
        {
            this.startPoint = startPoint;
        }

        protected Size size {

            get
            {
                int w = startPoint.X - EndPoint.X;
                int h = startPoint.Y - EndPoint.Y;

                return new Size(w, h);

            }
        }

        public Color FrontColor { get; set; }

        public Color BackColor { set; get; }
        public Boolean IsDash { get; set; } = false;
        public Boolean IsFill { get; set; } = false;

        public Shape(Shape shape):this(shape.startPoint)
        {
            PenWidth = shape.PenWidth;
            FrontColor = shape.FrontColor;
            BackColor = shape.BackColor;
            IsFill = shape.IsFill;
            IsDash = shape.IsFill;
        }

        
        public abstract void Draw(Graphics graphics,Point offset= default(Point));
        
    }

    public static class ShapeHelper
    {
        public static Point[] ConvertToPoints(Point a,Point b)
        {
            int x1 = Math.Min(a.X, b.X);
            int y1 = Math.Min(a.Y, b.Y);

            int x2 = Math.Max(a.X, b.X);
            int y2 = Math.Max(a.Y, b.Y);

            return new Point[]{ new Point(x1,y1),new Point(x2,y2)};
        }
    }

    public enum SupportShape
    {
        brushes,
        pen,
        line,
        rectang,
        ellipse
    }
}
