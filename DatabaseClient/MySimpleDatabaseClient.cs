
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace my_simple_web_api.DatabaseClient;

public class MySimpleDatabaseClient<T>
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


    public async Task<IEnumerable<T>> IssueSelectCommand(string command)
    {
        var commandBytes = Encoding.UTF8.GetBytes(command);
        await _client.SendAsync(commandBytes, SocketFlags.None);
        Console.WriteLine($"Socket client sent message: \"{command}\"");

        var response = "";
        while (response.Length < 3)
        {
            var buffer = new byte[10000];
            var received = await _client.ReceiveAsync(buffer, SocketFlags.None);
            response = Encoding.UTF8.GetString(buffer, 0, received);
            Console.WriteLine($"Socket client received: \"{response}\"");
            var list = JsonSerializer.Deserialize<List<T>>(response);
            list.ForEach(el => Console.WriteLine(el));
            return list;
        }
        return null;
    }

    public async Task<T> IssueSelectOneCommand(string command)
    {
        var commandBytes = Encoding.UTF8.GetBytes(command);
        await _client.SendAsync(commandBytes, SocketFlags.None);
        Console.WriteLine($"Socket client sent message: \"{command}\"");

        var response = "";
        var buffer = new byte[10000];
        var received = await _client.ReceiveAsync(buffer, SocketFlags.None);
        response = Encoding.UTF8.GetString(buffer, 0, received);
        Console.WriteLine($"Socket client received: \"{response}\"");
        var result = JsonSerializer.Deserialize<T>(response);
        Console.WriteLine(result);
        return result;
    }

    public async Task IssueInsertCommand(string command)
    {
        var commandBytes = Encoding.UTF8.GetBytes(command);
        await _client.SendAsync(commandBytes, SocketFlags.None);
        Console.WriteLine($"Socket client sent message: \"{command}\"");
    }

    public async Task nesto(int asd) {
        var petar = "aaaa";
        
    }
}
