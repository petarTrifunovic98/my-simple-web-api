
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace MySimpleDatabaseDriver;

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


    public async Task<IEnumerable<T>> IssueSelectCommand<T>()
    {
        var command = "select";
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
            Console.WriteLine(list);
            list.ForEach(el => Console.WriteLine(el));
            return list;
        }
        return null;
    }

    public async Task<T> IssueSelectOneCommand<T>(object id)
    {
        var command = "selectOne " + id.ToString();
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

    public async Task IssueInsertCommand<T>(T entity)
    {
        var command = "insert";
        Type t = entity.GetType();
        foreach (PropertyInfo pi in t.GetProperties())
        {
            command += " ";
            command += pi.GetValue(entity);
        }
        var commandBytes = Encoding.UTF8.GetBytes(command);
        await _client.SendAsync(commandBytes, SocketFlags.None);
        Console.WriteLine($"Socket client sent message: \"{command}\"");
    }
}
