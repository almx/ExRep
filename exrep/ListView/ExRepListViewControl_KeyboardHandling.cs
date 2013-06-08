using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Drawing;

namespace ExRep
{
	public partial class ExRepListViewControl : UserControl
	{
		private void listView_KeyUp(object sender, KeyEventArgs e)
		{
			List<FileSystemInfo> fsiList = GetSelectedFileSystemInfoList();

			ProgramAction action;

			try
			{
				action = this.exRepListView.KeyboardShortcutHandler.GetAction(e);
			}
			catch (KeyboardShortcutNotRecognizedException)
			{
				return;
			}

			if (action == ProgramAction.RefreshList)
			{
				List<string> selectedPaths = this.GetSelectedPaths();
				string focusedPath = this.GetFocusedPath();
				
				this.PopulateListView(this.exRepListView.CurrentDirectory.FullName);

				foreach (ListViewItem lvi in this.GetListViewItems(selectedPaths))
				{
					lvi.Selected = true;
				}

				ListViewItem focusedItem = this.GetListViewItem(focusedPath);

				if (focusedItem != null)
					focusedItem.Focused = true;
			}

			if (action == ProgramAction.NavigateUp)
			{
				DirectoryInfo parentDirectory = this.exRepListView.CurrentDirectory.Parent;

				if (parentDirectory != null)
				{
					ChangeCurrentDirectory(this.exRepListView.CurrentDirectory.Parent.FullName);
				}
				else
				{
					// todo: no parent = show drive list
				}

				return;
			}

			if (action == ProgramAction.MarkItemsForCopy)
			{

			}

			if (fsiList.Count == 1)
			{
				FileSystemInfo fsi = fsiList[0];

				if (action == ProgramAction.OpenItem)
				{
					if (fsi != null && fsi is FileInfo)
					{
						FileInfo fi = (FileInfo)fsi;

						if (listView.SelectedItems.Count == 1)
						{
							if (File.Exists(fi.FullName))
								Process.Start(fi.FullName);
							else
								this.exRepListView.MessageHandler.AddMessage("Item does not exist: " + fi.FullName, MessagePanelColor.Error);
						}
					}
					else if (fsi != null && fsi is DirectoryInfo)
					{
						ChangeCurrentDirectory(fsi.FullName);

						if (listView.Items.Count > 0)
							listView.Items[0].Selected = true;
					}
				}

				if (action == ProgramAction.RenameItem)
				{
					Rectangle subItemRectangle = GetSubItemBounds(this.listView.SelectedItems[0], 0);
					BeginEditSelectedItemName(subItemRectangle);
				}
			}
		}

		private void txtLocation_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
			{
				try
				{
					ChangeCurrentDirectory(this.exRepListView.LocationTextBox.GetText());
					this.listView.Focus();
				}
				catch (DirectoryNotFoundException)
				{
					this.exRepListView.MessageHandler.AddMessage(String.Format(@"The entered path does not exist: {0}", this.exRepListView.LocationTextBox.GetText()), MessagePanelColor.Error);
				}
			}
		}
	}
}
