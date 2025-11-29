using System;
using System.Drawing;
using System.Windows.Forms;

namespace COTUI.控件类库
{
    /// <summary>
    /// 工具栏增强功能类
    /// </summary>
    public static class ToolStripHelper
    {
        /// <summary>
        /// 启用工具栏的完整拖动功能
        /// </summary>
        /// <param name="toolStrip">要启用拖动的工具栏</param>
        /// <param name="parentForm">父窗体</param>
        public static void EnableDragging(ToolStrip toolStrip, Form parentForm)
        {
            // 启用基本拖动功能
            toolStrip.GripStyle = ToolStripGripStyle.Visible;
            toolStrip.AllowItemReorder = true;
            
            // 添加提示
            toolStrip.GripMargin = new Padding(2, 2, 4, 2);
            
            // 创建拖动状态
            bool isDragging = false;
            Point dragStartPoint = Point.Empty;
            Point originalLocation = Point.Empty;
            DockStyle originalDock = toolStrip.Dock;

            // 鼠标按下事件
            toolStrip.MouseDown += (sender, e) =>
            {
                if (e.Button == MouseButtons.Left)
                {
                    // 检查是否点击在Grip区域
                    var item = toolStrip.GetItemAt(e.Location);
                    if (item == null || e.X < 50)
                    {
                        isDragging = true;
                        dragStartPoint = e.Location;
                        originalLocation = toolStrip.Location;
                        originalDock = toolStrip.Dock;
                        toolStrip.Cursor = Cursors.SizeAll;
                    }
                }
            };

            // 鼠标移动事件
            toolStrip.MouseMove += (sender, e) =>
            {
                if (isDragging && e.Button == MouseButtons.Left)
                {
                    // 如果是停靠状态，先取消停靠
                    if (toolStrip.Dock != DockStyle.None)
                    {
                        toolStrip.Dock = DockStyle.None;
                        // 设置初始位置为鼠标当前位置
                        originalLocation = parentForm.PointToClient(Cursor.Position);
                        originalLocation.X -= dragStartPoint.X;
                        originalLocation.Y -= dragStartPoint.Y;
                    }

                    // 计算新位置
                    Point newLocation = new Point(
                        originalLocation.X + (e.X - dragStartPoint.X),
                        originalLocation.Y + (e.Y - dragStartPoint.Y)
                    );

                    // 限制在窗体范围内
                    newLocation.X = Math.Max(0, Math.Min(newLocation.X, 
                        parentForm.ClientSize.Width - toolStrip.Width));
                    newLocation.Y = Math.Max(0, Math.Min(newLocation.Y, 
                        parentForm.ClientSize.Height - toolStrip.Height));

                    toolStrip.Location = newLocation;
                }
            };

            // 鼠标释放事件
            toolStrip.MouseUp += (sender, e) =>
            {
                if (isDragging)
                {
                    isDragging = false;
                    toolStrip.Cursor = Cursors.Default;

                    // 自动停靠检测
                    AutoDock(toolStrip, parentForm);
                }
            };
        }

        /// <summary>
        /// 自动停靠检测
        /// </summary>
        private static void AutoDock(ToolStrip toolStrip, Form parentForm)
        {
            const int dockThreshold = 30; // 停靠阈值

            // 检查各个边缘
            if (toolStrip.Top < dockThreshold)
            {
                toolStrip.Dock = DockStyle.Top;
            }
            else if (toolStrip.Bottom > parentForm.ClientSize.Height - dockThreshold)
            {
                toolStrip.Dock = DockStyle.Bottom;
            }
            else if (toolStrip.Left < dockThreshold)
            {
                toolStrip.Dock = DockStyle.Left;
            }
            else if (toolStrip.Right > parentForm.ClientSize.Width - dockThreshold)
            {
                toolStrip.Dock = DockStyle.Right;
            }
        }

        /// <summary>
        /// 重置工具栏到默认顶部停靠
        /// </summary>
        public static void ResetDock(ToolStrip toolStrip)
        {
            toolStrip.Dock = DockStyle.Top;
        }

        /// <summary>
        /// 切换工具栏停靠状态
        /// </summary>
        public static void ToggleDock(ToolStrip toolStrip, Form parentForm)
        {
            if (toolStrip.Dock != DockStyle.None)
            {
                toolStrip.Dock = DockStyle.None;
                // 移动到窗体中央
                toolStrip.Location = new Point(
                    (parentForm.ClientSize.Width - toolStrip.Width) / 2,
                    (parentForm.ClientSize.Height - toolStrip.Height) / 2
                );
            }
            else
            {
                toolStrip.Dock = DockStyle.Top;
            }
        }
    }
}
