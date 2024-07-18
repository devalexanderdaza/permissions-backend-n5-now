using Microsoft.AspNetCore.Mvc;
using permissions_backend.Models;
using permissions_backend.Models.Dto;
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

    /**
     * Get all permission types
     * GET /api/permission-type
     * @return IEnumerable<PermissionTypeDto>
     */
    [HttpGet]
    [ActionName("GetPermissionTypes")]
    public IEnumerable<PermissionTypeDto> GetPermissionTypes()
    {
        var permissionTypes = _permissionTypeRepository.GetPermissionTypes();
        return permissionTypes.Select(pt => new PermissionTypeDto
        {
            Id = pt.Id,
            Descripcion = pt.Descripcion
        });
    }

    /**
     * Get permission type by id
     * GET /api/permission-type/{id}
     * @param int id
     * @return PermissionTypeDto
     */
    [HttpGet("{id}")]
    [ActionName("GetPermissionTypeByIdAsync")]
    public async Task<ActionResult<PermissionTypeDto>> GetPermissionTypeByIdAsync(int id)
    {
        var storedPermissionType = await _permissionTypeRepository.GetPermissionTypeById(id);
        if (storedPermissionType == null)
        {
            return NotFound();
        }

        return new PermissionTypeDto
        {
            Id = storedPermissionType.Id,
            Descripcion = storedPermissionType.Descripcion
        };
    }

    /**
     * Create a new permission type
     * POST /api/permission-type
     * @param PermissionType permissionType
     * @return PermissionTypeDto
     */
    [HttpPost]
    [ActionName("CreatePermissionTypeAsync")]
    public async Task<PermissionTypeDto> CreatePermissionTypeAsync(PermissionType permissionType)
    {
        var createdPermissionType = await _permissionTypeRepository.CreatePermissionTypeAsync(permissionType);
        return new PermissionTypeDto
        {
            Id = createdPermissionType.Id,
            Descripcion = createdPermissionType.Descripcion
        };
    }

    /**
     * Update a permission type
     * PUT /api/permission-type/{id}
     * @param int id
     * @param PermissionType permissionType
     * @return PermissionTypeDto
     */
    [HttpPut("{id}")]
    [ActionName("UpdatePermissionTypeAsync")]
    public async Task<ActionResult<PermissionTypeDto>> UpdatePermissionTypeAsync(int id, PermissionType permissionType)
    {
        if (id != permissionType.Id)
        {
            return BadRequest();
        }
        
        var storedPermissionType = await _permissionTypeRepository.GetPermissionTypeById(id);
        if (storedPermissionType == null)
        {
            return NotFound();
        }

        var updatedPermissionType = await _permissionTypeRepository.UpdatePermissionTypeAsync(permissionType);
        return new PermissionTypeDto
        {
            Id = updatedPermissionType.Id,
            Descripcion = updatedPermissionType.Descripcion
        };
    }

    /**
     * Delete a permission type
     * DELETE /api/permission-type/{id}
     * @param int id
     * @return bool
     */
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