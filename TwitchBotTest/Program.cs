using System;
using System.Threading;

using TwitchBot;

namespace TwitchBotTest
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Console.WriteLine("TwitchBotTest Start");

			Console.WriteLine("Connecting ...");

			TwitchClient client = new TwitchClient();
			client.Connect("ereldev", "oauth:4gjpks0n9fl9lxch0z1xegklbqdjcs", "thegaarnik");

			Console.WriteLine("Connected !");

			while(client.IsConnected)
			{
				AbstractMessage message = client.NextMessage();
				if(message != null)
				{
					switch(message.GetMessageType())
					{
					case AbstractMessage.MessageType.CHAT:
						ChatMessage chatMessage = (ChatMessage) message;
						Console.WriteLine(chatMessage.User + ": " + chatMessage.Text);
						break;
					case AbstractMessage.MessageType.WHISPER:
						WhisperMessage whisperMessage = (WhisperMessage) message;
						Console.WriteLine(whisperMessage.FromUser + " -> " + whisperMessage.ToUser + ": " + whisperMessage.Text);
						break;
					}
				}
			}

			Console.WriteLine("TwitchBotTest End");
		}

	}
}
