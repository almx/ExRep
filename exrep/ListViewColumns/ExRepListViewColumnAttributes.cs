using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;

namespace ExRep
{
	public class ExRepListViewColumnAttributesPresenterDefault : IExRepListViewColumnValuePresenter
	{
		public string GetPresentation(FileSystemInfo fsi)
		{
			return AttributeStringCreator.GetAttributesString(fsi.Attributes);
		}
	}

	public class ExRepListViewColumnAttributes : ExRepListViewColumn
	{
		public override string Name
		{
			get { return "Attr"; }
		}

		public override int Compare(object x, object y)
		{
			return ListViewItemComparer.CompareByString((ListViewItem)x, (ListViewItem)y, new ExRepListViewColumnAttributesPresenterDefault(), this.SortOrder);
		}
	}
}
