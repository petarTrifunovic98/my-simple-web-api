using Microsoft.AspNetCore.Mvc;
using my_simple_web_api.DatabaseClient;

namespace my_simple_web_api.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    private readonly MySimpleDatabaseClient<Row> _mySimpleDatabaseClient;

    public UserController(MySimpleDatabaseClient<Row> mySimpleDatabaseClient)
    {
        _mySimpleDatabaseClient = mySimpleDatabaseClient;
    }

    [HttpGet]
    [Route("all")]
    public async Task<ActionResult> GetAllUsers()
    {
        var rows = await _mySimpleDatabaseClient.IssueSelectCommand("select");
        return Ok(rows);
    }

    [HttpGet]
    [Route("{userId}")]
    public async Task<ActionResult> GetUser(int userId)
    {
        var user = await _mySimpleDatabaseClient.IssueSelectOneCommand("selectOne " + userId.ToString());
        return Ok(user);
    }

    [HttpPost]
    [Route("add-user")]
    public async Task<ActionResult> AddUser([FromBody] Row user) 
    {
        var addUserCommand = "insert " + user.id + " " + user.username + " " + user.email;
        await _mySimpleDatabaseClient.IssueInsertCommand(addUserCommand);
        return Ok(); 
    }
}