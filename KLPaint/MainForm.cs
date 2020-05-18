using KLPaint.Shapes;
using KLPaint.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Brushes = KLPaint.Shapes.Brushes;
using Rectangle = KLPaint.Shapes.Rectangle;

namespace KLPaint
{
    public partial class MainForm : Form
    {

        #region member data

        private LinkedList<Shape> shapes;
        private Boolean isDrawing;
        private Bitmap cache_bmp;
        private Graphics graphics;
        private Graphics cache_graph;


        private Color frontColor;
        private Color backColor;
        private Point startPoint;
        private float penWidth =1f;

        private LinkedList<PointF> points;
        private SupportShape shape_now= SupportShape.brushes;
        private Shape currentShape;
        #endregion

        public MainForm()
        {
            InitializeComponent();
        }

        #region draw event
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {

            if(cache_bmp != null)
            {
                Point offset = Point.Empty;

                // how to cauclate offset 

                e.Graphics.DrawImage(cache_bmp, offset);
            }
            else if (shapes != null && shapes.Count>0)
            {
                foreach (var item in shapes)
                {
                    item.Draw(e.Graphics);
                }
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            isDrawing = true;
            startPoint = e.Location;

            graphics = this.drawingBoard.CreateGraphics();
            cache_bmp = new Bitmap(drawingBoard.Width, drawingBoard.Height);
            cache_graph = Graphics.FromImage(cache_bmp);

            Shape temp = new ShapeBuilder(startPoint)
            {
                FrontColor = frontColor,
                PenWidth = penWidth,
                BackColor = backColor,
                IsFill = draw_isfill.Checked,
                IsDash = new Random().Next(2) > 1
            };


            switch (shape_now)
            {
                case SupportShape.brushes:
                    points = new LinkedList<PointF>();
                    points.AddLast(startPoint);
                    currentShape = new Brushes(temp, points);
                    break;
                case SupportShape.pen:
                    points = new LinkedList<PointF>();
                    points.AddLast(startPoint);
                    currentShape = new Polygon(temp, points);
                    break;
                case SupportShape.line:
                    currentShape = new Beeline(temp);
                    break;
                case SupportShape.rectang:
                    currentShape = new Rectangle(temp);
                    break;
                case SupportShape.ellipse:
                    currentShape = new Ellipse(temp);

                    break;
                default:
                    break;
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isDrawing)
            {
                return;
            }
            //graphics.Clear(this.drawingBoard.BackColor);



            switch (shape_now)
            {
                case SupportShape.brushes:
                case SupportShape.pen:
                    points.AddLast(e.Location);
                    break;
                case SupportShape.line:
                    currentShape.EndPoint = e.Location;
                    break;
                case SupportShape.rectang:
                    currentShape.EndPoint = e.Location;
                    break;
                case SupportShape.ellipse:
                    currentShape.EndPoint = e.Location;
                    break;
                default:
                    break;
            }
            
            cache_graph.Clear(drawingBoard.BackColor);
            
            foreach (var item in shapes)
            {
                item.Draw(cache_graph);
            }
            currentShape.Draw(cache_graph);
            graphics.DrawImage(cache_bmp, new Point());


        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isDrawing = false;
            shapes.AddLast(currentShape);
            switch (shape_now)
            {
                case SupportShape.brushes:
                case SupportShape.pen:
                    points = null;
                    break;
                case SupportShape.line:
                    
                    break;
                case SupportShape.rectang:
                    
                    break;
                default:
                    break;
            }

            //drawingBoard.Invalidate();
            graphics.Dispose();
            cache_graph.Dispose();

        }
        #endregion


        #region menu event
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            draw_pen.Checked = false;
            draw_bucket.Checked = false;
            draw_cricle.Checked = false;
            draw_eraser.Checked = false;
            draw_line.Checked = false;
            draw_rectangle.Checked = false;
            draw_word.Checked = false;
            draw_brush.Checked = false;
            shape_now = SupportShape.rectang;
            var btn = ((ToolStripButton)sender);
            btn.Checked = true;
            switch (btn.Name)
            {
                case "draw_pen":
                    shape_now = SupportShape.pen;
                    break;
                case "draw_cricle":
                    shape_now = SupportShape.ellipse;
                    break;
                case "draw_line":
                    shape_now = SupportShape.line;
                    break;
                case "draw_rectangle":
                    shape_now = SupportShape.rectang;
                    break;
                case "draw_brush":
                    shape_now = SupportShape.brushes;
                    break;
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.cache_bmp = null;
            this.shapes = new LinkedList<Shape>();
            drawingBoard.Invalidate();
        }

        private void front_color_btn_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            var btn = (ToolStripButton)sender;
            colorDialog.Color = btn.BackColor;
            if(colorDialog.ShowDialog() == DialogResult.OK)
            {
                
                btn.BackColor = colorDialog.Color;
                

                switch (btn.Name)
                {
                    case "front_color_btn":
                        frontColor = colorDialog.Color;
                        break;
                    case "back_color_btn":
                        backColor = colorDialog.Color;
                        break;
                }
            }
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            frontColor = Color.Red;
            backColor = drawingBoard.BackColor;
            shapes = new LinkedList<Shape>();


            for (float i = 1; i < 3; i += 0.5f)
            {
                pen_width.Items.Add(i);
            }
            pen_width.SelectedItem = 1f;
        }

        private void pen_width_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.penWidth = float.Parse(pen_width.SelectedItem.ToString());
        }
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (FileDialog fileDialog = new OpenFileDialog())
                {
                    fileDialog.Filter = "KLPint resource |*.klp";
                    if (fileDialog.ShowDialog() == DialogResult.OK)
                    {
                        shapes = Fileutil.read<LinkedList<Shape>>(fileDialog.FileName);
                        cache_bmp = null;
                        drawingBoard.Invalidate();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FileDialog fileDialog = new SaveFileDialog())
            {
                fileDialog.Filter = "KLPint resource |*.klp";
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    Fileutil.Write(shapes, fileDialog.FileName);
                }
            }
        }
        #endregion

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Fileutil.Write(shapes);
        }
        
    }
}
