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
}