using permissions_backend.Models;
using permissions_backend.Models.Dto;
using permissions_backend.Models.Interface;
using permissions_backend.Services.Interface;

namespace permissions_backend.Services;

public class PermissionTypeService: IPermissionTypeService
{
    private IPermissionTypeRepository _permissionTypeRepository;
    
    /**
     * Constructor for the PermissionTypeService
     */
    public PermissionTypeService(IPermissionTypeRepository permissionTypeRepository)
    {
        _permissionTypeRepository = permissionTypeRepository;
    }
    
    /**
     * Get all permission types
     */
    public Task<IEnumerable<PermissionTypeDto>> GetAllPermissionTypesAsync()
    {
        var permissionTypes = _permissionTypeRepository.GetPermissionTypes();
        return Task.FromResult(permissionTypes.Select(pt => new PermissionTypeDto
        {
            Id = pt.Id,
            Descripcion = pt.Descripcion
        }));
    }

    /**
     * Get permission type by id
     * @param id
     */
    public async Task<PermissionType> GetPermissionTypeByIdAsync(int id)
    {
        var storedPermissionType = await _permissionTypeRepository.GetPermissionTypeById(id);
        if (storedPermissionType == null)
        {
            return null;
        }
        return storedPermissionType;
    }

    /**
     * Create a new permission type
     * @param permissionTypeDto
     */
    public async Task<PermissionTypeDto> CreatePermissionTypeAsync(CreatePermissionTypeDto permissionTypeDto)
    {
        var permissionType = new PermissionType
        {
            Descripcion = permissionTypeDto.Descripcion.ToUpper()
        };
        var createdPermissionType = await _permissionTypeRepository.CreatePermissionTypeAsync(permissionType);
        return new PermissionTypeDto
        {
            Id = createdPermissionType.Id,
            Descripcion = createdPermissionType.Descripcion
        };
    }

    /**
     * Update a permission type
     * @param id
     * @param permissionTypeDto
     */
    public async Task<PermissionTypeDto> UpdatePermissionTypeAsync(int id, UpdatePermissionTypeDto permissionTypeDto)
    {
        if (id != permissionTypeDto.Id)
        {
            throw new ArgumentException("Id in body doesn't match the id in the URL");
        }
        
        var storedPermissionType = await _permissionTypeRepository.GetPermissionTypeById(id);
        if (storedPermissionType == null)
        {
            return null;
        }
        
        storedPermissionType.Descripcion = permissionTypeDto.Descripcion.ToUpper();
        
        var updatedPermissionType = await _permissionTypeRepository.UpdatePermissionTypeAsync(storedPermissionType);
        return new PermissionTypeDto
        {
            Id = updatedPermissionType.Id,
            Descripcion = updatedPermissionType.Descripcion
        };
        
    }

    /**
     * Delete a permission type
     * @param id
     */
    public async Task<bool> DeletePermissionTypeAsync(int id)
    {
        var storedPermissionType = await _permissionTypeRepository.GetPermissionTypeById(id);
        if (storedPermissionType == null)
        {
            return false;
        }

        return await _permissionTypeRepository.DeletePermissionTypeAsync(storedPermissionType);
    }
}