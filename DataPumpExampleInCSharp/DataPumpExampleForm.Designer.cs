namespace DataPumpExampleInCSharp
{
    partial class DataPumpExampleForm
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
            this.usernameTextBox = new System.Windows.Forms.TextBox();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.logListBox = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tickersListBox = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.getUsernamePasswordButton = new System.Windows.Forms.Button();
            this.busyTextBox = new System.Windows.Forms.TextBox();
            this.sendTickButton = new System.Windows.Forms.Button();
            this.sendBarButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 62);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Username";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(45, 85);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Password";
            // 
            // usernameTextBox
            // 
            this.usernameTextBox.Location = new System.Drawing.Point(101, 60);
            this.usernameTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.usernameTextBox.Name = "usernameTextBox";
            this.usernameTextBox.ReadOnly = true;
            this.usernameTextBox.Size = new System.Drawing.Size(136, 20);
            this.usernameTextBox.TabIndex = 2;
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Location = new System.Drawing.Point(101, 83);
            this.passwordTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.ReadOnly = true;
            this.passwordTextBox.Size = new System.Drawing.Size(136, 20);
            this.passwordTextBox.TabIndex = 3;
            // 
            // logListBox
            // 
            this.logListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.logListBox.FormattingEnabled = true;
            this.logListBox.Location = new System.Drawing.Point(25, 140);
            this.logListBox.Margin = new System.Windows.Forms.Padding(2);
            this.logListBox.Name = "logListBox";
            this.logListBox.Size = new System.Drawing.Size(381, 316);
            this.logListBox.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 125);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Log";
            // 
            // tickersListBox
            // 
            this.tickersListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tickersListBox.FormattingEnabled = true;
            this.tickersListBox.Location = new System.Drawing.Point(410, 140);
            this.tickersListBox.Margin = new System.Windows.Forms.Padding(2);
            this.tickersListBox.Name = "tickersListBox";
            this.tickersListBox.Size = new System.Drawing.Size(155, 316);
            this.tickersListBox.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(407, 125);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Live Tickers";
            // 
            // getUsernamePasswordButton
            // 
            this.getUsernamePasswordButton.Enabled = false;
            this.getUsernamePasswordButton.Location = new System.Drawing.Point(27, 11);
            this.getUsernamePasswordButton.Margin = new System.Windows.Forms.Padding(2);
            this.getUsernamePasswordButton.Name = "getUsernamePasswordButton";
            this.getUsernamePasswordButton.Size = new System.Drawing.Size(210, 27);
            this.getUsernamePasswordButton.TabIndex = 8;
            this.getUsernamePasswordButton.Text = "Get Username and Password";
            this.getUsernamePasswordButton.UseVisualStyleBackColor = true;
            this.getUsernamePasswordButton.Click += new System.EventHandler(this.getUsernamePasswordButton_Click);
            // 
            // busyTextBox
            // 
            this.busyTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.busyTextBox.ForeColor = System.Drawing.Color.Red;
            this.busyTextBox.Location = new System.Drawing.Point(410, 92);
            this.busyTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.busyTextBox.Name = "busyTextBox";
            this.busyTextBox.ReadOnly = true;
            this.busyTextBox.Size = new System.Drawing.Size(155, 20);
            this.busyTextBox.TabIndex = 10;
            // 
            // sendTickButton
            // 
            this.sendTickButton.Location = new System.Drawing.Point(410, 11);
            this.sendTickButton.Margin = new System.Windows.Forms.Padding(2);
            this.sendTickButton.Name = "sendTickButton";
            this.sendTickButton.Size = new System.Drawing.Size(155, 27);
            this.sendTickButton.TabIndex = 11;
            this.sendTickButton.Text = "Send Tick";
            this.sendTickButton.UseVisualStyleBackColor = true;
            this.sendTickButton.Click += new System.EventHandler(this.sendTickButton_Click);
            // 
            // sendBarButton
            // 
            this.sendBarButton.Location = new System.Drawing.Point(410, 43);
            this.sendBarButton.Name = "sendBarButton";
            this.sendBarButton.Size = new System.Drawing.Size(155, 27);
            this.sendBarButton.TabIndex = 12;
            this.sendBarButton.Text = "Send Bar";
            this.sendBarButton.UseVisualStyleBackColor = true;
            this.sendBarButton.Click += new System.EventHandler(this.sendBarButton_Click);
            // 
            // DataPumpExampleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 461);
            this.Controls.Add(this.sendBarButton);
            this.Controls.Add(this.sendTickButton);
            this.Controls.Add(this.busyTextBox);
            this.Controls.Add(this.getUsernamePasswordButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tickersListBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.logListBox);
            this.Controls.Add(this.passwordTextBox);
            this.Controls.Add(this.usernameTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximumSize = new System.Drawing.Size(600, 500);
            this.MinimumSize = new System.Drawing.Size(600, 500);
            this.Name = "DataPumpExampleForm";
            this.Text = "MTAPI DataPump Example in C#";
            this.Load += new System.EventHandler(this.DataPumpExampleForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox usernameTextBox;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.ListBox logListBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox tickersListBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button getUsernamePasswordButton;
        private System.Windows.Forms.TextBox busyTextBox;
        private System.Windows.Forms.Button sendTickButton;
        private System.Windows.Forms.Button sendBarButton;
    }
}

