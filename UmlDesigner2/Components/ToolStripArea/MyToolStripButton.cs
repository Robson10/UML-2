namespace SbWinNew.Components.ToolStripArea
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
