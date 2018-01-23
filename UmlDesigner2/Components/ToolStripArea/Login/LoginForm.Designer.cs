namespace SbWinNew.Components.ToolStripArea.Login
{
    partial class LoginForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.LoginTB = new System.Windows.Forms.TextBox();
            this.PasswordTB = new System.Windows.Forms.TextBox();
            this.btLogin = new System.Windows.Forms.Button();
            this.Exit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Wheat;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Login";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Wheat;
            this.label2.Location = new System.Drawing.Point(11, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Haslo";
            // 
            // LoginTB
            // 
            this.LoginTB.Location = new System.Drawing.Point(68, 12);
            this.LoginTB.Name = "LoginTB";
            this.LoginTB.Size = new System.Drawing.Size(204, 20);
            this.LoginTB.TabIndex = 2;
            this.LoginTB.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.LoginTB_KeyPress);
            // 
            // PasswordTB
            // 
            this.PasswordTB.Location = new System.Drawing.Point(68, 38);
            this.PasswordTB.Name = "PasswordTB";
            this.PasswordTB.PasswordChar = 'X';
            this.PasswordTB.Size = new System.Drawing.Size(204, 20);
            this.PasswordTB.TabIndex = 3;
            this.PasswordTB.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.PasswordTB_KeyPress);
            // 
            // btLogin
            // 
            this.btLogin.BackColor = System.Drawing.Color.Gray;
            this.btLogin.FlatAppearance.BorderSize = 0;
            this.btLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btLogin.ForeColor = System.Drawing.Color.Wheat;
            this.btLogin.Location = new System.Drawing.Point(12, 64);
            this.btLogin.Name = "btLogin";
            this.btLogin.Size = new System.Drawing.Size(127, 23);
            this.btLogin.TabIndex = 4;
            this.btLogin.Text = "Zaloguj";
            this.btLogin.UseVisualStyleBackColor = false;
            this.btLogin.Click += new System.EventHandler(this.btLogin_Click);
            // 
            // Exit
            // 
            this.Exit.BackColor = System.Drawing.Color.Gray;
            this.Exit.FlatAppearance.BorderSize = 0;
            this.Exit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Exit.ForeColor = System.Drawing.Color.Wheat;
            this.Exit.Location = new System.Drawing.Point(145, 64);
            this.Exit.Name = "Exit";
            this.Exit.Size = new System.Drawing.Size(127, 23);
            this.Exit.TabIndex = 5;
            this.Exit.Text = "Anuluj";
            this.Exit.UseVisualStyleBackColor = false;
            this.Exit.Click += new System.EventHandler(this.Exit_Click);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(284, 102);
            this.Controls.Add(this.Exit);
            this.Controls.Add(this.btLogin);
            this.Controls.Add(this.PasswordTB);
            this.Controls.Add(this.LoginTB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "LoginForm";
            this.Text = "Logowanie";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox LoginTB;
        private System.Windows.Forms.TextBox PasswordTB;
        private System.Windows.Forms.Button btLogin;
        private System.Windows.Forms.Button Exit;
    }
}