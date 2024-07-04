using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace my_simple_web_api.Controllers;

[ApiController]
[Route("api/admin")]
public class AdminController : ControllerBase
{
    private readonly IRepositoryWrapper _repository;
    private readonly ILoggerManager _logger;

    public AdminController(IRepositoryWrapper repository, ILoggerManager logger)
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
    [Route("exit")]
    public async Task<ActionResult> Exit()
    {
        await _repository.UserRepository.Create(user);
        return Ok();
    }
}