using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;

namespace ExRep
{
	public class ExRepListViewColumnExtensionPresenterDefault : IExRepListViewColumnValuePresenter
	{
		public string GetPresentation(FileSystemInfo fsi)
		{
			if (fsi is DirectoryInfo)
				return "";
			else
				return ((FileInfo)fsi).Extension.TrimStart('.');
		}
	}

	public class ExRepListViewColumnExtensionPresenterLowerCase : IExRepListViewColumnValuePresenter
	{
		public string GetPresentation(FileSystemInfo fsi)
		{
			if (fsi is DirectoryInfo)
				return "";
			else
				return ((FileInfo)fsi).Extension.ToLower().TrimStart('.');
		}
	}

	public class ExRepListViewColumnExtension : ExRepListViewColumn
	{
		public override string Name
		{
			get { return "Ext"; }
		}

		public override int Compare(object x, object y)
		{
			return ListViewItemComparer.CompareByString((ListViewItem)x, (ListViewItem)y, new ExRepListViewColumnExtensionPresenterLowerCase(), this.SortOrder);
		}
	}
}
