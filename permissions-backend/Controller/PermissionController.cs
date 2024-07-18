using Microsoft.AspNetCore.Mvc;
using permissions_backend.Models;
using permissions_backend.Models.Dto;
using permissions_backend.Models.Interface;

namespace permissions_backend.Controller;

[Route("api/permission")]
[ApiController]
public class PermissionController : ControllerBase
{
    private IPermissionRepository _permissionRepository;
    private IPermissionTypeRepository _permissionTypeRepository;

    public PermissionController(IPermissionRepository permissionRepository, IPermissionTypeRepository permissionTypeRepository)
    {
        _permissionRepository = permissionRepository;
        _permissionTypeRepository = permissionTypeRepository;
    }

    /**
     * Get all permissions
     * GET: /api/permission
     * Return: List of PermissionDto
     */
    [HttpGet]
    [ActionName("GetPermissions")]
    public async Task<ActionResult<IEnumerable<PermissionDto>>> GetPermissions()
    {
        var permissions = await _permissionRepository.GetPermissions();
        var permissionsDto = permissions.Select(p => new PermissionDto
        {
            Id = p.Id,
            NombreEmpleado = p.NombreEmpleado,
            ApellidoEmpleado = p.ApellidoEmpleado,
            TipoPermiso = p.TipoPermiso.Descripcion,
            FechaPermiso = p.FechaPermiso
        }).ToList();
        
        return Ok(permissionsDto);
    }

    /**
     * Get permission by id
     * GET: /api/permission/{id}
     * Return: PermissionDto
     */
    [HttpGet("{id}")]
    [ActionName("GetPermissionByIdAsync")]
    public async Task<ActionResult<PermissionDto>> GetPermissionByIdAsync(int id)
    {
        var storedPermission = await _permissionRepository.GetPermissionById(id);
        if (storedPermission == null)
        {
            return NotFound();
        }

        var permissionDto = new PermissionDto
        {
            Id = storedPermission.Id,
            NombreEmpleado = storedPermission.NombreEmpleado,
            ApellidoEmpleado = storedPermission.ApellidoEmpleado,
            TipoPermiso = storedPermission.TipoPermiso.Descripcion,
            FechaPermiso = storedPermission.FechaPermiso
        };
        
        return Ok(permissionDto);
    }

    /**
     * Create a new permission
     * POST: /api/permission
     * Return: PermissionDto
     */
    [HttpPost]
    [ActionName("CreatePermissionAsync")]
    public async Task<ActionResult<PermissionDto>> CreatePermissionAsync(CreatePermissionDto permission)
    {
        var permissionType = await _permissionTypeRepository.GetPermissionTypeById(permission.TipoPermiso);
        if (permissionType == null)
        {
            return BadRequest("Invalid Permission Type Id.");
        }
        var newPermission = new Permission
        {
            NombreEmpleado = permission.NombreEmpleado,
            ApellidoEmpleado = permission.ApellidoEmpleado,
            TipoPermiso = permissionType,
            FechaPermiso = permission.FechaPermiso
        };
        var createdPermission = await _permissionRepository.CreatePermissionAsync(newPermission);
        
        var permissionDto = new PermissionDto
        {
            Id = createdPermission.Id,
            NombreEmpleado = createdPermission.NombreEmpleado,
            ApellidoEmpleado = createdPermission.ApellidoEmpleado,
            TipoPermiso = createdPermission.TipoPermiso.Descripcion,
            FechaPermiso = createdPermission.FechaPermiso
        };
        
        return CreatedAtAction("CreatePermissionAsync", new { id = createdPermission.Id }, permissionDto);
    }

    /**
     * Update a permission
     * PUT: /api/permission/{id}
     * Return: PermissionDto
     */
    [HttpPut("{id}")]
    [ActionName("UpdatePermissionAsync")]
    public async Task<ActionResult<Permission>> UpdatePermissionAsync(int id, UpdatePermissionDto permission)
    {
        if (id != permission.Id)
        {
            return BadRequest();
        }
        
        var storedPermission = await _permissionRepository.GetPermissionById(id);
        if (storedPermission == null)
        {
            return NotFound();
        }
        
        var permissionType = await _permissionTypeRepository.GetPermissionTypeById(permission.TipoPermiso);

        if (permissionType == null)
        {
            return BadRequest("Invalid Permission Type Id.");
        }
        
        storedPermission.NombreEmpleado = permission.NombreEmpleado;
        storedPermission.ApellidoEmpleado = permission.ApellidoEmpleado;
        storedPermission.TipoPermiso = permissionType;
        storedPermission.FechaPermiso = permission.FechaPermiso;
        
        var updatedPermission = await _permissionRepository.UpdatePermissionAsync(storedPermission);
        
        var permissionDto = new PermissionDto
        {
            Id = updatedPermission.Id,
            NombreEmpleado = updatedPermission.NombreEmpleado,
            ApellidoEmpleado = updatedPermission.ApellidoEmpleado,
            TipoPermiso = updatedPermission.TipoPermiso.Descripcion,
            FechaPermiso = updatedPermission.FechaPermiso
        };
        
        return Ok(permissionDto);
    }

    /**
     * Delete a permission
     * DELETE: /api/permission/{id}
     * Return: bool
     */
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