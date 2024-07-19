using permissions_backend.Models.Dto;

namespace permissions_backend.Services.Interface;

public interface IElasticsearchService
{
    Task IndexPermissionAsync(PermissionDto permission);
    Task<IEnumerable<PermissionDto>> SearchPermissionsAsync(string query);
}