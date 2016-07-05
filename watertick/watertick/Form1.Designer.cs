namespace watertick
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
            this.TickCount = new System.Windows.Forms.Label();
            this.LastMinute = new System.Windows.Forms.Label();
            this.LastHour = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // TickCount
            // 
            this.TickCount.AutoSize = true;
            this.TickCount.Location = new System.Drawing.Point(13, 13);
            this.TickCount.Name = "TickCount";
            this.TickCount.Size = new System.Drawing.Size(51, 13);
            this.TickCount.TabIndex = 0;
            this.TickCount.Text = "tickcount";
            // 
            // LastMinute
            // 
            this.LastMinute.AutoSize = true;
            this.LastMinute.Location = new System.Drawing.Point(13, 40);
            this.LastMinute.Name = "LastMinute";
            this.LastMinute.Size = new System.Drawing.Size(54, 13);
            this.LastMinute.TabIndex = 1;
            this.LastMinute.Text = "lastminute";
            // 
            // LastHour
            // 
            this.LastHour.AutoSize = true;
            this.LastHour.Location = new System.Drawing.Point(13, 67);
            this.LastHour.Name = "LastHour";
            this.LastHour.Size = new System.Drawing.Size(44, 13);
            this.LastHour.TabIndex = 2;
            this.LastHour.Text = "lasthour";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.LastHour);
            this.Controls.Add(this.LastMinute);
            this.Controls.Add(this.TickCount);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Water Meter";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label TickCount;
        private System.Windows.Forms.Label LastMinute;
        private System.Windows.Forms.Label LastHour;
    }
}

