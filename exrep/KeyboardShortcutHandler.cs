using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ExRep
{
	public struct KeyboardCombo
	{
		public Keys Key;
		public bool Alt;
		public bool Control;
		public bool Shift;

		public KeyboardCombo(Keys key, bool alt, bool control, bool shift)
		{
			Key = key;
			Alt = alt;
			Control = control;
			Shift = shift;
		}

		public KeyboardCombo(Keys key)
		{
			Key = key;
			Alt = false;
			Control = false;
			Shift = false;
		}
	}

	public enum ProgramAction
	{
		MarkItemsForCopy,
		MarkItemsForMove,
		NavigateUp,
		OpenItem,
		RefreshList,
		RenameItem
	}

	public class KeyboardShortcutHandler
	{
		private Dictionary<KeyboardCombo, ProgramAction> shortCutActionDict { get; set; }
		
		public KeyboardShortcutHandler(Dictionary<KeyboardCombo, ProgramAction> shortCutActionDict)
		{
			this.shortCutActionDict = shortCutActionDict;
		}

		public ProgramAction GetAction(KeyEventArgs args)
		{
			KeyboardCombo combo = new KeyboardCombo();
			combo.Key = args.KeyCode;
			combo.Alt = args.Alt;
			combo.Control = args.Control;
			combo.Shift = args.Shift;

			if (shortCutActionDict.ContainsKey(combo))
				return shortCutActionDict[combo];
			else
				throw new KeyboardShortcutNotRecognizedException("Keyboard combo not recognized");
		}
	}
}
