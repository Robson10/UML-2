namespace UmlDesigner2.Components.ToolStripArea.OpenFromServer
{
    partial class OpenFromServerForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.btLoad = new System.Windows.Forms.Button();
            this.btAbort = new System.Windows.Forms.Button();
            this.comboFiles = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Wheat;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nazwa Projektu";
            // 
            // btLoad
            // 
            this.btLoad.BackColor = System.Drawing.Color.Gray;
            this.btLoad.FlatAppearance.BorderSize = 0;
            this.btLoad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btLoad.ForeColor = System.Drawing.Color.Wheat;
            this.btLoad.Location = new System.Drawing.Point(12, 38);
            this.btLoad.Name = "btLoad";
            this.btLoad.Size = new System.Drawing.Size(127, 23);
            this.btLoad.TabIndex = 4;
            this.btLoad.Text = "Wczytaj";
            this.btLoad.UseVisualStyleBackColor = false;
            this.btLoad.Click += new System.EventHandler(this.btLoad_Click);
            // 
            // btAbort
            // 
            this.btAbort.BackColor = System.Drawing.Color.Gray;
            this.btAbort.FlatAppearance.BorderSize = 0;
            this.btAbort.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btAbort.ForeColor = System.Drawing.Color.Wheat;
            this.btAbort.Location = new System.Drawing.Point(145, 38);
            this.btAbort.Name = "btAbort";
            this.btAbort.Size = new System.Drawing.Size(127, 23);
            this.btAbort.TabIndex = 5;
            this.btAbort.Text = "Anuluj";
            this.btAbort.UseVisualStyleBackColor = false;
            this.btAbort.Click += new System.EventHandler(this.btAbort_Click);
            // 
            // comboFiles
            // 
            this.comboFiles.FormattingEnabled = true;
            this.comboFiles.Location = new System.Drawing.Point(100, 7);
            this.comboFiles.Name = "comboFiles";
            this.comboFiles.Size = new System.Drawing.Size(172, 21);
            this.comboFiles.TabIndex = 6;
            // 
            // OpenFromServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(284, 74);
            this.Controls.Add(this.comboFiles);
            this.Controls.Add(this.btAbort);
            this.Controls.Add(this.btLoad);
            this.Controls.Add(this.label1);
            this.Name = "OpenFromServerForm";
            this.Text = "Pobierz z serwera";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btLoad;
        private System.Windows.Forms.Button btAbort;
        private System.Windows.Forms.ComboBox comboFiles;
    }
}