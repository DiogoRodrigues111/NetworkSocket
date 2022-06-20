using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

/// <summary>
/// Server, is work client to server always.
/// 
/// And receive message from client.
/// 
/// ...
/// </summary>
static void StartServer()
{
	IPHostEntry host = Dns.GetHostEntry("localhost");
	IPAddress addr = host.AddressList[0];
	IPEndPoint end = new IPEndPoint(addr, 1234);
	Socket socket = new Socket(addr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
	Socket handler;
	Int32 receive;
	string data;
	byte[] msg;
	byte[] buffer = Array.Empty<byte>();
	byte[] status = Array.Empty<byte>();

	socket.Bind(end);	// end point using bind.
	socket.Listen(10);  // ten request for a time.

	Console.WriteLine("Wait to connection ...");

	handler = socket.Accept();

	while (true)
	{
		receive = handler.Receive(status);
		data = Encoding.UTF8.GetString(status);

		// check if read end line of the word.
		if (data.IndexOf("<EOF>") > -1) break;
	}

	Console.WriteLine("Receiving text: {0}", data);

	msg = Encoding.UTF8.GetBytes(data);

	handler.Send(msg);
	handler.Shutdown(SocketShutdown.Both);
	handler.Close();

	Console.WriteLine("Press Any Key to Exit.");
	Console.ReadKey();
}

static void Main(string[] args)
{
	StartServer();
}