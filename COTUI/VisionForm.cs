using COTUI.通用功能类;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace COTUI
{
    public partial class VisionForm : Form
    {
        #region//显示控件变量声明
        private Image currentImage;
        private Point imageLocation;
        private double zoomFactor = 0.5;
        #endregion
        private bool isDragging = false;
        private Point initialMouseLocation;
        private Point initialImageLocation;

        public VisionForm()
        {
            InitializeComponent();
            // 启用拖放支持
            if (imagePictureBox != null)
            {
                imagePictureBox.AllowDrop = true;
            }
        }

        private void imagePictureBox_MouseEnter(object sender, EventArgs e)
        {
            if (imagePictureBox != null)
            {
                imagePictureBox.Focus();
            }
        }

        private void imagePictureBox_MouseWheel(object sender, MouseEventArgs e)
        {
            // 滚轮缩放
            if (currentImage == null)
                return;

            double oldZoom = zoomFactor;

            // 计算缩放
            double zoomDelta = e.Delta > 0 ? 1.1 : 1.0 / 1.1;
            zoomFactor *= zoomDelta;

            // 限制缩放范围
            zoomFactor = Math.Max(0.1, Math.Min(10.0, zoomFactor));

            if (Math.Abs(zoomFactor - oldZoom) > 0.001)
            {
                // 以鼠标位置为中心缩放
                double imageMouseX = (e.X - imageLocation.X) / oldZoom;
                double imageMouseY = (e.Y - imageLocation.Y) / oldZoom;

                imageLocation.X = e.X - (int)(imageMouseX * zoomFactor);
                imageLocation.Y = e.Y - (int)(imageMouseY * zoomFactor);

                // 应用边界约束
                ApplyZoomBoundaryConstraints();

                imagePictureBox.Invalidate();
            }
        }

        // 在类的开头添加这些辅助方法
        private static int Clamp(int value, int min, int max)
        {
            return value < min ? min : value > max ? max : value;
        }

        private static double Clamp(double value, double min, double max)
        {
            return value < min ? min : value > max ? max : value;
        }

        private static float Clamp(float value, float min, float max)
        {
            return value < min ? min : value > max ? max : value;
        }

        private void btnLoadImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "图像文件|*.jpg;*.jpeg;*.png;*.bmp;*.gif|所有文件|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    currentImage = Image.FromFile(openFileDialog.FileName);
                    if (imagePictureBox != null)
                    {
                        // 不直接设置imagePictureBox.Image，而是通过重绘来显示图像
                        // 重置图像位置和缩放
                        imageLocation = new Point(0, 0);
                        zoomFactor = 1.0;
                        imagePictureBox.Invalidate(); // 重绘
                    }
                    if (listBox1 != null)
                    {
                        listBox1.Items.Add(openFileDialog.FileName);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("无法加载图像: " + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1 != null && listBox1.SelectedIndex >= 0)
            {
                string imagePath = listBox1.SelectedItem?.ToString();
                if (!string.IsNullOrEmpty(imagePath) && System.IO.File.Exists(imagePath))
                {
                    try
                    {
                        currentImage = Image.FromFile(imagePath);
                        if (imagePictureBox != null)
                        {
                            // 不直接设置imagePictureBox.Image，而是通过重绘来显示图像
                            // 重置图像位置和缩放
                            imageLocation = new Point(0, 0);
                            zoomFactor = 1.0;
                            imagePictureBox.Invalidate(); // 重绘
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("无法加载图像: " + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void imagePictureBox_Paint(object sender, PaintEventArgs e)
        {
            // 绘制棋盘格背景
            DrawCheckerboardBackground(e.Graphics);

            // 如果有图像，则绘制图像
            if (currentImage != null)
            {
                e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                e.Graphics.DrawImage(currentImage,
                    imageLocation.X,
                    imageLocation.Y,
                    (float)(currentImage.Width * zoomFactor),
                    (float)(currentImage.Height * zoomFactor));
            }
        }

        private void DrawCheckerboardBackground(Graphics g)
        {
            int tileSize = 20;
            using (SolidBrush lightBrush = new SolidBrush(Color.LightGray))
            using (SolidBrush darkBrush = new SolidBrush(Color.DarkGray))
            {
                int width = (imagePictureBox != null) ? imagePictureBox.Width : 0;
                int height = (imagePictureBox != null) ? imagePictureBox.Height : 0;

                for (int y = 0; y < height; y += tileSize)
                {
                    for (int x = 0; x < width; x += tileSize)
                    {
                        Brush brush = ((x / tileSize + y / tileSize) % 2 == 0) ? lightBrush : darkBrush;
                        g.FillRectangle(brush, x, y, tileSize, tileSize);
                    }
                }
            }
        }

        private void imagePictureBox_DoubleClick(object sender, EventArgs e)
        {
            // 双击使图像居中
            zoomFactor = 1.0;
            if (currentImage != null)
            {
                // 将图像居中
                int displayWidth = (int)(currentImage.Width * zoomFactor);
                int displayHeight = (int)(currentImage.Height * zoomFactor);
                
                if (imagePictureBox != null)
                {
                    imageLocation = new Point(
                        (imagePictureBox.Width - displayWidth) / 2,
                        (imagePictureBox.Height - displayHeight) / 2
                    );
                }
            }
            else
            {
                imageLocation = new Point(0, 0);
            }

            if (imagePictureBox != null)
            {
                imagePictureBox.Invalidate();
            }
        }

        private void ApplyZoomBoundaryConstraints()
        {
            if (currentImage == null || imagePictureBox == null) return;

            int scaledWidth = (int)(currentImage.Width * zoomFactor);
            int scaledHeight = (int)(currentImage.Height * zoomFactor);
            int containerWidth = imagePictureBox.ClientSize.Width;
            int containerHeight = imagePictureBox.ClientSize.Height;

            // 水平边界
            if (scaledWidth <= containerWidth)
                imageLocation.X = (containerWidth - scaledWidth) / 2; // 居中
            else
            {
                // 限制在边界内
                int minX = containerWidth - scaledWidth;
                imageLocation.X = Math.Max(minX, Math.Min(0, imageLocation.X));
            }

            // 垂直边界
            if (scaledHeight <= containerHeight)
                imageLocation.Y = (containerHeight - scaledHeight) / 2; // 居中
            else
            {
                // 限制在边界内
                int minY = containerHeight - scaledHeight;
                imageLocation.Y = Math.Max(minY, Math.Min(0, imageLocation.Y));
            }
        }

        private void imagePictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (currentImage == null)
                return;

            // 只支持左键拖拽
            if (e.Button != MouseButtons.Left)
                return;

            try
            {
                isDragging = true;
                initialMouseLocation = e.Location;
                initialImageLocation = imageLocation;

                // 设置拖拽光标
                imagePictureBox.Cursor = Cursors.Hand;

                // 捕获鼠标
                imagePictureBox.Capture = true;
            }
            catch (Exception ex)
            {
                Gvar.Logger.ErrorException(ex, "图片拖拽开始时发生错误");
                isDragging = false;
                imagePictureBox.Cursor = Cursors.Default;
            }
        }

        private void imagePictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isDragging || currentImage == null)
                return;

            // 计算移动距离
            int deltaX = e.X - initialMouseLocation.X;
            int deltaY = e.Y - initialMouseLocation.Y;

            // 更新图像位置
            imageLocation.X = initialImageLocation.X + deltaX;
            imageLocation.Y = initialImageLocation.Y + deltaY;

            // 边界检查
            int scaledWidth = (int)(currentImage.Width * zoomFactor);
            int scaledHeight = (int)(currentImage.Height * zoomFactor);
            int containerWidth = imagePictureBox.ClientSize.Width;
            int containerHeight = imagePictureBox.ClientSize.Height;

            // 水平边界
            if (scaledWidth <= containerWidth)
                imageLocation.X = (containerWidth - scaledWidth) / 2; // 居中
            else
            {
                // 限制在边界内
                int minX = containerWidth - scaledWidth;
                imageLocation.X = Math.Max(minX, Math.Min(0, imageLocation.X));
            }

            // 垂直边界
            if (scaledHeight <= containerHeight)
                imageLocation.Y = (containerHeight - scaledHeight) / 2; // 居中
            else
            {
                // 限制在边界内
                int minY = containerHeight - scaledHeight;
                imageLocation.Y = Math.Max(minY, Math.Min(0, imageLocation.Y));
            }

            // 重绘
            imagePictureBox.Invalidate();
        }

        private void imagePictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (!isDragging)
                return;

            try
            {
                // 支持多种按钮释放
                if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Middle || e.Button == MouseButtons.Right)
                {
                    // 重置拖拽状态
                    isDragging = false;

                    if (imagePictureBox != null)
                    {
                        imagePictureBox.Cursor = Cursors.Default;
                        imagePictureBox.Capture = false; // 释放鼠标捕获
                    }

                    // 应用缩放边界约束
                    ApplyZoomBoundaryConstraints();
                    
                    // 重绘
                    if (imagePictureBox != null)
                    {
                        imagePictureBox.Invalidate();
                    }
                }
            }
            catch (Exception ex)
            {
                Gvar.Logger.ErrorException(ex, "图片拖拽结束时发生错误");
                // 确保状态被重置
                isDragging = false;
                if (imagePictureBox != null)
                {
                    imagePictureBox.Cursor = Cursors.Default;
                    imagePictureBox.Capture = false;
                }
            }
        }
    }
}