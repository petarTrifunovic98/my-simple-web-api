
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace my_simple_web_api.DatabaseClient;

public class MySimpleDatabaseClient
{

    private readonly Socket _client;

    public MySimpleDatabaseClient(string hostName, int port)
    {
        IPHostEntry ipHostInfo = Dns.GetHostEntry(hostName);
        IPAddress ipAddress = ipHostInfo.AddressList[0];
        IPEndPoint ipEndPoint = new(ipAddress, port);

        _client = new(ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        _client.Connect(ipEndPoint);
    }


    public async void IssueSelectCommand(string command) 
    {
        var commandBytes = Encoding.UTF8.GetBytes(command);
        await _client.SendAsync(commandBytes, SocketFlags.None);
        Console.WriteLine($"Socket client sent message: \"{command}\"");

        var response = "";
        while (response.Length < 3 || !response.EndsWith("end\n")) {
            var buffer = new byte[1024];
            var received = await _client.ReceiveAsync(buffer, SocketFlags.None);
            // var received = _client.Receive(buffer, SocketFlags.None);
            response = Encoding.UTF8.GetString(buffer, 0, received);
            Console.WriteLine($"Socket client received: \"{response}\"");
        }
        Console.WriteLine("Ending...");
    }
}