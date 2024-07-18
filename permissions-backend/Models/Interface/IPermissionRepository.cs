namespace permissions_backend.Models.Repository;

/**
 * The permission repository interface
 */
public interface IPermissionRepository
{
    /**
     * Fetches all permissions from the database
     * @return IEnumerable<Permission> - A list of all permissions
     */
    IEnumerable<Permission> GetPermissions();
    
    /**
     * Fetches a permission by its ID from the database
     * @param id - The ID of the permission to fetch
     * @return Permission - The fetched permission
     */
    Task<Permission> GetPermissionById(int id);
    
    /**
     * Creates a new permission in the database
     * @param permission - The permission to create
     * @return Permission - The created permission
     */
    Task<Permission> CreatePermissionAsync(Permission permission);
    
    /**
     * Updates a permission in the database
     * @param permission - The permission to update
     * @return Permission - The updated permission
     */
    Task<Permission> UpdatePermissionAsync(Permission permission);
    
    /**
     * Deletes a permission from the database
     * @param permission - The permission to delete
     * @return bool - Whether the permission was deleted
     */
    Task<bool> DeletePermissionAsync(Permission permission);
}