using System.Data;
using Microsoft.EntityFrameworkCore;
using permissions_backend.Models.Interface;

namespace permissions_backend.Models.Repository;

public class PermissionRepository : IPermissionRepository
{
    // The database context
    protected readonly ApplicationDbContext _context;

    /**
     * Constructor
     * @param context - The database context
     */
    public PermissionRepository(ApplicationDbContext context) => _context = context;

    /**
     * Fetches all permissions from the database
     * @return IEnumerable<Permission> - A list of all permissions
     * @throws DataException - If an error occurs while fetching permissions
     */
    public async Task<IEnumerable<Permission>> GetPermissions()
    {
        try
        {
            return await _context.Permissions.ToListAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new DataException("An error occurred while fetching permissions", e);
        }
    }

    /**
     * Fetches a permission by its ID from the database
     * @param id - The ID of the permission to fetch
     * @return Permission - The fetched permission
     * @throws DataException - If an error occurs while fetching the permission
     */
    public async Task<Permission> GetPermissionById(int id)
    {
        try
        {
            return await _context.Permissions.Include(p => p.TipoPermiso).FirstOrDefaultAsync(p => p.Id == id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new DataException("An error occurred while fetching permission", e);
        }
    }

    /**
     * Creates a new permission in the database
     * @param permission - The permission to create
     * @return Permission - The created permission
     * @throws DataException - If an error occurs while creating the permission
     */
    public async Task<Permission> CreatePermissionAsync(Permission permission)
    {
        try
        {
            permission.NombreEmpleado = permission.NombreEmpleado.ToUpper();
            permission.ApellidoEmpleado = permission.ApellidoEmpleado.ToUpper();
            await _context.Set<Permission>().AddAsync(permission);
            await _context.SaveChangesAsync();
            return permission;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new DataException("An error occurred while creating permission", e);
        }
    }

    /**
     * Updates a permission in the database
     * @param permission - The permission to update
     * @return Permission - The updated permission
     * @throws DataException - If an error occurs while updating the permission
     */
    public async Task<Permission> UpdatePermissionAsync(Permission permission)
    {
        try
        {
            permission.NombreEmpleado = permission.NombreEmpleado.ToUpper();
            permission.ApellidoEmpleado = permission.ApellidoEmpleado.ToUpper();
            _context.Entry(permission).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return permission;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new DataException("An error occurred while updating permission", e);
        }
    }

    /**
     * Deletes a permission from the database
     * @param permission - The permission to delete
     * @return bool - Whether the permission was deleted
     * @throws DataException - If an error occurs while deleting the permission
     */
    public async Task<bool> DeletePermissionAsync(Permission permission)
    {
        try
        {
            var storedPermission = await GetPermissionById(permission.Id);
            if (storedPermission == null)
            {
                return false;
            }

            _context.Permissions.Remove(permission);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new DataException("An error occurred while deleting permission", e);
        }
    }
}