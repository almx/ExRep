using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ExRep
{
	public class ExRepListViewColumnDateModifiedPresenterSortablePattern : IExRepListViewColumnValuePresenter
	{
		public string GetPresentation(FileSystemInfo fsi)
		{
			return fsi.LastWriteTime.ToString("yyyy'-'MM'-'dd HH':'mm':'ss");
		}
	}

	public class ExRepListViewColumnDateModified : ExRepListViewColumn
	{
		public override string Name
		{
			get { return "Date Modified"; }
		}

		public override int Compare(object x, object y)
		{
			return ListViewItemComparer.CompareByString((ListViewItem)x, (ListViewItem)y, new ExRepListViewColumnDateModifiedPresenterSortablePattern(), this.SortOrder);
		}
	}
}
