using permissions_backend.Models;
using permissions_backend.Models.Dto;
using permissions_backend.Models.Interface;
using permissions_backend.Services.Interface;

namespace permissions_backend.Services;

public class PermissionTypeService: IPermissionTypeService
{
    private IPermissionTypeRepository _permissionTypeRepository;
    
    public PermissionTypeService(IPermissionTypeRepository permissionTypeRepository)
    {
        _permissionTypeRepository = permissionTypeRepository;
    }
    
    public Task<IEnumerable<PermissionTypeDto>> GetAllPermissionTypesAsync()
    {
        var permissionTypes = _permissionTypeRepository.GetPermissionTypes();
        return Task.FromResult(permissionTypes.Select(pt => new PermissionTypeDto
        {
            Id = pt.Id,
            Descripcion = pt.Descripcion
        }));
    }

    public async Task<PermissionTypeDto> GetPermissionTypeByIdAsync(int id)
    {
        var storedPermissionType = await _permissionTypeRepository.GetPermissionTypeById(id);
        if (storedPermissionType == null)
        {
            return null;
        }

        return new PermissionTypeDto
        {
            Id = storedPermissionType.Id,
            Descripcion = storedPermissionType.Descripcion
        };
    }

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