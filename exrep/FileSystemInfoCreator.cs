using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ExRep
{
	public static class FileSystemInfoCreator
	{
		public static DirectoryInfo GetDirectoryInfo(string directoryPath)
		{
			return new DirectoryInfo(directoryPath);
		}

		public static List<DirectoryInfo> GetSubDirectoryInfoList(string directoryPath)
		{
			List<DirectoryInfo> list = new List<DirectoryInfo>();

			foreach (string directoryName in Directory.GetDirectories(directoryPath))
				list.Add(GetDirectoryInfo(Path.Combine(directoryPath, directoryName)));

			return list;
		}

		public static FileInfo GetFileInfo(string filePath)
		{
			return new FileInfo(filePath);
		}

		public static List<FileInfo> GetFileInfoList(string directoryPath)
		{
			List<FileInfo> files = new List<FileInfo>();

			foreach (string filename in Directory.GetFiles(directoryPath))
				files.Add(GetFileInfo(Path.Combine(directoryPath, filename)));

			return files;
		}
	}
}
