using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

/// <summary>
///	StartClient is turn on send message to server.
/// </summary>
static void StartClient()
{
	IPHostEntry host = Dns.GetHostEntry("localhost");
	IPAddress addr = host.AddressList[0];
	IPEndPoint end = new IPEndPoint(addr, 1234);
	Socket socket = new Socket(addr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
	Int32 receive, send;
	byte[] msg;
	byte[] status = Array.Empty<byte>();

	socket.Connect(end);

	Console.WriteLine("Socket connected of {0}", socket.RemoteEndPoint);

	msg = Encoding.UTF8.GetString("This is a Test <EOF>");
	send = socket.Send(msg);
	receive = socket.Receive(status);

	Console.WriteLine("Echo: {0}", Encoding.UTF8.GetString(msg));

	socket.Shutdown(SocketShutdown.Both);
	socket.Close();
}

static void Main(string[] args)
{
	StartClient();
}