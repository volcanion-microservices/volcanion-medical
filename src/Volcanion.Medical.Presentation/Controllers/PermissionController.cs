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
/// PermissionController
/// </summary>
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[ApiVersion("1.0")]
[AllowAnonymous]
public class PermissionController : BaseController
{
    /// <summary>
    /// IPermissionHandler instance
    /// </summary>
    private readonly IPermissionHandler _permissionHandler;

    /// <summary>
    /// IMapper instance
    /// </summary>
    private readonly IMapper _mapper;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="permissionHandler"></param>
    /// <param name="mapper"></param>
    public PermissionController(IPermissionHandler permissionHandler, IMapper mapper)
    {
        _permissionHandler = permissionHandler;
        _mapper = mapper;
    }

    /// <summary>
    /// CreateAsync
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> CreateAsync(PermissionRequestDTO request)
    {
        var permission = _mapper.Map<Permission>(request);
        var result = await _permissionHandler.CreateAsync(permission);
        return Ok(SuccessData(result));
    }

    /// <summary>
    /// UpdateAsync
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<IActionResult> UpdateAsync(PermissionRequestDTO request)
    {
        var permission = _mapper.Map<Permission>(request);
        var result = await _permissionHandler.UpdateAsync(permission);
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
        var result = await _permissionHandler.SoftDeleteAsync(id);
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
        var result = await _permissionHandler.DeleteAsync(id);
        return Ok(SuccessData(result));
    }

    /// <summary>
    /// GetAllAsync
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var result = await _permissionHandler.GetAllAsync();
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
        var result = await _permissionHandler.GetAsync(id);
        return Ok(SuccessData(result));
    }
}
