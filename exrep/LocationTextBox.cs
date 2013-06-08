using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ExRep
{
	public class LocationTextBox
	{
		private delegate void SetTextDelegate(string text);
		
		private TextBox locationBox;

		public LocationTextBox(TextBox textBox)
		{
			this.locationBox = textBox;
		}

		public string GetText()
		{
			return this.locationBox.Text;
		}

		public void SetText(string text)
		{
			if(this.locationBox.InvokeRequired)
				this.locationBox.Invoke(new SetTextDelegate(SetText), text);
			else
			{
				this.locationBox.Text = text;
			}
		}
	}
}
