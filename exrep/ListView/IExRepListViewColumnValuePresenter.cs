using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ExRep
{
	public interface IExRepListViewColumnValuePresenter
	{
		string GetPresentation(FileSystemInfo fsi);
	}
}
