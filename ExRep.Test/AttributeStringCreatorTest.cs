using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.IO;

namespace ExRep.Test
{
	[TestFixture]
	public class AttributeStringCreatorTest
	{
		[TestCase]
		public void GetAttributesStringTest()
		{
			FileInfo fi = new FileInfo(Path.Combine(Paths.TestDirectoryStatic, "SA Fiestacat Compilation.wmv"));

			Assert.AreEqual("A-EHR-", AttributeStringCreator.GetAttributesString(fi.Attributes));
		}
	}
}
