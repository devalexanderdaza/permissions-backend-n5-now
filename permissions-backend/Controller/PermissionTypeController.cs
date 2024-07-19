using Microsoft.AspNetCore.Mvc;
using permissions_backend.Models.Dto;
using permissions_backend.Services.Interface;

namespace permissions_backend.Controller;

[Route("api/permission-type")]
[ApiController]
public class PermissionTypeController : ControllerBase
{
    private readonly IPermissionTypeService _permissionTypeService;

    public PermissionTypeController(IPermissionTypeService permissionTypeService)
    {
        _permissionTypeService = permissionTypeService;
    }

    /**
     * Get all permission types
     * GET /api/permission-type
     * @return IEnumerable<PermissionTypeDto>
     */
    [HttpGet]
    [ActionName("GetPermissionTypes")]
    public async Task<ActionResult<IEnumerable<PermissionTypeDto>>> GetPermissionTypes()
    {
        var permissionTypes = await _permissionTypeService.GetAllPermissionTypesAsync();
        return Ok(permissionTypes);
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
        var permissionType = await _permissionTypeService.GetPermissionTypeByIdAsync(id);
        if (permissionType == null)
        {
            return NotFound();
        }

        return Ok(permissionType);
    }

    /**
     * Create a new permission type
     * POST /api/permission-type
     * @param CreatePermissionTypeDto permissionType
     * @return PermissionTypeDto
     */
    [HttpPost]
    [ActionName("CreatePermissionTypeAsync")]
    public async Task<PermissionTypeDto> CreatePermissionTypeAsync(CreatePermissionTypeDto permissionType)
    {
        return await _permissionTypeService.CreatePermissionTypeAsync(permissionType);
    }

    /**
     * Update a permission type
     * PUT /api/permission-type/{id}
     * @param int id
     * @param UpdatePermissionTypeDto permissionType
     * @return PermissionTypeDto
     */
    [HttpPut("{id}")]
    [ActionName("UpdatePermissionTypeAsync")]
    public async Task<ActionResult<PermissionTypeDto>> UpdatePermissionTypeAsync(int id, UpdatePermissionTypeDto permissionType)
    {
        var updatedPermissionType = await _permissionTypeService.UpdatePermissionTypeAsync(id, permissionType);
        if (updatedPermissionType == null)
        {
            return NotFound("Permission type not found");
        }

        return Ok(updatedPermissionType);
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
        var deleted = await _permissionTypeService.DeletePermissionTypeAsync(id);
        if (!deleted)
        {
            return NotFound("Permission type not found");
        }

        return Ok(deleted);
    }
}