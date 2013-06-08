using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExRep
{
	public class KeyboardShortcutNotRecognizedException : Exception
	{
		public KeyboardShortcutNotRecognizedException()
		{
		}
		
		public KeyboardShortcutNotRecognizedException(string message)
			: base(message)
		{
		}
		
		public KeyboardShortcutNotRecognizedException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
