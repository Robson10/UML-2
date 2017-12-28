using System.Windows.Forms;

namespace UmlDesigner2.Components.Settings.SetShortcut.Keyboard
{
    public class KeyboardKey:Button
    {
        public Keys Key { get; set; } = Keys.None;
        public bool isSelected { get; set; } = false;
        public bool isModyfier { get; set; } = false;
    }
}
