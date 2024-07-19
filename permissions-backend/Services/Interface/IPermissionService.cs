using permissions_backend.Models.Dto;

namespace permissions_backend.Services.Interface;

/**
 * Interface for the PermissionService
 */
public interface IPermissionService
{
    Task<IEnumerable<PermissionDto>> GetAllPermissionsAsync();
    Task<PermissionDto> GetPermissionByIdAsync(int id);
    Task<PermissionDto> CreatePermissionAsync(CreatePermissionDto permissionDto);
    Task<PermissionDto> UpdatePermissionAsync(int id, UpdatePermissionDto permissionDto);
    Task<bool> DeletePermissionAsync(int id);
}