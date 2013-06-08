using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;

namespace ExRep
{
	public static class ListViewItemComparer
	{
		public static int CompareByString(ListViewItem lvi1, ListViewItem lvi2, IExRepListViewColumnValuePresenter presenter, SortOrder sortOrder)
		{
			if (Configuration.SortDirectoriesOnTop && ((lvi1.Tag is DirectoryInfo && lvi2.Tag is FileInfo) || (lvi1.Tag is FileInfo && lvi2.Tag is DirectoryInfo)))
				return -1;

			string value1 = presenter.GetPresentation((FileSystemInfo)lvi1.Tag);
			string value2 = presenter.GetPresentation((FileSystemInfo)lvi2.Tag);

			return GetCompareValueByString(sortOrder, value1, value2);
		}

		public static int CompareByLong(ListViewItem lvi1, ListViewItem lvi2, IExRepListViewColumnValuePresenter presenter, SortOrder sortOrder)
		{
			if (Configuration.SortDirectoriesOnTop && ((lvi1.Tag is DirectoryInfo && lvi2.Tag is FileInfo) || (lvi1.Tag is FileInfo && lvi2.Tag is DirectoryInfo)))
				return -1;

			long value1;
			if(!long.TryParse(presenter.GetPresentation((FileSystemInfo)lvi1.Tag), out value1))
				value1 = 0;

			long value2;
			if(!long.TryParse(presenter.GetPresentation((FileSystemInfo)lvi2.Tag), out value2))
				value2 = 0;

			return GetCompareValueByLong(sortOrder, value1, value2);
		}

		private static int GetCompareValueByLong(SortOrder sortOrder, long value1, long value2)
		{
			if (sortOrder == SortOrder.Ascending)
				return value1.CompareTo(value2);
			else
				return value2.CompareTo(value1);
		}

		private static int GetCompareValueByString(SortOrder sortOrder, string value1, string value2)
		{
			if (sortOrder == SortOrder.Ascending)
				return new CaseInsensitiveComparer().Compare(value1, value2);
			else
				return new CaseInsensitiveComparer().Compare(value2, value1);
		}
	}
}
