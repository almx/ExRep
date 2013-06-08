using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;

namespace ExRep
{
	public class ExRepListViewColumnNamePresenterDefault : IExRepListViewColumnValuePresenter
	{
		public string GetPresentation(FileSystemInfo fsi)
		{
			return fsi.Name;
		}
	}

	public class ExRepListViewColumnName : ExRepListViewColumn
	{
		public override string Name
		{
			get { return "Name"; }
		}

		public override int Compare(object x, object y)
		{
			return ListViewItemComparer.CompareByString((ListViewItem)x, (ListViewItem)y, new ExRepListViewColumnNamePresenterDefault(), this.SortOrder);
		}
	}
}
