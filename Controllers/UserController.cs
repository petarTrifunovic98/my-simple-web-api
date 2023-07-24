using Microsoft.AspNetCore.Mvc;
using my_simple_web_api.DatabaseClient;

namespace my_simple_web_api.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    private readonly MySimpleDatabaseClient _mySimpleDatabaseClient;

    public UserController(MySimpleDatabaseClient mySimpleDatabaseClient)
    {
        _mySimpleDatabaseClient = mySimpleDatabaseClient;
    }

    [HttpGet]
    [Route("all")]
    public void GetAllUsers()
    {
        _mySimpleDatabaseClient.IssueSelectCommand("select");
    }
}