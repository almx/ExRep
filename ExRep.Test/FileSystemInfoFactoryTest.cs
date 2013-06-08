using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.IO;

namespace ExRep.Test
{
	[TestFixture]
	public class FileSystemInfoFactoryTest
	{
		[TestCase]
		public void GetDirectoryInfoTest()
		{
			DirectoryInfo di = FileSystemInfoCreator.GetDirectoryInfo(Paths.TestDirectoryStatic);
			Assert.AreEqual("static", di.Name);
			Assert.AreEqual("exrep", di.Parent.Name);
		}

		[TestCase]
		public void GetDirectoryInfoListTest()
		{
			List<DirectoryInfo> list = FileSystemInfoCreator.GetSubDirectoryInfoList(Paths.TestDirectoryStatic);
			Assert.AreEqual(2, list.Count);
			Assert.AreEqual("subdir1", list[0].Name);
			Assert.AreEqual("subdir2", list[1].Name);
		}

		[TestCase]
		public void GetFileInfoTest()
		{
			FileInfo fi = FileSystemInfoCreator.GetFileInfo(Paths.TestTextFile);
			Assert.AreEqual("test.txt", fi.Name);
			Assert.AreEqual(Paths.TestTextFile, fi.FullName);
			Assert.AreEqual(12, fi.Length);
		}

		[TestCase]
		public void GetFileInfoListTest()
		{
			List<FileInfo> list = FileSystemInfoCreator.GetFileInfoList(Paths.TestDirectoryStatic);
			Assert.AreEqual(5, list.Count);
			Assert.AreEqual("bmw.doc", list[0].Name);

		}
	}
}
