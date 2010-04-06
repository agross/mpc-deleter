namespace MpcDeleter
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
			this.btnPlayPause.Size = new System.Drawing.Size(90, 23);
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
			this.lbxEvents.Location = new System.Drawing.Point(12, 40);
			this.lbxEvents.Name = "lbxEvents";
			this.lbxEvents.Size = new System.Drawing.Size(430, 264);
			this.lbxEvents.TabIndex = 5;
			// 
			// btnDeleteCurrent
			// 
			this.btnDeleteCurrent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnDeleteCurrent.Location = new System.Drawing.Point(256, 12);
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
			this.btnArchiveCurrent.Location = new System.Drawing.Point(352, 12);
			this.btnArchiveCurrent.Name = "btnArchiveCurrent";
			this.btnArchiveCurrent.Size = new System.Drawing.Size(90, 23);
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
			this.chkWhatIf.Location = new System.Drawing.Point(192, 16);
			this.chkWhatIf.Name = "chkWhatIf";
			this.chkWhatIf.Size = new System.Drawing.Size(58, 17);
			this.chkWhatIf.TabIndex = 7;
			this.chkWhatIf.Text = "WhatIf";
			this.chkWhatIf.UseVisualStyleBackColor = true;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(454, 332);
			this.Controls.Add(this.chkWhatIf);
			this.Controls.Add(this.btnArchiveCurrent);
			this.Controls.Add(this.lbxEvents);
			this.Controls.Add(this.btnDeleteCurrent);
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
	}
}