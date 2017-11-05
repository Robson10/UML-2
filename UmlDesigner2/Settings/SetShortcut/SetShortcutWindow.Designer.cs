namespace UmlDesigner2.Settings.SetShortcut
{
    partial class SetShortcutWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Ok = new System.Windows.Forms.Button();
            this.Anuluj = new System.Windows.Forms.Button();
            this.keyboard1 = new Keyboard.Keyboard();
            this.SuspendLayout();
            // 
            // Ok
            // 
            this.Ok.FlatAppearance.BorderSize = 0;
            this.Ok.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Ok.Location = new System.Drawing.Point(227, 232);
            this.Ok.Name = "Ok";
            this.Ok.Size = new System.Drawing.Size(75, 23);
            this.Ok.TabIndex = 1;
            this.Ok.Text = "Ok";
            this.Ok.UseVisualStyleBackColor = true;
            // 
            // Anuluj
            // 
            this.Anuluj.FlatAppearance.BorderSize = 0;
            this.Anuluj.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Anuluj.Location = new System.Drawing.Point(308, 232);
            this.Anuluj.Name = "Anuluj";
            this.Anuluj.Size = new System.Drawing.Size(75, 23);
            this.Anuluj.TabIndex = 2;
            this.Anuluj.Text = "Anuluj";
            this.Anuluj.UseVisualStyleBackColor = true;
            // 
            // keyboard1
            // 
            this.keyboard1.BackColor = System.Drawing.Color.Transparent;
            this.keyboard1.ControlBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(47)))), ((int)(((byte)(47)))));
            this.keyboard1.KeyBackColor = System.Drawing.Color.Gray;
            this.keyboard1.KeyForeColor = System.Drawing.Color.Wheat;
            this.keyboard1.KeySelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(130)))), ((int)(((byte)(0)))));
            this.keyboard1.Location = new System.Drawing.Point(13, 13);
            this.keyboard1.Name = "keyboard1";
            this.keyboard1.SelectedKeys = System.Windows.Forms.Keys.None;
            this.keyboard1.Size = new System.Drawing.Size(593, 189);
            this.keyboard1.TabIndex = 3;
            // 
            // SetShortcutWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(618, 267);
            this.Controls.Add(this.keyboard1);
            this.Controls.Add(this.Anuluj);
            this.Controls.Add(this.Ok);
            this.Name = "SetShortcutWindow";
            this.Text = "SetShortcutWindow";
            this.ResumeLayout(false);

        }

        #endregion
        
        private System.Windows.Forms.Button Ok;
        private System.Windows.Forms.Button Anuluj;
        private Keyboard.Keyboard keyboard1;
    }
}