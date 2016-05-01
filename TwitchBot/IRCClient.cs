using System;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace TwitchBot
{
	public class IRCClient
	{
		// ************************************************************************************************

		// ************************************************************************************************
		public string Host { get; private set; }
		public int Port { get; private set; }

		public string Username { get; private set; }
		public string Room { get; private set; }

		public bool IsConnected { get { return this.tcpClient.Connected; } }

		private TcpClient tcpClient;
		private StreamReader inputStream;
		private StreamWriter outputStream;

		// ************************************************************************************************
		public IRCClient (string host, int port)
		{
			this.Host = host;
			this.Port = port;
		}

		// ************************************************************************************************
		public void Connect(string username, string password)
		{
			this.tcpClient = new TcpClient(this.Host, this.Port);
			this.inputStream = new StreamReader(this.tcpClient.GetStream());
			this.outputStream = new StreamWriter(this.tcpClient.GetStream());

			this.WriteIRCMessage("PASS " + password);
			this.WriteIRCMessage("NICK " + username);
			this.WriteIRCMessage("USER " + username + " 8 * :" + username);

			this.Username = username;
		}

		public void Disconnect()
		{
			this.tcpClient.Close();
		}

		public void Join(string room)
		{
			this.WriteIRCMessage("JOIN #" + room);

			this.Room = room;
		}

		public void WriteChatMessage(string message, string appHost)
		{
			this.WriteIRCMessage(
				":" + this.Username + "!" + this.Username + "@" + this.Username + "." + appHost 
				+ " PRIVMSG #" + this.Room + " :" + message);
		}

		public void WriteWhisperMessage(string user, string message, string appHost)
		{
			this.WriteIRCMessage(
				":" + this.Username + "!" + this.Username + "@" + this.Username + "." + appHost 
				+ " PRIVMSG #" + this.Room + " :" + "/w " + user + " " + message);
		}

		public void WriteIRCMessage(string message)
		{
			this.outputStream.WriteLine(message);
			this.outputStream.Flush();
		}

		public string ReadIRCMessage()
		{
			return this.inputStream.ReadLine();
		}

		// ************************************************************************************************

		// ************************************************************************************************

		// ************************************************************************************************

	}
}

