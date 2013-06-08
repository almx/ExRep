using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.IO;

namespace ExRep
{
	public class FileTypeIconHandler
	{
		public Dictionary<FileTypeIconInfo, int> IconInfoImageListIndexDict { get; set; }
		public ImageList ImageListLargeIcons { get; set; }
		public ImageList ImageListSmallIcons { get; set; }

		public FileTypeIconHandler(IContainer container)
		{
			this.IconInfoImageListIndexDict = new Dictionary<FileTypeIconInfo, int>();

			this.ImageListLargeIcons = new ImageList();
			this.ImageListSmallIcons = new ImageList();

			this.ImageListLargeIcons.ImageSize = SystemInformation.IconSize;
			this.ImageListLargeIcons.ColorDepth = ColorDepth.Depth32Bit;
			this.ImageListSmallIcons.ImageSize = SystemInformation.SmallIconSize;
			this.ImageListSmallIcons.ColorDepth = ColorDepth.Depth32Bit;
		}

		public FileTypeIconInfo GetIconInfoFromDictionary(string iconLookupString)
		{
			foreach (FileTypeIconInfo info in this.IconInfoImageListIndexDict.Keys)
				if (info.LookupString == iconLookupString)
					return info;

			return null;
		}

		public FileTypeIconInfo AddIconToImageList(string path, string iconLookupString)
		{
			FileTypeIconInfo iconInfo = GetIconInfoFromDictionary(iconLookupString);

			if (iconInfo == null)
			{
				iconInfo = FileTypeIconCreator.GetIconInfo(path, iconLookupString);

				if (iconInfo.IconSmall != null && iconInfo.IconLarge != null)
				{
					this.ImageListLargeIcons.Images.Add(iconInfo.IconLarge);
					this.ImageListSmallIcons.Images.Add(iconInfo.IconSmall);

					this.IconInfoImageListIndexDict.Add(iconInfo, this.ImageListLargeIcons.Images.Count - 1);
				}
				else
					return null;
			}

			return iconInfo;
		}

		public static string GetIconLookupString(string path)
		{
			string fileExtension;

			if (File.Exists(path))
			{
				if (path.Contains('.'))
				{
					fileExtension = path.Substring(path.LastIndexOf('.') + 1).ToLower();

					if (fileExtension == "exe" || fileExtension == "lnk")
						fileExtension = path;
				}
				else
					fileExtension = Constants.FileWithoutExtensionTypeGuid;
			}
			else if (Directory.Exists(path))
				fileExtension = Constants.DirectoryTypeGuid;
			else
				throw new Exception("path is neither file nor directory: " + path);

			return fileExtension;
		}
	}
}
