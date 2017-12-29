namespace UmlDesigner2.Components.ToolStripArea.SaveOnServer
{
    partial class SaveOnServerForm
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
            this.tbProjectName = new System.Windows.Forms.TextBox();
            this.btSave = new System.Windows.Forms.Button();
            this.btAbort = new System.Windows.Forms.Button();
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
            // tbProjectName
            // 
            this.tbProjectName.Location = new System.Drawing.Point(100, 12);
            this.tbProjectName.Name = "tbProjectName";
            this.tbProjectName.Size = new System.Drawing.Size(172, 20);
            this.tbProjectName.TabIndex = 2;
            this.tbProjectName.Text = ".xml";
            this.tbProjectName.TextChanged += new System.EventHandler(this.LoginTB_TextChanged);
            this.tbProjectName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbProjectName_KeyPress);
            // 
            // btSave
            // 
            this.btSave.BackColor = System.Drawing.Color.Gray;
            this.btSave.FlatAppearance.BorderSize = 0;
            this.btSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btSave.ForeColor = System.Drawing.Color.Wheat;
            this.btSave.Location = new System.Drawing.Point(12, 38);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(127, 23);
            this.btSave.TabIndex = 4;
            this.btSave.Text = "Zapisz";
            this.btSave.UseVisualStyleBackColor = false;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
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
            // SaveOnServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(284, 74);
            this.Controls.Add(this.btAbort);
            this.Controls.Add(this.btSave);
            this.Controls.Add(this.tbProjectName);
            this.Controls.Add(this.label1);
            this.Name = "SaveOnServerForm";
            this.Text = "Zapisz na serwerze";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbProjectName;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.Button btAbort;
    }
}