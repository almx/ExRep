using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Runtime.InteropServices;
using System.Collections;

namespace ExRep
{
	public delegate void CurrentDirectoryChangedDelegate(string currentDirectory);

	public partial class ExRepListViewControl : UserControl
	{
		private delegate void AddListViewItemDelegate(string path);
		private delegate void SetSelectedItemInListViewDelegate();
		private delegate void PopulateListViewDelegate(string directoryPath);
		private delegate ListViewItem GetListViewItemDelegate(string path);
		private delegate void RemoveListViewItemDelegate(ListViewItem item);
		public event CurrentDirectoryChangedDelegate CurrentDirectoryChanged;

		private ExRepListView exRepListView;
		private TextBox editTextBox;

		public ExRepListViewControl()
		{
			InitializeComponent();

			this.exRepListView = new ExRepListView();
			this.exRepListView.MessageHandler = new MessageHandler(this.labelMessage);
			this.exRepListView.KeyboardShortcutHandler = new KeyboardShortcutHandler(Configuration.GetKeyboardShortcutMapping());
			this.exRepListView.IconHandler = new FileTypeIconHandler(this.components);
			this.exRepListView.LocationTextBox = new LocationTextBox(this.txtLocation);

			InitializeControls();
		}

		private void InitializeControls()
		{
			this.listView.Columns.AddRange(ListViewColumnHeaderCreator.CreateColumnHeaders(this.exRepListView.ColumnsSortedByIndex).ToArray());
			this.listView.View = View.Details;
			this.listView.LargeImageList = this.exRepListView.IconHandler.ImageListLargeIcons;
			this.listView.SmallImageList = this.exRepListView.IconHandler.ImageListSmallIcons;
			this.labelMessage.Text = "";

			//this.listViewColumnSorter = new ListViewColumnSorter();
			this.listView.ListViewItemSorter = this.exRepListView.Columns[this.exRepListView.CurrentSortColumnIndex];
		}

		private void SetSelectedItemInListView()
		{
			if (this.listView.InvokeRequired)
				this.listView.Invoke(new SetSelectedItemInListViewDelegate(SetSelectedItemInListView));
			else
			{
				if (listView.SelectedItems.Count == 0 && listView.Items.Count > 0 && GetFocusedPath() == null)
				{
					listView.Items[0].Selected = true;
					listView.Items[0].Focused = true;
				}
			}
		}

		private List<FileSystemInfo> GetSelectedFileSystemInfoList()
		{
			List<FileSystemInfo> fsiList = new List<FileSystemInfo>();

			foreach (ListViewItem lvi in this.listView.SelectedItems)
				fsiList.Add((FileSystemInfo)lvi.Tag);

			return fsiList;
		}

		private List<string> GetSelectedPaths()
		{
			List<string> paths = new List<string>();

			foreach (ListViewItem lvi in this.listView.SelectedItems)
				paths.Add(((FileSystemInfo)lvi.Tag).FullName);

			return paths;
		}

		private string GetFocusedPath()
		{
			ListViewItem item = this.listView.FocusedItem;

			if (item != null)
				return ((FileSystemInfo)this.listView.FocusedItem.Tag).FullName;
			else
				return null;
		}

		private List<ListViewItem> GetListViewItems(List<string> paths)
		{
			List<ListViewItem> items = new List<ListViewItem>();

			foreach (string path in paths)
				items.Add(GetListViewItem(path));

			return items;
		}

		private ListViewItem GetListViewItem(string path)
		{
			if (this.listView.InvokeRequired)
				return (ListViewItem)this.listView.Invoke(new GetListViewItemDelegate(GetListViewItem), path);
			else
			{
				foreach (ListViewItem lvi in this.listView.Items)
				{
					if (((FileSystemInfo)lvi.Tag).FullName == path)
						return lvi;
				}

				return null;
			}
		}

		public void ChangeCurrentDirectory(string directory)
		{
			this.exRepListView.MessageHandler.ClearMessage();
			
			if(this.exRepListView.FileSystemWatcher != null)
				this.exRepListView.FileSystemWatcher.Dispose();

			if (this.exRepListView.FileSystemWatcherCurrentDirectory != null)
				this.exRepListView.FileSystemWatcherCurrentDirectory.Dispose();

			if (Directory.Exists(directory))
			{
				if (PathValidator.IsPathLogicalDriveRoot(directory) && !directory.EndsWith(@"\"))
					directory += @"\";

				this.exRepListView.CurrentDirectory = new DirectoryInfo(directory);
				PopulateListView(directory);
				this.exRepListView.LocationTextBox.SetText(directory);

				if (this.CurrentDirectoryChanged != null)
					this.CurrentDirectoryChanged(directory);

				SetSelectedItemInListView();

				SetupFileSystemWatcher(directory);
			}
			else if (PathValidator.IsPathUNCServerRoot(directory))
			{
				// todo: show list of shares on server
			}
			else
			{
				this.txtLocation.SelectAll();
				throw new DirectoryNotFoundException("directory not found attempting to change dir: " + directory);
			}
		}

		private FileSystemInfo GetFileSystemInfo(string path)
		{
			if (Directory.Exists(path))
				return new DirectoryInfo(path);
			else if (File.Exists(path))
				return new FileInfo(path);
			else
				return null;
		}

		private void AddItemToListView(string path)
		{
			if (this.listView.InvokeRequired)
				this.listView.Invoke(new AddListViewItemDelegate(AddItemToListView), path);
			else
			{
				ListViewItem lvi = ListViewItemCreator.Create(GetFileSystemInfo(path), this.exRepListView);

				this.listView.Items.Add(lvi);
			}
		}

		private void PopulateListView(string directoryPath)
		{
			if (this.listView.InvokeRequired)
				this.listView.Invoke(new PopulateListViewDelegate(PopulateListView), directoryPath);
			else
			{
				this.listView.Items.Clear();

				if (String.IsNullOrEmpty(directoryPath))
					return;
				else
				{
					List<ListViewItem> listViewItems = new List<ListViewItem>();

					foreach (DirectoryInfo di in FileSystemInfoCreator.GetSubDirectoryInfoList(directoryPath))
						listViewItems.Add(ListViewItemCreator.Create(di, this.exRepListView));

					foreach (FileInfo fi in FileSystemInfoCreator.GetFileInfoList(directoryPath))
						listViewItems.Add(ListViewItemCreator.Create(fi, this.exRepListView));

					this.listView.Items.AddRange(listViewItems.ToArray<ListViewItem>());
				}
			}
		}

		private void listView_ColumnClick(object sender, ColumnClickEventArgs e)
		{
			this.exRepListView.CurrentSortColumnIndex = e.Column;
			this.listView.ListViewItemSorter = this.exRepListView.Columns[this.exRepListView.CurrentSortColumnIndex];

			if (e.Column == this.exRepListView.CurrentSortColumnIndex)
			{
				if (this.exRepListView.CurrentSortColumn.SortOrder == SortOrder.Ascending)
					this.exRepListView.CurrentSortColumn.SortOrder = SortOrder.Descending;
				else
					this.exRepListView.CurrentSortColumn.SortOrder = SortOrder.Ascending;
			}
			else
				this.exRepListView.CurrentSortColumn.SortOrder = SortOrder.Ascending;

			this.listView.Sort();
		}
	}
}
