using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volcanion.Core.Presentation.Controllers;
using Volcanion.Medical.Handlers.Abstractions;
using Volcanion.Medical.Models.Request;

namespace Volcanion.Medical.Presentation.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[ApiVersion("1.0")]
[AllowAnonymous]
public class AuthController : BaseController
{
    /// <summary>
    /// ILogger instance
    /// </summary>
    private readonly ILogger<AuthController> _logger;

    /// <summary>
    /// IAccountHandler instance
    /// </summary>
    private readonly IAccountHandler _accountHandler;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="accountHandler"></param>
    public AuthController(ILogger<AuthController> logger, IAccountHandler accountHandler)
    {
        _logger = logger;
        _accountHandler = accountHandler;
    }

    /// <summary>
    /// Register
    /// </summary>
    /// <param name="account"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register(AccountRegister account)
    {
        var result = await _accountHandler.Register(account);
        return Ok(SuccessData(result));
    }

    /// <summary>
    /// Login
    /// </summary>
    /// <param name="account"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login(AccountLogin account)
    {
        var result = await _accountHandler.Login(account);
        if (result == null) return BadRequest(ErrorMessage("Invalid username or password!"));
        return Ok(SuccessData(result));
    }

    /// <summary>
    /// RefreshToken
    /// </summary>
    /// <param name="account"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("refresh-token")]
    public async Task<IActionResult> RefreshToken(TokenRequest request)
    {
        var result = await _accountHandler.RefreshToken(request);
        return Ok(SuccessData(result));
    }
}
