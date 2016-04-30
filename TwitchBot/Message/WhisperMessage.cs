using System;

namespace TwitchBot
{
	public class WhisperMessage: AbstractMessage
	{
		// ************************************************************************************************

		// ************************************************************************************************
		public string FromUser { get; private set; }
		public string ToUser { get; private set; }
		public string Text { get; private set; }

		// ************************************************************************************************
		public WhisperMessage ()
		{
		}

		// ************************************************************************************************
		public override void Parse (string[] parts)
		{
			this.FromUser = this.ParseUser(parts[0]);
			this.ToUser = parts[2];
			this.Text = this.ParseText(parts, 3);
		}

		// ************************************************************************************************

		// ************************************************************************************************

		// ************************************************************************************************
		public override MessageType GetMessageType ()
		{
			return MessageType.WHISPER;
		}

	}
}

