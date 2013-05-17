namespace ScreenMaker
{
    partial class Form1
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
            this.button1 = new System.Windows.Forms.Button();
            this.TBX1 = new System.Windows.Forms.TextBox();
            this.TBY1 = new System.Windows.Forms.TextBox();
            this.TBX2 = new System.Windows.Forms.TextBox();
            this.TBY2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.XLabel = new System.Windows.Forms.Label();
            this.YLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(118, 64);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 59);
            this.button1.TabIndex = 0;
            this.button1.Text = "Take Screenshot";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // TBX1
            // 
            this.TBX1.Location = new System.Drawing.Point(12, 12);
            this.TBX1.Name = "TBX1";
            this.TBX1.Size = new System.Drawing.Size(100, 20);
            this.TBX1.TabIndex = 1;
            this.TBX1.Text = "X1";
            // 
            // TBY1
            // 
            this.TBY1.Location = new System.Drawing.Point(118, 12);
            this.TBY1.Name = "TBY1";
            this.TBY1.Size = new System.Drawing.Size(100, 20);
            this.TBY1.TabIndex = 2;
            this.TBY1.Text = "Y1";
            // 
            // TBX2
            // 
            this.TBX2.Location = new System.Drawing.Point(12, 38);
            this.TBX2.Name = "TBX2";
            this.TBX2.Size = new System.Drawing.Size(100, 20);
            this.TBX2.TabIndex = 3;
            this.TBX2.Text = "X2";
            // 
            // TBY2
            // 
            this.TBY2.Location = new System.Drawing.Point(118, 38);
            this.TBY2.Name = "TBY2";
            this.TBY2.Size = new System.Drawing.Size(100, 20);
            this.TBY2.TabIndex = 4;
            this.TBY2.Text = "Y2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "X:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Y:";
            // 
            // XLabel
            // 
            this.XLabel.AutoSize = true;
            this.XLabel.Location = new System.Drawing.Point(32, 61);
            this.XLabel.Name = "XLabel";
            this.XLabel.Size = new System.Drawing.Size(0, 13);
            this.XLabel.TabIndex = 7;
            // 
            // YLabel
            // 
            this.YLabel.AutoSize = true;
            this.YLabel.Location = new System.Drawing.Point(32, 75);
            this.YLabel.Name = "YLabel";
            this.YLabel.Size = new System.Drawing.Size(0, 13);
            this.YLabel.TabIndex = 8;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(225, 131);
            this.Controls.Add(this.YLabel);
            this.Controls.Add(this.XLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TBY2);
            this.Controls.Add(this.TBX2);
            this.Controls.Add(this.TBY1);
            this.Controls.Add(this.TBX1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox TBX1;
        private System.Windows.Forms.TextBox TBY1;
        private System.Windows.Forms.TextBox TBX2;
        private System.Windows.Forms.TextBox TBY2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label XLabel;
        private System.Windows.Forms.Label YLabel;
    }
}

