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
        byte[] buffer = new byte[1024];
        int bytes = await client.Client.ReceiveAsync(buffer);
        Console.WriteLine("buffer : " + Encoding.ASCII.GetString(buffer));
        var requestData = Encoding.ASCII.GetString(buffer).Split("\r\n");
        string responseString = "";

        if (requestData.Length > 2)
        {
            string request = requestData[2].ToLower();

            switch (request)
            {
                case "ping":
                    responseString = "+PONG\r\n";
                    break;
                case "echo":
                    responseString = $"${requestData[4].Length}\r\n{requestData[4]}\r\n";
                    break;
            }
        }

        await client.Client.SendAsync(Encoding.UTF8.GetBytes(responseString));
    }
}

string HandleCommand(byte[] buffer)
{
    RESP rESP = new RESP();
    return rESP.ParseCommand(buffer);
}