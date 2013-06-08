using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace ExRep
{
	public partial class MainForm : Form
	{
		private delegate void MainFormDelegate(string currentDirectory);

		public MainForm()
		{
			InitializeComponent();

			this.exRepListView1.CurrentDirectoryChanged += new CurrentDirectoryChangedDelegate(exRepListView1_CurrentDirectoryChanged);
		
			this.exRepListView1.ChangeCurrentDirectory(@"c:");
		}

		void exRepListView1_CurrentDirectoryChanged(string currentDirectory)
		{
			if (this.InvokeRequired)
				this.Invoke(new MainFormDelegate(exRepListView1_CurrentDirectoryChanged), currentDirectory);
			else
			{
				this.Text = String.Format("{0} - {1}", currentDirectory, Constants.ApplicationName);
			}
		}
	}
}
