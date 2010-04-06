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
			this.btnDeleteCurrentWhatIf = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// btnStartMpc
			// 
			this.btnStartMpc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
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
			this.btnPlayPause.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
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
			this.lbxEvents.Size = new System.Drawing.Size(411, 342);
			this.lbxEvents.TabIndex = 5;
			// 
			// btnDeleteCurrent
			// 
			this.btnDeleteCurrent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnDeleteCurrent.Location = new System.Drawing.Point(333, 12);
			this.btnDeleteCurrent.Name = "btnDeleteCurrent";
			this.btnDeleteCurrent.Size = new System.Drawing.Size(90, 23);
			this.btnDeleteCurrent.TabIndex = 4;
			this.btnDeleteCurrent.Text = "Delete Current";
			this.btnDeleteCurrent.UseVisualStyleBackColor = true;
			this.btnDeleteCurrent.Click += new System.EventHandler(this.btnDeleteCurrent_Click);
			// 
			// btnDeleteCurrentWhatIf
			// 
			this.btnDeleteCurrentWhatIf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnDeleteCurrentWhatIf.Location = new System.Drawing.Point(189, 11);
			this.btnDeleteCurrentWhatIf.Name = "btnDeleteCurrentWhatIf";
			this.btnDeleteCurrentWhatIf.Size = new System.Drawing.Size(138, 23);
			this.btnDeleteCurrentWhatIf.TabIndex = 4;
			this.btnDeleteCurrentWhatIf.Text = "Delete Current (WhatIf)";
			this.btnDeleteCurrentWhatIf.UseVisualStyleBackColor = true;
			this.btnDeleteCurrentWhatIf.Click += new System.EventHandler(this.btnDeleteCurrentWhatIf_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(435, 399);
			this.Controls.Add(this.lbxEvents);
			this.Controls.Add(this.btnDeleteCurrentWhatIf);
			this.Controls.Add(this.btnDeleteCurrent);
			this.Controls.Add(this.btnPlayPause);
			this.Controls.Add(this.btnStartMpc);
			this.Name = "MainForm";
			this.Text = "Media Player Classic Deleter";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnStartMpc;
		private System.Windows.Forms.Button btnPlayPause;
		private System.Windows.Forms.ListBox lbxEvents;
		private System.Windows.Forms.Button btnDeleteCurrent;
		private System.Windows.Forms.Button btnDeleteCurrentWhatIf;
	}
}