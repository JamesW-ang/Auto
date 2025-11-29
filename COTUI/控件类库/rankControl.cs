using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace COTUI.控件类库
{
    public class rankControl
    {
        public static void ArrangeRightControls(ToolStrip toolstrip, ToolStripItem fisrtRightItem, ToolStripItem lastTRightItem)
        {
            toolstrip.SuspendLayout();

            //收集所有控件（脚本控件除外）
            //var allItems = toolstrip.Items.Cast<ToolStripItem>().ToList();
            //var existingItems = allItems.Where(item => item != fisrtRightItem && item != lastTRightItem).ToList();

            var manualItems= toolstrip.Items.Cast<ToolStripItem>()
                             .Where(item => item != fisrtRightItem && item != lastTRightItem).ToList();

            //清空toolStrip
            toolstrip.Items.Clear();

            //重新排布控件
            toolstrip.Items.Add(fisrtRightItem);
            //添加所有手动控件
            foreach (var item in manualItems) { toolstrip.Items.Add(item); }
            toolstrip.Items.Add(lastTRightItem);


            toolstrip.ResumeLayout();
        }

        //private static ToolStripSeparator CreateStretchSeparator()
        //{
        //    ToolStripSeparator separator = new ToolStripSeparator();
        //    separator.AutoSize = false;
        //    separator.Width = 1;
        //    return separator;
        //}
    }
}
