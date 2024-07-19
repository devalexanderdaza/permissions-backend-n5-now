using permissions_backend.Models.Dto;

namespace permissions_backend.Services.Interface;

public interface IPermissionTypeService
{
    Task<IEnumerable<PermissionTypeDto>> GetAllPermissionTypesAsync();
    Task<PermissionTypeDto> GetPermissionTypeByIdAsync(int id);
    Task<PermissionTypeDto> CreatePermissionTypeAsync(CreatePermissionTypeDto permissionTypeDto);
    Task<PermissionTypeDto> UpdatePermissionTypeAsync(int id, UpdatePermissionTypeDto permissionTypeDto);
    Task<bool> DeletePermissionTypeAsync(int id);
}