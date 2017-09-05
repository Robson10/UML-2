using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmlDesigner2.Component.ToolStripArea
{
    public sealed class MyToolStripButton : System.Windows.Forms.ToolStripButton
    {
        public ToolStripButtonParameters.StripButtons ButtonType { get; }
        public MyToolStripButton(ToolStripButtonParameters.StripButtons buttonType)
        {
            ButtonType = buttonType;
            ToolTipText = ToolStripButtonParameters.StripButtonToolTip(ButtonType);
            Image = ToolStripButtonParameters.GetIcon(ButtonType, ToolStripButtonParameters.IconSize);
        }
    }
}
