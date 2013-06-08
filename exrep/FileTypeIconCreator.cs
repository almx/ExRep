using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace ExRep
{
	public class FileTypeIconInfo
	{
		/// <summary>
		/// Often the file's extension; the full path for .exe/.lnk files; special GUIDs for directories and files with no extension
		/// </summary>
		public string LookupString { get; set; }
		public Icon IconLarge { get; set; }
		public Icon IconSmall { get; set; }
	}

	public static class FileTypeIconCreator
	{
		[DllImport("shell32.dll")]
		public static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbSizeFileInfo, uint uFlags);

		private const uint SHGFI_ICON = 0x100;
		private const uint SHGFI_LARGEICON = 0x0;
		private const uint SHGFI_SMALLICON = 0x1;

		[StructLayout(LayoutKind.Sequential)]
		public struct SHFILEINFO
		{
			public IntPtr hIcon;
			public IntPtr iIcon;
			public uint dwAttributes;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
			public string szDisplayName;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
			public string szTypeName;
		};

		public static FileTypeIconInfo GetIconInfo(string path, string iconLookupString)
		{
			FileTypeIconInfo iconInfo = new FileTypeIconInfo();
			iconInfo.LookupString = iconLookupString;
			
			try	{ iconInfo.IconSmall = GetIcon(path, SHGFI_SMALLICON); }
			catch (ArgumentException)
			{
				iconInfo.IconSmall = GetIcon(path, SHGFI_SMALLICON);
			}

			try { iconInfo.IconLarge = GetIcon(path, SHGFI_LARGEICON); }
			catch (ArgumentException)
			{
				iconInfo.IconLarge = GetIcon(path, SHGFI_LARGEICON);
			}

			return iconInfo;
		}

		private static Icon GetIcon(string path, uint sizeDef)
		{
			SHFILEINFO shinfo = new SHFILEINFO();

			SHGetFileInfo(path, 0, ref shinfo, (uint)Marshal.SizeOf(shinfo), SHGFI_ICON | sizeDef);

			if (shinfo.hIcon == IntPtr.Zero)
				return null;
			else
				return Icon.FromHandle(shinfo.hIcon);
		}
	}
}
