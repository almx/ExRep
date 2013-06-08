using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ExRep
{
	public static class ListViewColumnHeaderCreator
	{
		public static List<ColumnHeader> CreateColumnHeaders(List<ExRepListViewColumn> columns)
		{
			List<ColumnHeader> columnHeaders = new List<ColumnHeader>();

			foreach (ExRepListViewColumn column in columns)
			{
				columnHeaders.Add(CreateColumnHeader(column));
			}

			return columnHeaders;
		}

		private static ColumnHeader CreateColumnHeader(ExRepListViewColumn column)
		{
			ColumnHeader header = new ColumnHeader();
			header.Text = column.Name;
			header.Name = column.Name;
			header.Width = column.Width;
			header.TextAlign = column.Alignment;

			return header;
		}
	}
}
