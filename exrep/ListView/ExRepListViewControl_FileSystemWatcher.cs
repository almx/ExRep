using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace ExRep
{
	public partial class ExRepListViewControl : UserControl
	{
		private void SetupFileSystemWatcher(string directory)
		{
			this.exRepListView.FileSystemWatcher = new FileSystemWatcher(directory);
			this.exRepListView.FileSystemWatcher.NotifyFilter = NotifyFilters.Attributes | NotifyFilters.CreationTime | NotifyFilters.DirectoryName | NotifyFilters.FileName | NotifyFilters.LastWrite | NotifyFilters.Security | NotifyFilters.Size;
			this.exRepListView.FileSystemWatcher.Changed += new FileSystemEventHandler(FileSystemWatcher_Changed);
			this.exRepListView.FileSystemWatcher.Created += new FileSystemEventHandler(FileSystemWatcher_Created);
			this.exRepListView.FileSystemWatcher.Deleted += new FileSystemEventHandler(FileSystemWatcher_Deleted);
			this.exRepListView.FileSystemWatcher.Renamed += new RenamedEventHandler(FileSystemWatcher_Renamed);
			this.exRepListView.FileSystemWatcher.EnableRaisingEvents = true;

			DirectoryInfo di = new DirectoryInfo(directory);

			if (!IsTopLevelDirectory(directory) && di.Parent != null)
			{
				this.exRepListView.FileSystemWatcherCurrentDirectory = new FileSystemWatcher(di.Parent.FullName, di.Name);
				this.exRepListView.FileSystemWatcherCurrentDirectory.Deleted += new FileSystemEventHandler(FileSystemWatcherCurrentDirectory_Deleted);
				this.exRepListView.FileSystemWatcherCurrentDirectory.EnableRaisingEvents = true;
			}
		}

		void FileSystemWatcherCurrentDirectory_Deleted(object sender, FileSystemEventArgs e)
		{
			ChangeCurrentDirectory(this.exRepListView.CurrentDirectory.Parent.FullName);
			this.exRepListView.MessageHandler.AddMessage(String.Format(@"Current directory '{0}' was deleted; moved up one level.", this.exRepListView.CurrentDirectory.Name), MessagePanelColor.Message);
		}

		private bool IsTopLevelDirectory(string directory)
		{
			return (directory == new DirectoryInfo(directory).Root.FullName);
		}

		void FileSystemWatcher_Renamed(object sender, RenamedEventArgs e)
		{
			ListViewItem lvi = GetListViewItem(e.OldFullPath);
			
			if(lvi != null)
				ListViewItemCreator.SetListViewItemValues(lvi, GetFileSystemInfo(e.FullPath), this.exRepListView);
		}

		void FileSystemWatcher_Deleted(object sender, FileSystemEventArgs e)
		{
			ListViewItem lvi = GetListViewItem(e.FullPath);

			if (this.listView.InvokeRequired)
				this.listView.Invoke(new RemoveListViewItemDelegate(RemoveListViewItem), lvi);
			else
				RemoveListViewItem(lvi);
		}

		private void RemoveListViewItem(ListViewItem lvi)
		{
			this.listView.Items.Remove(lvi);
		}

		void FileSystemWatcher_Created(object sender, FileSystemEventArgs e)
		{
			// newly created files are caught by the Changed event
			if (Directory.Exists(e.FullPath))
				this.AddItemToListView(e.FullPath);
		}

		void FileSystemWatcher_Changed(object sender, FileSystemEventArgs e)
		{
			Thread.Sleep(50);	// ensures all changes are properly read

			if (GetFileSystemInfo(e.FullPath) == null)
				return;

			if (e.ChangeType == WatcherChangeTypes.Changed)
			{
				ListViewItem lvi = GetListViewItem(e.FullPath);

				if (lvi == null)
					this.AddItemToListView(e.FullPath);
				else
					ListViewItemCreator.SetListViewItemValues(lvi, GetFileSystemInfo(e.FullPath), this.exRepListView);
			}
		}
	}
}
