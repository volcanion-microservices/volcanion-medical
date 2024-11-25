using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volcanion.Core.Models.Attributes;
using Volcanion.Core.Presentation.Controllers;
using Volcanion.Medical.Handlers.Abstractions;
using Volcanion.Medical.Models.Entities.Identity;
using Volcanion.Medical.Models.Request.DTOs;
using Volcanion.Medical.Models.Response.BOs;

namespace Volcanion.Medical.Presentation.Controllers;

/// <summary>
/// GrantPermissionController
/// </summary>
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[ApiVersion("1.0")]
[AllowAnonymous]
public class GrantPermissionController : BaseController
{
    /// <summary>
    /// IGrantPermissionHandler instance
    /// </summary>
    private readonly IGrantPermissionHandler _grantPermissionHandler;

    /// <summary>
    /// IMapper instance
    /// </summary>
    private readonly IMapper _mapper;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="GrantPermissionHandler"></param>
    /// <param name="mapper"></param>
    public GrantPermissionController(IGrantPermissionHandler GrantPermissionHandler, IMapper mapper)
    {
        _grantPermissionHandler = GrantPermissionHandler;
        _mapper = mapper;
    }

    /// <summary>
    /// CreateAsync
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> CreateAsync(GrantPermissionRequestDTO request)
    {
        var GrantPermission = _mapper.Map<GrantPermission>(request);
        var result = await _grantPermissionHandler.CreateAsync(GrantPermission);
        return Ok(SuccessData(result));
    }

    /// <summary>
    /// UpdateAsync
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<IActionResult> UpdateAsync(GrantPermissionRequestDTO request)
    {
        var GrantPermission = _mapper.Map<GrantPermission>(request);
        var result = await _grantPermissionHandler.UpdateAsync(GrantPermission);
        return Ok(SuccessData(result));
    }

    /// <summary>
    /// DeleteAsync
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var result = await _grantPermissionHandler.SoftDeleteAsync(id);
        return Ok(SuccessData(result));
    }

    /// <summary>
    /// HardDeleteAsync
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("hard-delete/{id}")]
    public async Task<IActionResult> HardDeleteAsync(Guid id)
    {
        var result = await _grantPermissionHandler.DeleteAsync(id);
        return Ok(SuccessData(result));
    }

    /// <summary>
    /// GetAllAsync
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var result = await _grantPermissionHandler.GetAllAsync();
        return Ok(SuccessData(result));
    }

    /// <summary>
    /// GetByIdAsync
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        var result = await _grantPermissionHandler.GetAsync(id);
        return Ok(SuccessData(result));
    }
}
