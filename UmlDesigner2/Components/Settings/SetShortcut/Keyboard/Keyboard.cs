using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using SbWinNew.Class;

namespace SbWinNew.Components.Settings.SetShortcut.Keyboard
{
    public partial class Keyboard : UserControl
    {
        private List<KeyboardKey> keyList = new List<KeyboardKey>();

        private Color _keyBackColor= Helper.ButtonColor;
        [Browsable(true)]
        public Color KeyBackColor { get => _keyBackColor;
            set
            {
                _keyBackColor = value;
                Invalidate();
            }
        }

        private Color _keyForeColor = Helper.TextColor;
        [Browsable(true)]
        public Color KeyForeColor
        {
            get => _keyForeColor;
            set
            {
                _keyForeColor = value;
                Invalidate();
            }
        }

        private Color _keySelectedColor = Helper.ButtonSelectedColor;
        [Browsable(true)]
        public Color KeySelectedColor
        {
            get => _keySelectedColor;
            set
            {
                _keySelectedColor = value;
                Invalidate();
            }
        }

        private Color _backColor = Helper.BackColor;
        [Browsable(true)]
        public Color ControlBackColor
        {
            get => _backColor;
            set
            {
                _backColor = value;
                Invalidate();
            }
        }

        [Browsable(false)]
        public Keys SelectedKeys { get; set; } = Keys.None;

        public Keyboard()
        {
            InitializeComponent();
            BackColor = Color.Transparent;
            SetViewForButtons();
        }
        private void SetViewForButtons()
        {
            keyList.Clear();
            foreach (var key in Controls)//load all keyboard key which typy is KeyboardKey
            {
                if (key is KeyboardKey)
                {
                    keyList.Add(key as KeyboardKey);
                }
            }
            for (int i = 0; i < keyList.Count; i++)
            {
                keyList[i].BackColor = KeyBackColor;
                keyList[i].ForeColor = KeyForeColor;
                keyList[i].Click += Key_Click;
                keyList[i].Key = (Keys)Enum.Parse(typeof(Keys), keyList[i].Text, true);
            }
        }

        private void Key_Click(object sender, EventArgs e)
        {
            if ((sender as KeyboardKey).BackColor == KeyBackColor)
            {
                if (!(sender as KeyboardKey).isModyfier)
                {
                    keyList.FindAll(x => !x.isModyfier).ForEach(x => x.BackColor = KeyBackColor);
                }
                (sender as KeyboardKey).BackColor = KeySelectedColor;
                (sender as KeyboardKey).isSelected = true;
                ReadSelectedKeysToVariable();
            }
            else
            {
                (sender as KeyboardKey).BackColor = KeyBackColor;
                (sender as KeyboardKey).isSelected = false;
            }
        }

        private void ReadSelectedKeysToVariable()
        {
            var modifiers = keyList.FindAll(x => x.isSelected && x.isModyfier);
            var keys = keyList.FindAll(x => x.isSelected && !x.isModyfier);
            SelectedKeys = Keys.None;

            if (modifiers.Count > 0 && keys.Count>0)
            {                
                for (int i = 0; i < modifiers.Count; i++)
                    SelectedKeys |= modifiers[i].Key;
                for (int i = 0; i < keys.Count; i++)
                    SelectedKeys |= keys[i].Key;
            }
        }
    }
}
