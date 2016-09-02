namespace MpcDeleter.Forms
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
      this.btnStartMpc = new System.Windows.Forms.Button();
      this.btnPlayPause = new System.Windows.Forms.Button();
      this.lbxEvents = new System.Windows.Forms.ListBox();
      this.btnDeleteCurrent = new System.Windows.Forms.Button();
      this.btnArchiveCurrent = new System.Windows.Forms.Button();
      this.chkWhatIf = new System.Windows.Forms.CheckBox();
      this.btnlFastForward10Percent = new System.Windows.Forms.Button();
      this.btnNext = new System.Windows.Forms.Button();
      this.lblCurrentFile = new System.Windows.Forms.Label();
      this.lblLength = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // btnStartMpc
      // 
      this.btnStartMpc.Location = new System.Drawing.Point(12, 11);
      this.btnStartMpc.Name = "btnStartMpc";
      this.btnStartMpc.Size = new System.Drawing.Size(75, 23);
      this.btnStartMpc.TabIndex = 3;
      this.btnStartMpc.Text = "Start MPC";
      this.btnStartMpc.UseVisualStyleBackColor = true;
      this.btnStartMpc.Click += new System.EventHandler(this.btnStartMpc_Click);
      // 
      // btnPlayPause
      // 
      this.btnPlayPause.Location = new System.Drawing.Point(93, 11);
      this.btnPlayPause.Name = "btnPlayPause";
      this.btnPlayPause.Size = new System.Drawing.Size(72, 23);
      this.btnPlayPause.TabIndex = 4;
      this.btnPlayPause.Text = "Play/Pause";
      this.btnPlayPause.UseVisualStyleBackColor = true;
      this.btnPlayPause.Click += new System.EventHandler(this.btnPlayPause_Click);
      // 
      // lbxEvents
      // 
      this.lbxEvents.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.lbxEvents.FormattingEnabled = true;
      this.lbxEvents.HorizontalScrollbar = true;
      this.lbxEvents.Location = new System.Drawing.Point(12, 40);
      this.lbxEvents.Name = "lbxEvents";
      this.lbxEvents.Size = new System.Drawing.Size(624, 264);
      this.lbxEvents.TabIndex = 5;
      // 
      // btnDeleteCurrent
      // 
      this.btnDeleteCurrent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnDeleteCurrent.Location = new System.Drawing.Point(429, 12);
      this.btnDeleteCurrent.Name = "btnDeleteCurrent";
      this.btnDeleteCurrent.Size = new System.Drawing.Size(90, 23);
      this.btnDeleteCurrent.TabIndex = 4;
      this.btnDeleteCurrent.Text = "Delete Current";
      this.btnDeleteCurrent.UseVisualStyleBackColor = true;
      this.btnDeleteCurrent.Click += new System.EventHandler(this.btnDeleteCurrent_Click);
      // 
      // btnArchiveCurrent
      // 
      this.btnArchiveCurrent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnArchiveCurrent.Location = new System.Drawing.Point(525, 12);
      this.btnArchiveCurrent.Name = "btnArchiveCurrent";
      this.btnArchiveCurrent.Size = new System.Drawing.Size(111, 23);
      this.btnArchiveCurrent.TabIndex = 6;
      this.btnArchiveCurrent.Text = "Archive Current";
      this.btnArchiveCurrent.UseVisualStyleBackColor = true;
      this.btnArchiveCurrent.Click += new System.EventHandler(this.btnArchiveCurrent_Click);
      // 
      // chkWhatIf
      // 
      this.chkWhatIf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.chkWhatIf.AutoSize = true;
      this.chkWhatIf.Checked = true;
      this.chkWhatIf.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chkWhatIf.Location = new System.Drawing.Point(365, 16);
      this.chkWhatIf.Name = "chkWhatIf";
      this.chkWhatIf.Size = new System.Drawing.Size(58, 17);
      this.chkWhatIf.TabIndex = 7;
      this.chkWhatIf.Text = "WhatIf";
      this.chkWhatIf.UseVisualStyleBackColor = true;
      // 
      // btnlFastForward10Percent
      // 
      this.btnlFastForward10Percent.Location = new System.Drawing.Point(171, 11);
      this.btnlFastForward10Percent.Name = "btnlFastForward10Percent";
      this.btnlFastForward10Percent.Size = new System.Drawing.Size(82, 23);
      this.btnlFastForward10Percent.TabIndex = 4;
      this.btnlFastForward10Percent.Text = "Ffw 10%";
      this.btnlFastForward10Percent.UseVisualStyleBackColor = true;
      this.btnlFastForward10Percent.Click += new System.EventHandler(this.btnlFastForward10Percent_Click);
      // 
      // btnNext
      // 
      this.btnNext.Location = new System.Drawing.Point(259, 12);
      this.btnNext.Name = "btnNext";
      this.btnNext.Size = new System.Drawing.Size(82, 23);
      this.btnNext.TabIndex = 4;
      this.btnNext.Text = "Next >>";
      this.btnNext.UseVisualStyleBackColor = true;
      this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
      // 
      // lblCurrentFile
      // 
      this.lblCurrentFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.lblCurrentFile.AutoEllipsis = true;
      this.lblCurrentFile.Location = new System.Drawing.Point(13, 318);
      this.lblCurrentFile.Name = "lblCurrentFile";
      this.lblCurrentFile.Size = new System.Drawing.Size(520, 23);
      this.lblCurrentFile.TabIndex = 8;
      // 
      // lblLength
      // 
      this.lblLength.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.lblLength.Location = new System.Drawing.Point(536, 318);
      this.lblLength.Name = "lblLength";
      this.lblLength.Size = new System.Drawing.Size(100, 23);
      this.lblLength.TabIndex = 9;
      this.lblLength.TextAlign = System.Drawing.ContentAlignment.TopRight;
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(648, 343);
      this.Controls.Add(this.lblLength);
      this.Controls.Add(this.lblCurrentFile);
      this.Controls.Add(this.chkWhatIf);
      this.Controls.Add(this.btnArchiveCurrent);
      this.Controls.Add(this.lbxEvents);
      this.Controls.Add(this.btnDeleteCurrent);
      this.Controls.Add(this.btnNext);
      this.Controls.Add(this.btnlFastForward10Percent);
      this.Controls.Add(this.btnPlayPause);
      this.Controls.Add(this.btnStartMpc);
      this.MinimumSize = new System.Drawing.Size(470, 370);
      this.Name = "MainForm";
      this.Text = "Media Player Classic Deleter";
      this.ResumeLayout(false);
      this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnStartMpc;
		private System.Windows.Forms.Button btnPlayPause;
		private System.Windows.Forms.ListBox lbxEvents;
		private System.Windows.Forms.Button btnDeleteCurrent;
		private System.Windows.Forms.Button btnArchiveCurrent;
		private System.Windows.Forms.CheckBox chkWhatIf;
		private System.Windows.Forms.Button btnlFastForward10Percent;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Label lblCurrentFile;
        private System.Windows.Forms.Label lblLength;
	}
}