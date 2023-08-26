using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using my_simple_web_api.DatabaseClient;

namespace my_simple_web_api.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    private readonly MySimpleDatabaseClient<User> _mySimpleDatabaseClient;
    private readonly ILoggerManager _logger;

    public UserController(MySimpleDatabaseClient<User> mySimpleDatabaseClient, ILoggerManager logger)
    {
        _mySimpleDatabaseClient = mySimpleDatabaseClient;
        _logger = logger;
    }

    [HttpGet]
    [Route("all")]
    public async Task<ActionResult> GetAllUsers()
    {
        _logger.LogInfo("Here is info message from the controller.");
        _logger.LogDebug("Here is debug message from the controller.");
        _logger.LogWarn("Here is warn message from the controller.");
        _logger.LogError("Here is error message from the controller.");

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
    public async Task<ActionResult> AddUser([FromBody] User user)
    {
        var addUserCommand = "insert " + user.id + " " + user.username + " " + user.email;
        await _mySimpleDatabaseClient.IssueInsertCommand(addUserCommand);
        return Ok();
    }
}