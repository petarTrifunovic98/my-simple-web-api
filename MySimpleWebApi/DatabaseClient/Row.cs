
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace my_simple_web_api.DatabaseClient;

public class Row
{
    public int id {get; set;}
    public string? username {get; set;}
    public string? email {get; set;}

    public override string ToString()
    {
        return String.Format("ID: {0}, Username: {1}, Email: {2}", id, username, email);
    }
}