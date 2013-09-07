namespace Clicker
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            this.SalvageButton = new System.Windows.Forms.Button();
            this.ChangeStateButton = new System.Windows.Forms.Button();
            this.TBRoundCount = new System.Windows.Forms.TextBox();
            this.QuitCheck = new System.Windows.Forms.CheckBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.LRoundLeft = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.StopButton2 = new System.Windows.Forms.Button();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // SalvageButton
            // 
            this.SalvageButton.Location = new System.Drawing.Point(7, 6);
            this.SalvageButton.Name = "SalvageButton";
            this.SalvageButton.Size = new System.Drawing.Size(75, 23);
            this.SalvageButton.TabIndex = 0;
            this.SalvageButton.Text = "Start";
            this.SalvageButton.UseVisualStyleBackColor = true;
            this.SalvageButton.Click += new System.EventHandler(this.SalvageButton_Click);
            // 
            // ChangeStateButton
            // 
            this.ChangeStateButton.Location = new System.Drawing.Point(112, 113);
            this.ChangeStateButton.Name = "ChangeStateButton";
            this.ChangeStateButton.Size = new System.Drawing.Size(75, 23);
            this.ChangeStateButton.TabIndex = 1;
            this.ChangeStateButton.Text = "Start(F8)";
            this.ChangeStateButton.UseVisualStyleBackColor = true;
            this.ChangeStateButton.Click += new System.EventHandler(this.ChangeStateButton_Click);
            // 
            // TBRoundCount
            // 
            this.TBRoundCount.Location = new System.Drawing.Point(8, 23);
            this.TBRoundCount.Name = "TBRoundCount";
            this.TBRoundCount.Size = new System.Drawing.Size(86, 20);
            this.TBRoundCount.TabIndex = 3;
            this.TBRoundCount.Text = "10";
            // 
            // QuitCheck
            // 
            this.QuitCheck.AutoSize = true;
            this.QuitCheck.Location = new System.Drawing.Point(155, 7);
            this.QuitCheck.Name = "QuitCheck";
            this.QuitCheck.Size = new System.Drawing.Size(143, 17);
            this.QuitCheck.TabIndex = 4;
            this.QuitCheck.Text = "Выйти  по завершению";
            this.QuitCheck.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.QuitCheck.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(1, 1);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(313, 190);
            this.tabControl1.TabIndex = 5;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.LRoundLeft);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.ChangeStateButton);
            this.tabPage1.Controls.Add(this.QuitCheck);
            this.tabPage1.Controls.Add(this.TBRoundCount);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(305, 164);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Start";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // LRoundLeft
            // 
            this.LRoundLeft.AutoSize = true;
            this.LRoundLeft.Location = new System.Drawing.Point(4, 46);
            this.LRoundLeft.Name = "LRoundLeft";
            this.LRoundLeft.Size = new System.Drawing.Size(95, 13);
            this.LRoundLeft.TabIndex = 6;
            this.LRoundLeft.Text = "Циклов осталось";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Количество циклов";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.StopButton2);
            this.tabPage2.Controls.Add(this.SalvageButton);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(305, 164);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Salvaging";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // StopButton2
            // 
            this.StopButton2.Location = new System.Drawing.Point(224, 6);
            this.StopButton2.Name = "StopButton2";
            this.StopButton2.Size = new System.Drawing.Size(75, 23);
            this.StopButton2.TabIndex = 6;
            this.StopButton2.Text = "Stop";
            this.StopButton2.UseVisualStyleBackColor = true;
            this.StopButton2.Click += new System.EventHandler(this.StopButton2_Click);
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(315, 189);
            this.Controls.Add(this.tabControl1);
            this.Name = "MainForm";
            this.Text = "Miner";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button SalvageButton;
        private System.Windows.Forms.Button ChangeStateButton;
        private System.Windows.Forms.TextBox TBRoundCount;
        private System.Windows.Forms.CheckBox QuitCheck;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button StopButton2;
        private System.Windows.Forms.Label LRoundLeft;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer;
    }
}