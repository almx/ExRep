using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;

namespace ExRep
{
	public class ExRepListViewColumnSizePresenterRawInteger : IExRepListViewColumnValuePresenter
	{
		public string GetPresentation(FileSystemInfo fsi)
		{
			if (fsi is DirectoryInfo)
				return "";
			else
			{
				return ((FileInfo)fsi).Length.ToString();
			}
		}
	}

	public class ExRepListViewColumnSizePresenterThousandSeparated : IExRepListViewColumnValuePresenter
	{
		public string GetPresentation(FileSystemInfo fsi)
		{
			if (fsi is DirectoryInfo)
				return "";
			else
			{
				return ((FileInfo)fsi).Length.ToString("N0");
			}
		}
	}

	public class ExRepListViewColumnSize : ExRepListViewColumn
	{
		public override string Name
		{
			get { return "Size"; }
		}

		public override int Compare(object x, object y)
		{
			return ListViewItemComparer.CompareByLong((ListViewItem)x, (ListViewItem)y, new ExRepListViewColumnSizePresenterRawInteger(), this.SortOrder);
		}
	}
}
