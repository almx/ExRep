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
		public Rectangle GetSubItemBounds(ListViewItem lvi, int subItemIndex)
		{
			Rectangle lviBounds = lvi.GetBounds(ItemBoundsPortion.Entire);

			int subItemX = lviBounds.Left;

			for (int i = 0; i < GetNameColumnIndex(); i++)
				subItemX += this.listView.Columns[i].Width;

			Rectangle rect = new Rectangle();
			rect.Y = lviBounds.Top;
			rect.X = subItemX + lvi.GetBounds(ItemBoundsPortion.Icon).Width + 4;	// magic number for making the textbox appear just to the right of the icon

			rect.Size = new Size(GetSelectedItemNameTextWidth(), lvi.GetBounds(ItemBoundsPortion.Entire).Height);

			return rect;
		}

		private int GetSelectedItemNameTextWidth()
		{
			Graphics graphics = this.CreateGraphics();
			FileSystemInfo fsi = GetSelectedFileSystemInfoList()[0];

			return GetTextWidth(fsi.Name, this.listView.SelectedItems[0].Font);
		}

		private void BeginEditSelectedItemName(Rectangle subItemRectangle)
		{
			subItemRectangle.Width += 5;

			editTextBox = new TextBox();
			editTextBox.Bounds = subItemRectangle;
			editTextBox.Text = GetSelectedFileSystemInfoList()[0].Name;
			editTextBox.BorderStyle = BorderStyle.FixedSingle;
			editTextBox.LostFocus += new EventHandler(textBox_LostFocus);
			editTextBox.KeyUp += new KeyEventHandler(textBox_KeyUp);
			editTextBox.TextChanged += new EventHandler(editTextBox_TextChanged);

			HideNameColumnText();
			this.listView.Controls.Add(editTextBox);
			editTextBox.Focus();
		}

		private void HideNameColumnText()
		{
			listView.SelectedItems[0].Text = "";
		}

		private void EndEditSelectedItemname()
		{
			editTextBox.Dispose();
			ShowNameColumnText();
			this.exRepListView.MessageHandler.ClearMessage();
		}

		private void ShowNameColumnText()
		{
			listView.SelectedItems[0].Text = ((FileSystemInfo)listView.SelectedItems[0].Tag).Name;
		}

		void textBox_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				FileSystemInfo fsi = GetSelectedFileSystemInfoList()[0];

				if (PathContainsInvalidChars(editTextBox.Text))
				{
					this.exRepListView.MessageHandler.AddMessage(String.Format("Entered name '{0}' contains invalid characters.", editTextBox.Text), MessagePanelColor.Error);
					return;
				}

				if (fsi is DirectoryInfo)
					Directory.Move(fsi.FullName, Path.Combine(Path.GetDirectoryName(fsi.FullName), editTextBox.Text));
				else
					File.Move(fsi.FullName, Path.Combine(Path.GetDirectoryName(fsi.FullName), editTextBox.Text));

				EndEditSelectedItemname();
			}
			else if (e.KeyCode == Keys.Escape)
				EndEditSelectedItemname();
		}

		private bool PathContainsInvalidChars(string path)
		{
			foreach (char ch in Path.GetInvalidPathChars())
			{
				if (path.Contains(ch))
					return true;
			}

			return false;
		}

		void editTextBox_TextChanged(object sender, EventArgs e)
		{
			editTextBox.Width = GetTextWidth(editTextBox.Text, editTextBox.Font) + 5;
		}

		private int GetTextWidth(string text, Font font)
		{
			SizeF textSize = this.CreateGraphics().MeasureString(text, font);

			return (int)textSize.Width;
		}

		void textBox_LostFocus(object sender, EventArgs e)
		{
			EndEditSelectedItemname();
		}

		private int GetNameColumnIndex()
		{
			return this.exRepListView.GetColumn(typeof(ExRepListViewColumnName)).Index;
		}
	}
}
