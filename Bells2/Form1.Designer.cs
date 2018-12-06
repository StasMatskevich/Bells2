namespace Bells2
{
    partial class Main
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.drawer = new System.Windows.Forms.Timer(this.components);
            this.timeLabel = new System.Windows.Forms.Label();
            this.bellTime = new System.Windows.Forms.Label();
            this.tableBox = new System.Windows.Forms.PictureBox();
            this.copyRightLink = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.tableBox)).BeginInit();
            this.SuspendLayout();
            // 
            // drawer
            // 
            this.drawer.Enabled = true;
            this.drawer.Interval = 10;
            this.drawer.Tick += new System.EventHandler(this.drawer_Tick);
            // 
            // timeLabel
            // 
            this.timeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.timeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.timeLabel.Location = new System.Drawing.Point(12, 9);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(916, 49);
            this.timeLabel.TabIndex = 2;
            this.timeLabel.Text = "Сейчас: ";
            this.timeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bellTime
            // 
            this.bellTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bellTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bellTime.Location = new System.Drawing.Point(12, 54);
            this.bellTime.Name = "bellTime";
            this.bellTime.Size = new System.Drawing.Size(916, 45);
            this.bellTime.TabIndex = 3;
            this.bellTime.Text = "Звонок через: ";
            this.bellTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableBox
            // 
            this.tableBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableBox.BackColor = System.Drawing.Color.White;
            this.tableBox.Location = new System.Drawing.Point(12, 108);
            this.tableBox.Name = "tableBox";
            this.tableBox.Size = new System.Drawing.Size(916, 459);
            this.tableBox.TabIndex = 0;
            this.tableBox.TabStop = false;
            this.tableBox.Click += new System.EventHandler(this.tableBox_Click);
            this.tableBox.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawFrame);
            // 
            // copyRightLink
            // 
            this.copyRightLink.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.copyRightLink.AutoSize = true;
            this.copyRightLink.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.copyRightLink.Location = new System.Drawing.Point(416, 570);
            this.copyRightLink.Name = "copyRightLink";
            this.copyRightLink.Size = new System.Drawing.Size(124, 13);
            this.copyRightLink.TabIndex = 4;
            this.copyRightLink.TabStop = true;
            this.copyRightLink.Text = "@2017 Stas Matskevich";
            this.copyRightLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.copyRightLink_LinkClicked);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(940, 590);
            this.Controls.Add(this.copyRightLink);
            this.Controls.Add(this.bellTime);
            this.Controls.Add(this.timeLabel);
            this.Controls.Add(this.tableBox);
            this.MinimumSize = new System.Drawing.Size(940, 590);
            this.Name = "Main";
            this.Text = "Bells";
            ((System.ComponentModel.ISupportInitialize)(this.tableBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer drawer;
        private System.Windows.Forms.PictureBox tableBox;
        private System.Windows.Forms.Label timeLabel;
        private System.Windows.Forms.Label bellTime;
        private System.Windows.Forms.LinkLabel copyRightLink;
    }
}

