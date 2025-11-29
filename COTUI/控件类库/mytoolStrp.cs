using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace COTUI.控件类库
{
    public class CustomToolStripRenderer : ToolStripProfessionalRenderer
    {
        private Dictionary<string, Color> _labelcolors = new Dictionary<string, Color>();
        public void SetlabelColor(string labelname, Color color) { _labelcolors[labelname] = color; }
        public void RemoveLabelColor(string labelname) { _labelcolors.Remove(labelname); }


        protected override void OnRenderLabelBackground(ToolStripItemRenderEventArgs e)
        {
            if (e.Item is ToolStripLabel Label && !string.IsNullOrEmpty(Label.Name))
            {
                if (_labelcolors.ContainsKey(Label.Name))
                {
                    using (Brush brush = new SolidBrush(_labelcolors[Label.Name]))
                    {
                        e.Graphics.FillRectangle(brush, new Rectangle(Point.Empty, Label.Size));
                    }
                    //using (Pen pen = new Pen(Color.DarkBlue, 1))
                    //{
                    //    e.Graphics.DrawRectangle(pen, new Rectangle(0, 0, Label.Width - 1, Label.Height - 1));
                    //}
                    //设置边框圆角
                    //DrawRoundedRectangle()
                }
            }
            else
            {
                base.OnRenderLabelBackground(e);
            }
        }

        private void DrawRoundedRectangle(Graphics g, Rectangle rec, int radios, Color bordercolor)
        {
        }
    }
}
