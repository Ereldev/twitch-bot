﻿using System;

namespace TwitchBot
{
	public class ChatMessage: AbstractMessage
	{
		// ************************************************************************************************

		// ************************************************************************************************
		public string User { get; private set; }
		public string Text { get; private set; }

		// ************************************************************************************************
		public ChatMessage ()
		{
		}

		// ************************************************************************************************
		public override void Parse (string[] parts)
		{
			this.User = this.ParseUser(parts[0]);
			this.Text = this.ParseText(parts, 3);
		}

		// ************************************************************************************************

		// ************************************************************************************************

		// ************************************************************************************************
		public override MessageType GetMessageType ()
		{
			return MessageType.CHAT;
		}

	}
}
