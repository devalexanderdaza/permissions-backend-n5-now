using Microsoft.AspNetCore.Mvc;
using permissions_backend.Models;
using permissions_backend.Models.Interface;

namespace permissions_backend.Controller;

[Route("api/permission-type")]
[ApiController]
public class PermissionTypeController : ControllerBase
{
    private IPermissionTypeRepository _permissionTypeRepository;

    public PermissionTypeController(IPermissionTypeRepository permissionTypeRepository)
    {
        _permissionTypeRepository = permissionTypeRepository;
    }

    [HttpGet]
    [ActionName("GetPermissionTypes")]
    public IEnumerable<PermissionType> GetPermissionTypes()
    {
        return _permissionTypeRepository.GetPermissionTypes();
    }

    [HttpGet("{id}")]
    [ActionName("GetPermissionTypeByIdAsync")]
    public async Task<ActionResult<PermissionType>> GetPermissionTypeByIdAsync(int id)
    {
        var storedPermissionType = await _permissionTypeRepository.GetPermissionTypeById(id);
        if (storedPermissionType == null)
        {
            return NotFound();
        }

        return storedPermissionType;
    }

    [HttpPost]
    [ActionName("CreatePermissionTypeAsync")]
    public async Task<PermissionType> CreatePermissionTypeAsync(PermissionType permissionType)
    {
        return await _permissionTypeRepository.CreatePermissionTypeAsync(permissionType);
    }

    [HttpPut("{id}")]
    [ActionName("UpdatePermissionTypeAsync")]
    public async Task<ActionResult<PermissionType>> UpdatePermissionTypeAsync(int id, PermissionType permissionType)
    {
        if (id != permissionType.Id)
        {
            return BadRequest();
        }

        return await _permissionTypeRepository.UpdatePermissionTypeAsync(permissionType);
    }

    [HttpDelete("{id}")]
    [ActionName("DeletePermissionTypeAsync")]
    public async Task<ActionResult<bool>> DeletePermissionTypeAsync(int id)
    {
        var storedPermissionType = await _permissionTypeRepository.GetPermissionTypeById(id);
        if (storedPermissionType == null)
        {
            return NotFound();
        }

        return await _permissionTypeRepository.DeletePermissionTypeAsync(storedPermissionType);
    }
}