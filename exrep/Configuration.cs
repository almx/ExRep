using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Text;

namespace ExRep
{
	public static class Configuration
	{
		public static bool SortDirectoriesOnTop
		{
			get { return true; }
		}

		public static Dictionary<KeyboardCombo, ProgramAction> GetKeyboardShortcutMapping()
		{
			Dictionary<KeyboardCombo, ProgramAction> keyboardShortcutMapping = new Dictionary<KeyboardCombo, ProgramAction>();

			// todo: read from file instead, in the format "Ctrl+Alt+C"
			keyboardShortcutMapping.Add(new KeyboardCombo(Keys.C, false, true, false), ProgramAction.MarkItemsForCopy);
			keyboardShortcutMapping.Add(new KeyboardCombo(Keys.X, false, true, false), ProgramAction.MarkItemsForMove);
			keyboardShortcutMapping.Add(new KeyboardCombo(Keys.Back), ProgramAction.NavigateUp);
			keyboardShortcutMapping.Add(new KeyboardCombo(Keys.Enter), ProgramAction.OpenItem);
			keyboardShortcutMapping.Add(new KeyboardCombo(Keys.F5), ProgramAction.RefreshList);
			keyboardShortcutMapping.Add(new KeyboardCombo(Keys.F2), ProgramAction.RenameItem);

			return keyboardShortcutMapping;
		}

		public static List<ExRepListViewColumn> GetColumnCollection()
		{
			List<ExRepListViewColumn> columns = new List<ExRepListViewColumn>();

			// Name column is required and should always be added
			ExRepListViewColumnName colName = new ExRepListViewColumnName();
			colName.Index = 0;
			colName.Width = 300;
			colName.Presenter = new ExRepListViewColumnNamePresenterDefault();
			columns.Add(colName);

			// todo: read from config file
			ExRepListViewColumnSize colSize = new ExRepListViewColumnSize();
			colSize.Index = 1;
			colSize.Width = 85;
			colSize.Alignment = HorizontalAlignment.Right;
			colSize.Presenter = new ExRepListViewColumnSizePresenterThousandSeparated();
			columns.Add(colSize);

			ExRepListViewColumnDateModified colDateModified = new ExRepListViewColumnDateModified();
			colDateModified.Index = 2;
			colDateModified.Width = 120;
			colDateModified.Presenter = new ExRepListViewColumnDateModifiedPresenterSortablePattern();
			columns.Add(colDateModified);

			ExRepListViewColumnExtension colExtension = new ExRepListViewColumnExtension();
			colExtension.Index = 4;
			colExtension.Width = 50;
			colExtension.Presenter = new ExRepListViewColumnExtensionPresenterLowerCase();
			columns.Add(colExtension);

			ExRepListViewColumnAttributes colAttributes = new ExRepListViewColumnAttributes();
			colAttributes.Index = 3;
			colAttributes.Width = 80;
			colAttributes.Presenter = new ExRepListViewColumnAttributesPresenterDefault();
			colAttributes.FontFamily = new FontFamily(GenericFontFamilies.Monospace);
			columns.Add(colAttributes);

			return columns;
		}
	}
}
