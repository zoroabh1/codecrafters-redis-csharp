using codecrafters_redis.src;
using System.Net;
using System.Net.Sockets;
using System.Text;

// You can use print statements as follows for debugging, they'll be visible when running tests.
Console.WriteLine("Logs from your program will appear here!");

// Uncomment this block to pass the first stage
TcpListener server = new TcpListener(IPAddress.Any, 6379);
server.Start();
while (true)
{
    TcpClient client = server.AcceptTcpClient(); // wait for client
    Task.Run(async () => await HandleRequest(client));
}


async Task HandleRequest(TcpClient client)
{
    while (client.Connected)
    {
        var buffer = new byte[1024];
        int size = await client.Client.ReceiveAsync(buffer);
        if (size == 0)
        {
            continue;
        }
        var request = Encoding.ASCII.GetString(buffer);
        //Console.WriteLine("request : "+request);
        client.Client.Send(Encoding.ASCII.GetBytes(HandleCommand(buffer)));
    }
}

string HandleCommand(byte[] buffer)
{
    RESP rESP = new RESP();
    return rESP.ParseCommand(buffer);
}