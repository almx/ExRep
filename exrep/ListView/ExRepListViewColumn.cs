using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using System.Drawing;

namespace ExRep
{
	public abstract class ExRepListViewColumn : IComparer
	{
		public abstract string Name { get; }
		public int Width { get; set; }
		public int Index { get; set; }
		public FontFamily FontFamily { get; set; }
		public HorizontalAlignment Alignment { get; set; }
		public SortOrder SortOrder { get; set; }
		
		public IExRepListViewColumnValuePresenter Presenter { get; set; }
		public abstract int Compare(object x, object y);
	}
}
