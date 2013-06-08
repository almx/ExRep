using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace ExRep
{
	public partial class ExRepListViewControl : UserControl
	{
		private void txtLocation_Enter(object sender, EventArgs e)
		{
			txtLocation.SelectAll();
		}

		private void listView_Enter(object sender, EventArgs e)
		{
			SetSelectedItemInListView();
		}
	}
}
