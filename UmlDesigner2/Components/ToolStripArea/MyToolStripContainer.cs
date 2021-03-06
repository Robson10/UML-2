﻿using System;
using System.Drawing;
using System.Windows.Forms;

namespace SbWinNew.Components.ToolStripArea
{
    public partial class MyToolStrip : ToolStrip
    {
        private readonly Size _buttonSize = new Size(40, 40);

        #region Var
        private readonly MyToolStripButton _newFile = new MyToolStripButton(ToolStripButtonParameters.StripButtons.NewFile);
        private readonly MyToolStripButton _openFile = new MyToolStripButton(ToolStripButtonParameters.StripButtons.OpenFile);
        private readonly MyToolStripButton _saveFile = new MyToolStripButton(ToolStripButtonParameters.StripButtons.SaveFile);
        private readonly MyToolStripButton _saveFileAs = new MyToolStripButton(ToolStripButtonParameters.StripButtons.SaveFileAs);
        private readonly ToolStripSeparator _s1 = new ToolStripSeparator();

        private readonly MyToolStripButton _redo = new MyToolStripButton(ToolStripButtonParameters.StripButtons.Redo);
        private readonly MyToolStripButton _undo = new MyToolStripButton(ToolStripButtonParameters.StripButtons.Undo);
        private readonly ToolStripSeparator _s2 = new ToolStripSeparator();

        private readonly MyToolStripButton _options = new MyToolStripButton(ToolStripButtonParameters.StripButtons.Options);
        private readonly MyToolStripButton _logIn = new MyToolStripButton(ToolStripButtonParameters.StripButtons.LogIn);
        private readonly MyToolStripButton _openFileFromServer = new MyToolStripButton(ToolStripButtonParameters.StripButtons.OpenFileFromServer);
        private readonly MyToolStripButton _saveFileOnServer = new MyToolStripButton(ToolStripButtonParameters.StripButtons.SaveFileOnServer);
        private readonly ToolStripSeparator _s3 = new ToolStripSeparator();

        private readonly MyToolStripButton _run = new MyToolStripButton(ToolStripButtonParameters.StripButtons.Run);
        private readonly MyToolStripButton _debug = new MyToolStripButton(ToolStripButtonParameters.StripButtons.Debug);
        #endregion
        
        public MyToolStrip()
        {
            Settings();
            AddElements();
        }

        private void Settings()
        {
            this.ImageScalingSize = _buttonSize;

            _openFileFromServer.Enabled = false;
            _saveFileOnServer.Enabled = false;
            //Default.RenderMode = ToolStripRenderMode.System;//usuwanie z rogów artefaktow

        }

        private void AddElements()
        {            
            this.Items.Add(_newFile);
            this.Items.Add(_openFile);
            this.Items.Add(_saveFile);
            this.Items.Add(_saveFileAs);
            this.Items.Add(_openFileFromServer);
            this.Items.Add(_saveFileOnServer);
            this.Items.Add(_s1);

            this.Items.Add(_undo);
            this.Items.Add(_redo);
            this.Items.Add(_s2);

            this.Items.Add(_options);
            this.Items.Add(_logIn);
            this.Items.Add(_s3);

            this.Items.Add(_run);
            this.Items.Add(_debug);
        }

        public void BlockButtonsForEgzam()
        {
            _newFile.Enabled = false;
            _openFile.Enabled = false;
            _openFileFromServer.Enabled = false;
        }

        public void Login()
        {
            if (_newFile.Enabled)
                _openFileFromServer.Enabled = true;
            _saveFileOnServer.Enabled = true;
        }


        #region eventHandlers
        public event EventHandler NewFileClick
        {
            add => _newFile.Click += value;
            remove => _newFile.Click -= value;
        }
        public event EventHandler OpenFileClick
        {
            add => _openFile.Click += value;
            remove => _openFile.Click -= value;
        }
        public event EventHandler SaveFileClick
        {
            add => _saveFile.Click += value;
            remove => _saveFile.Click -= value;
        }
        public event EventHandler SaveFileAsClick
        {
            add => _saveFileAs.Click += value;
            remove => _saveFileAs.Click -= value;
        }
        public event EventHandler RedoClick
        {
            add => _redo.Click += value;
            remove => _redo.Click -= value;
        }
        public event EventHandler UndoClick
        {
            add => _undo.Click += value;
            remove => _undo.Click -= value;
        }
        public event EventHandler OptionsClick
        {
            add => _options.Click += value;
            remove => _options.Click -= value;
        }
        public event EventHandler LogInClick
        {
            add => _logIn.Click += value;
            remove => _logIn.Click -= value;
        }
        public event EventHandler RunClick
        {
            add => _run.Click += value;
            remove => _run.Click -= value;
        }
        public event EventHandler DebugClick
        {
            add => _debug.Click += value;
            remove => _debug.Click -= value;
        }
        public event EventHandler SaveFileOnServerClick
        {
            add => _saveFileOnServer.Click += value;
            remove => _saveFileOnServer.Click -= value;
        }
        public event EventHandler OpenFileFromServerClick
        {
            add => _openFileFromServer.Click += value;
            remove => _openFileFromServer.Click -= value;
        }
        #endregion
    }
}