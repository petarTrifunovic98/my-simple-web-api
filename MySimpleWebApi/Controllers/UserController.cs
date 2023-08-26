using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using MySimpleDatabaseDriver;

namespace my_simple_web_api.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    private readonly IRepositoryWrapper _repository;
    private readonly ILoggerManager _logger;

    public UserController(IRepositoryWrapper repository, ILoggerManager logger)
    {
        _repository = repository;
        _logger = logger;
    }

    [HttpGet]
    [Route("all")]
    public async Task<ActionResult> GetAllUsers()
    {
        var users = await _repository.UserRepository.FindAll();
        return Ok(users);
    }

    [HttpGet]
    [Route("{userId}")]
    public async Task<ActionResult> GetUser(int userId)
    {
        var user = await _repository.UserRepository.FindByID(userId);
        return Ok(user);
    }

    [HttpPost]
    [Route("add-user")]
    public async Task<ActionResult> AddUser([FromBody] User user)
    {
        await _repository.UserRepository.Create(user);
        return Ok();
    }
}