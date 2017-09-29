using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Windows.Forms;

namespace UmlDesigner2.Settings
{
    public partial class SettingWindow : Form
    {

        private PropertyGrid _pg;
        public SettingWindow()
        {
            InitializeComponent();
            _pg = new PropertyGrid()
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right|AnchorStyles.Bottom,
                PropertySort = PropertySort.Categorized|PropertySort.Alphabetical,
            };
            _pg.SelectedObject = new SettingsPropertyGrid();
            tabControl1.TabPages[1].Controls.Add(_pg);
            _pg.SetBounds(0, 0, Width, Height);
            _pg.Dock =DockStyle.Fill;
            SetTextButtons(CopyBT,Helper.KeyCopy);
            SetTextButtons(PasteBT, Helper.KeyPaste);
            SetTextButtons(CutBT, Helper.KeyCut);
            SetTextButtons(DebugBT, Helper.KeyDebug);
            SetTextButtons(CompileBT, Helper.KeyRun);
            SetTextButtons(NewFileBT, Helper.KeyNewFile);
            SetTextButtons(OpenFileBT, Helper.KeyOpenFile);
            SetTextButtons(OpenFileFromServerBT, Helper.KeyOpenFileFromServer);
            SetTextButtons(SaveFileBT, Helper.KeySaveFile);
            SetTextButtons(SaveFileAsBT, Helper.KeySaveFileAs);
            SetTextButtons(UndoBT, Helper.KeyUndo);
            SetTextButtons(RedoBT, Helper.KeyRedo);

        }

        public void SetTextButtons(Button bt,Keys key)
        {
            //podzielic jakos keya na fragmenty np cotrol + C
            string temp = "";
            //var keyArr = key.ToString().Split(',').ToList();
            //var index = keyArr.FindIndex(x=>x.Equals("Control"));
            //temp+=keyArr[index];
            //keyArr.RemoveAt(index);

            temp += key.ToString();

            bt.Text = temp;
        }
    }
}
