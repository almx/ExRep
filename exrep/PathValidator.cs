using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.IO;

namespace ExRep
{
	public static class PathValidator
	{
		//[DllImport("shlwapi.dll", CharSet = CharSet.Unicode)]
		//[ResourceExposure(ResourceScope.None)]
		//[return: MarshalAsAttribute(UnmanagedType.Bool)]
		//private static extern bool PathIsUNC([MarshalAsAttribute(UnmanagedType.LPWStr), In] string pszPath);

		public static bool IsPathUNCServerRoot(string path)
		{
			path = path.TrimEnd('\\');

			return (path.StartsWith(@"\\") && path.LastIndexOf(@"\") == 1);
		}

		public static bool IsPathLogicalDriveRoot(string path)
		{
			if (path.EndsWith(":"))
				path += @"\";

			return Environment.GetLogicalDrives().Contains<string>(path.ToUpper());
		}
	}
}
