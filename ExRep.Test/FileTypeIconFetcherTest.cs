using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Drawing;
using System.Drawing.Imaging;
using Microsoft.Win32;
using System.IO;

namespace ExRep.Test
{
	[TestFixture]
	public class FileTypeIconFetcherTest
	{
		[TestCase]
		public void GetIconInfoFromFileTest()
		{
			FileTypeIconInfo iconInfo = FileTypeIconCreator.GetIconInfo(Path.Combine(Paths.TestDirectoryStatic, "test.txt"), "txt");
			Assert.IsNotNull(iconInfo);
			Assert.AreEqual("txt", iconInfo.LookupString);
			
			Assert.IsNotNull(iconInfo.IconLarge);
			Assert.AreEqual(new Size(32, 32), iconInfo.IconLarge.Size);

			Assert.IsNotNull(iconInfo.IconSmall);
			Assert.AreEqual(new Size(16, 16), iconInfo.IconSmall.Size);
		}
	}
}
