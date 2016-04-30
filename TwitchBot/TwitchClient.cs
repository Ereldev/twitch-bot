using System;
using System.Threading;
using System.Collections.Generic;

namespace TwitchBot
{
	public class TwitchClient
	{
		// ************************************************************************************************
		public const string DEFAULT_HOST = "irc.chat.twitch.tv";
		public const int 	DEFAULT_PORT = 6667;

		private const string APP_HOST = "tmi.twitch.tv";

		// ************************************************************************************************
		public bool IsConnected { get { return this.irc.IsConnected; } }

		private IRCClient irc;

		private Queue<AbstractMessage> messages;

		// ************************************************************************************************
		public TwitchClient(string host, int port)
		{
			this.irc = new IRCClient(host, port);

			this.messages = new Queue<AbstractMessage>();
		}

		public TwitchClient (): this(DEFAULT_HOST, DEFAULT_PORT)
		{
		}

		// ************************************************************************************************
		public void Connect(string username, string password, string channel)
		{
			this.irc.Connect(username, password);
			this.irc.Join(channel);
			this.irc.WriteIRCMessage("CAP REQ :twitch.tv/membership");
			this.irc.WriteIRCMessage("CAP REQ :twitch.tv/commands");

			Thread thread = new Thread(new ThreadStart(ReadThread));
			thread.Start();
		}

		public void Disconnect()
		{
			this.irc.Disconnect();
		}

		public AbstractMessage NextMessage()
		{
			lock(this.messages)
			{
				if(this.messages.Count > 0)
					return this.messages.Dequeue();
				else
					return null;
			}
		}

		// ************************************************************************************************
		private void ReadThread()
		{
			while(this.IsConnected == true)
			{
				string text = this.irc.ReadIRCMessage();
				string[] parts = text.Split(' ');
				AbstractMessage message = null;

				if(text.Contains("PRIVMSG"))
					message = new ChatMessage();
				else if(text.Contains("WHISPER"))
					message = new WhisperMessage();
				else if(text.Contains("PING"))
					this.irc.WriteIRCMessage("PONG :" + APP_HOST);

				if(message != null)
				{
					message.Parse(parts);

					lock(this.messages)
					{
						this.messages.Enqueue(message);
					}	
				}
			}
		}

		// ************************************************************************************************

	}
}

