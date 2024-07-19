using permissions_backend.Models;
using permissions_backend.Models.Dto;
using permissions_backend.Models.Interface;
using permissions_backend.Services.Interface;

namespace permissions_backend.Services;

public class PermissionService: IPermissionService
{
    private IPermissionRepository _permissionRepository;
    private readonly IPermissionTypeService _permissionTypeService;
 
    /**
     * Constructor for the PermissionService
     */
    public PermissionService(IPermissionRepository permissionRepository, IPermissionTypeService permissionTypeService)
    {
        _permissionRepository = permissionRepository;
        _permissionTypeService = permissionTypeService;
    }
    
    /**
     * Get all permissions
     */
    public async Task<IEnumerable<PermissionDto>> GetAllPermissionsAsync()
    {
        var permissions = await _permissionRepository.GetPermissions();
        return permissions.Select(p => new PermissionDto
        {
            Id = p.Id,
            NombreEmpleado = p.NombreEmpleado,
            ApellidoEmpleado = p.ApellidoEmpleado,
            TipoPermiso = p.TipoPermiso.Descripcion,
            FechaPermiso = p.FechaPermiso
        });
    }

    /**
     * Get permission by id
     * @param id
     */
    public async Task<PermissionDto> GetPermissionByIdAsync(int id)
    {
        var storedPermission = await _permissionRepository.GetPermissionById(id);
        if (storedPermission == null)
        {
            return null;
        }

        return new PermissionDto
        {
            Id = storedPermission.Id,
            NombreEmpleado = storedPermission.NombreEmpleado,
            ApellidoEmpleado = storedPermission.ApellidoEmpleado,
            TipoPermiso = storedPermission.TipoPermiso.Descripcion,
            FechaPermiso = storedPermission.FechaPermiso
        };
    }

    /**
     * Create a new permission
     * @param permissionDto
     */
    public async Task<PermissionDto> CreatePermissionAsync(CreatePermissionDto permissionDto)
    {
        var permissionType = await _permissionTypeService.GetPermissionTypeByIdAsync(permissionDto.TipoPermiso);
        if (permissionType == null)
        {
            throw new ArgumentException("Permission type not found");
        }
        
        var permission = new Permission
        {
            NombreEmpleado = permissionDto.NombreEmpleado.ToUpper(),
            ApellidoEmpleado = permissionDto.ApellidoEmpleado.ToUpper(),
            TipoPermiso = permissionType,
            FechaPermiso = permissionDto.FechaPermiso
        };
        var createdPermission = await _permissionRepository.CreatePermissionAsync(permission);
        
        return new PermissionDto
        {
            Id = createdPermission.Id,
            NombreEmpleado = createdPermission.NombreEmpleado,
            ApellidoEmpleado = createdPermission.ApellidoEmpleado,
            TipoPermiso = createdPermission.TipoPermiso.Descripcion,
            FechaPermiso = createdPermission.FechaPermiso
        };
    }

    /**
     * Update a permission
     * @param id
     * @param permissionDto
     */
    public async Task<PermissionDto> UpdatePermissionAsync(int id, UpdatePermissionDto permissionDto)
    {
        if (id != permissionDto.Id)
        {
            throw new ArgumentException("Id in body doesn't match the id in the URL");
        }
        
        var storedPermission = await _permissionRepository.GetPermissionById(id);
        if (storedPermission == null)
        {
            return null;
        }
        
        var permissionType = await _permissionTypeService.GetPermissionTypeByIdAsync(permissionDto.TipoPermiso);
        if (permissionType == null)
        {
            throw new ArgumentException("Permission type not found");
        }
        
        storedPermission.NombreEmpleado = permissionDto.NombreEmpleado.ToUpper();
        storedPermission.ApellidoEmpleado = permissionDto.ApellidoEmpleado.ToUpper();
        if (storedPermission.TipoPermiso.Id != permissionType.Id)
        {
            storedPermission.TipoPermiso = permissionType;
        }
        storedPermission.FechaPermiso = permissionDto.FechaPermiso;
        
        var updatedPermission = await _permissionRepository.UpdatePermissionAsync(storedPermission);
        return new PermissionDto
        {
            Id = updatedPermission.Id,
            NombreEmpleado = updatedPermission.NombreEmpleado,
            ApellidoEmpleado = updatedPermission.ApellidoEmpleado,
            TipoPermiso = updatedPermission.TipoPermiso.Descripcion,
            FechaPermiso = updatedPermission.FechaPermiso
        };
    }

    /**
     * Delete a permission
     * @param id
     */
    public async Task<bool> DeletePermissionAsync(int id)
    {
        var storedPermission = await _permissionRepository.GetPermissionById(id);
        if (storedPermission == null)
        {
            return false;
        }
        
        return await _permissionRepository.DeletePermissionAsync(storedPermission);
    }
}