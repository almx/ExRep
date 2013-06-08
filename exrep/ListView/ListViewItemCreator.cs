using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing;

namespace ExRep
{
	public static class ListViewItemCreator
	{
		private delegate void SetListViewItemValuesDelegate(ListViewItem item, FileSystemInfo fsi, ExRepListView listView);

		public static ListViewItem Create(FileSystemInfo fsi, ExRepListView listView)
		{
			ListViewItem item = new ListViewItem();
			item.UseItemStyleForSubItems = false;

			SetListViewItemValues(item, fsi, listView);

			return item;
		}

		public static void SetListViewItemValues(ListViewItem item, FileSystemInfo fsi, ExRepListView listView)
		{
			if (item.ListView != null && item.ListView.InvokeRequired)
				item.ListView.Invoke(new SetListViewItemValuesDelegate(SetListViewItemValues), item, fsi, listView);
			else
			{
				List<ExRepListViewColumn> columns = listView.ColumnsSortedByIndex;

				if (columns.Count == 0)
					throw new Exception("No columns to show in listview");

				item.Text = columns[0].Presenter.GetPresentation(fsi);

				if (columns[0].FontFamily != null)
					item.Font = new Font(columns[0].FontFamily, item.Font.Size);

				for (int i = 1; i < columns.Count; i++)
				{
					if (columns[i].FontFamily == null)
						item.SubItems.Add(columns[i].Presenter.GetPresentation(fsi));
					else
						item.SubItems.Add(columns[i].Presenter.GetPresentation(fsi), item.ForeColor, item.BackColor, new Font(columns[i].FontFamily, item.Font.Size));
				}

				item.Tag = fsi;

				int iconImageIndex = GetIconImageIndex(fsi.FullName, listView);

				if(iconImageIndex != -1)
					item.ImageIndex = GetIconImageIndex(fsi.FullName, listView);
			}
		}

		private static int GetIconImageIndex(string path, ExRepListView listView)
		{
			string iconLookupString = FileTypeIconHandler.GetIconLookupString(path);
			FileTypeIconInfo iconInfo = listView.IconHandler.AddIconToImageList(path, iconLookupString);

			if (iconInfo != null)
				return listView.IconHandler.IconInfoImageListIndexDict[iconInfo];
			else
				return -1;
		}
	}
}
