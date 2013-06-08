namespace ExRep
{
	partial class ExRepListViewControl
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.panelMessages = new System.Windows.Forms.Panel();
			this.labelMessage = new System.Windows.Forms.Label();
			this.listView = new System.Windows.Forms.ListView();
			this.txtLocation = new System.Windows.Forms.TextBox();
			this.panelMessages.SuspendLayout();
			this.SuspendLayout();
			// 
			// panelMessages
			// 
			this.panelMessages.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.panelMessages.BackColor = System.Drawing.SystemColors.Control;
			this.panelMessages.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panelMessages.Controls.Add(this.labelMessage);
			this.panelMessages.Location = new System.Drawing.Point(0, 26);
			this.panelMessages.Name = "panelMessages";
			this.panelMessages.Size = new System.Drawing.Size(804, 26);
			this.panelMessages.TabIndex = 5;
			// 
			// labelMessage
			// 
			this.labelMessage.AutoSize = true;
			this.labelMessage.Location = new System.Drawing.Point(4, 4);
			this.labelMessage.Name = "labelMessage";
			this.labelMessage.Size = new System.Drawing.Size(35, 13);
			this.labelMessage.TabIndex = 0;
			this.labelMessage.Text = "label1";
			// 
			// listView
			// 
			this.listView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.listView.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.listView.FullRowSelect = true;
			this.listView.Location = new System.Drawing.Point(0, 58);
			this.listView.Name = "listView";
			this.listView.Size = new System.Drawing.Size(804, 590);
			this.listView.TabIndex = 6;
			this.listView.UseCompatibleStateImageBehavior = false;
			this.listView.View = System.Windows.Forms.View.Details;
			this.listView.Enter += new System.EventHandler(this.listView_Enter);
			this.listView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView_ColumnClick);
			this.listView.KeyUp += new System.Windows.Forms.KeyEventHandler(this.listView_KeyUp);
			// 
			// txtLocation
			// 
			this.txtLocation.Dock = System.Windows.Forms.DockStyle.Top;
			this.txtLocation.Location = new System.Drawing.Point(0, 0);
			this.txtLocation.Name = "txtLocation";
			this.txtLocation.Size = new System.Drawing.Size(804, 20);
			this.txtLocation.TabIndex = 3;
			this.txtLocation.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtLocation_KeyUp);
			this.txtLocation.Enter += new System.EventHandler(this.txtLocation_Enter);
			// 
			// ExRepListViewControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.panelMessages);
			this.Controls.Add(this.listView);
			this.Controls.Add(this.txtLocation);
			this.Name = "ExRepListViewControl";
			this.Size = new System.Drawing.Size(804, 648);
			this.panelMessages.ResumeLayout(false);
			this.panelMessages.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel panelMessages;
		private System.Windows.Forms.Label labelMessage;
		private System.Windows.Forms.TextBox txtLocation;
		private System.Windows.Forms.ListView listView;

	}
}
