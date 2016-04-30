using System;

namespace TwitchBot
{
	public abstract class AbstractMessage
	{
		// ************************************************************************************************
		public enum MessageType
		{
			CHAT, WHISPER
		};

		// ************************************************************************************************

		// ************************************************************************************************
		public AbstractMessage ()
		{
		}

		// ************************************************************************************************
		public abstract void Parse(string[] parts);
		public abstract MessageType GetMessageType();

		// ************************************************************************************************
		protected string ParseUser(string str)
		{
			int start = str.IndexOf(":");
			int stop = str.IndexOf("!");
			if( start == - 1 || stop == -1)
				return null;

			return str.Substring(start + 1, stop - 1 - start);
		}

		protected string ParseText(string[] parts, int start)
		{
			string text = parts[start].Substring(1);

			if(parts.Length > start+1)
			{
				for(int i=start+1; i<parts.Length;i++)
					text += " " + parts[i];	
			}

			return text;
		}

		// ************************************************************************************************

		// ************************************************************************************************

	}
}

