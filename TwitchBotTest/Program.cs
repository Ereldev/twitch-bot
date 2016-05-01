using System;
using System.Configuration;
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

			string username = ConfigurationManager.AppSettings["username"];
			string password = ConfigurationManager.AppSettings["password"];
			string channel = ConfigurationManager.AppSettings["channel"];

			TwitchClient client = new TwitchClient();
			client.Connect(username, password, channel);

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

						client.WriteWhisperMessage(whisperMessage.FromUser, "Your message was: " + whisperMessage.Text);
						break;
					}
				}
			}

			Console.WriteLine("TwitchBotTest End");
		}

	}
}
