using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ExRep
{
	public static class AttributeStringCreator
	{
		public static string GetAttributesString(FileAttributes fileAttributes)
		{
			StringBuilder sbAttributes = new StringBuilder();

			sbAttributes.Append(GetAttributeLetter(fileAttributes, FileAttributes.Archive, "A"));
			sbAttributes.Append(GetAttributeLetter(fileAttributes, FileAttributes.Compressed, "C"));
			sbAttributes.Append(GetAttributeLetter(fileAttributes, FileAttributes.Encrypted, "E"));
			sbAttributes.Append(GetAttributeLetter(fileAttributes, FileAttributes.Hidden, "H"));
			sbAttributes.Append(GetAttributeLetter(fileAttributes, FileAttributes.ReadOnly, "R"));
			sbAttributes.Append(GetAttributeLetter(fileAttributes, FileAttributes.System, "S"));

			return sbAttributes.ToString();
		}

		private static string GetAttributeLetter(FileAttributes fileAttributes, FileAttributes attributeToSearch, string attributeLetter)
		{
			if ((fileAttributes & attributeToSearch) == attributeToSearch)
				return attributeLetter;
			else
				return "-";
		}
	}
}
