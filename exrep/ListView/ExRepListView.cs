using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ExRep
{
	public delegate void ExRepListViewColumnCollectionChanged();

	public class ExRepListView
	{
		public ExRepListViewColumnCollectionChanged ColumnsChanged;

		public DirectoryInfo CurrentDirectory { get; set; }
		public MessageHandler MessageHandler { get; set; }
		public KeyboardShortcutHandler KeyboardShortcutHandler { get; set; }
		public FileTypeIconHandler IconHandler { get; set; }
		public LocationTextBox LocationTextBox { get; set; }
		public FileSystemWatcher FileSystemWatcher { get; set; }
		public FileSystemWatcher FileSystemWatcherCurrentDirectory { get; set; }
		public int CurrentSortColumnIndex { get; set; }
		
		public ExRepListViewColumn CurrentSortColumn
		{
			get { return this.Columns[this.CurrentSortColumnIndex]; }
		}

		private List<ExRepListViewColumn> columns;
		public List<ExRepListViewColumn> Columns
		{
			get { return this.columns; }
			set
			{
				this.columns = value;

				if (ColumnsChanged != null)
					ColumnsChanged();
			}
		}

		public ExRepListView()
		{
			this.KeyboardShortcutHandler = new KeyboardShortcutHandler(Configuration.GetKeyboardShortcutMapping());
			this.Columns = Configuration.GetColumnCollection();
		}

		public List<ExRepListViewColumn> ColumnsSortedByIndex
		{
			get
			{
				this.Columns.Sort(new Comparison<ExRepListViewColumn>(ComparerByIndex));
				return this.Columns;
			}
		}

		private int ComparerByIndex(ExRepListViewColumn a, ExRepListViewColumn b)
		{
			return a.Index - b.Index;
		}
		
		public ExRepListViewColumn GetColumn(Type columnType)
		{
			foreach(ExRepListViewColumn column in this.Columns)
			{
				if (column.GetType() == columnType)
					return column;
			}

			return null;
		}
	}
}
