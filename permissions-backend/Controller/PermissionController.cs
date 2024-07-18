using Microsoft.AspNetCore.Mvc;
using permissions_backend.Models;
using permissions_backend.Models.Interface;

namespace permissions_backend.Controller;

[Route("api/permission")]
[ApiController]
public class PermissionController : ControllerBase
{
    private IPermissionRepository _permissionRepository;

    public PermissionController(IPermissionRepository permissionRepository)
    {
        _permissionRepository = permissionRepository;
    }

    [HttpGet]
    [ActionName("GetPermissions")]
    public IEnumerable<Permission> GetPermissions()
    {
        return _permissionRepository.GetPermissions();
    }

    [HttpGet("{id}")]
    [ActionName("GetPermissionByIdAsync")]
    public async Task<ActionResult<Permission>> GetPermissionByIdAsync(int id)
    {
        var storedPermission = await _permissionRepository.GetPermissionById(id);
        if (storedPermission != null)
        {
            return storedPermission;
        }

        return NotFound();
    }

    [HttpPost]
    [ActionName("CreatePermissionAsync")]
    public async Task<ActionResult<Permission>> CreatePermissionAsync(Permission permission)
    {
        return await _permissionRepository.CreatePermissionAsync(permission);
    }

    [HttpPut("{id}")]
    [ActionName("UpdatePermissionAsync")]
    public async Task<ActionResult<Permission>> UpdatePermissionAsync(int id, Permission permission)
    {
        if (id != permission.Id)
        {
            return BadRequest();
        }

        return await _permissionRepository.UpdatePermissionAsync(permission);
    }

    [HttpDelete("{id}")]
    [ActionName("DeletePermissionAsync")]
    public async Task<ActionResult<bool>> DeletePermissionAsync(int id)
    {
        var storedPermission = await _permissionRepository.GetPermissionById(id);
        if (storedPermission == null)
        {
            return NotFound();
        }

        return await _permissionRepository.DeletePermissionAsync(storedPermission);
    }
}