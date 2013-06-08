using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace ExRep
{
	public enum MessagePanelColor
	{
		Message,
		Error
	}

	public class MessageHandler
	{
		private delegate void AddMessageDelegate(string text, MessagePanelColor	color);

		private Label messageLabel;

		public MessageHandler(Label messageLabel)
		{
			this.messageLabel = messageLabel;
		}

		public void ClearMessage()
		{
			this.messageLabel.Text = "";
		}

		public void AddMessage(string text, MessagePanelColor color)
		{
			if (this.messageLabel.InvokeRequired)
				this.messageLabel.Invoke(new AddMessageDelegate(AddMessage), text, color);
			else
			{
				this.messageLabel.Text = text;
				this.messageLabel.Font = new Font(this.messageLabel.Font, FontStyle.Bold);

				switch (color)
				{
					case MessagePanelColor.Error:
						this.messageLabel.ForeColor = Color.Firebrick;
						break;

					case MessagePanelColor.Message:
						this.messageLabel.ForeColor = Color.DarkGreen;
						break;
				}
			}
		}
	}
}
