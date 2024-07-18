using System.Data;
using Microsoft.EntityFrameworkCore;

namespace permissions_backend.Models.Interface;

public class PermissionTypeRepository: IPermissionTypeRepository
{
    // The database context
    protected readonly ApplicationDbContext _context;
    
    /**
     * Constructor
     * @param context - The database context
     */
    public PermissionTypeRepository(ApplicationDbContext context) => _context = context;
    
    /**
     * Fetches all permission types from the database
     * @return IEnumerable<PermissionType> - A list of all permission types
     * @throws DataException - If an error occurs while fetching permission types
     */
    public IEnumerable<PermissionType> GetPermissionTypes()
    {
        try
        {
            return _context.PermissionTypes.ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new DataException("An error occurred while fetching permission types", e);
        }
    }
    
    /**
     * Fetches a permission type by its ID from the database
     * @param id - The ID of the permission type to fetch
     * @return PermissionType - The fetched permission type
     * @throws DataException - If an error occurs while fetching the permission type
     */
    public async Task<PermissionType> GetPermissionTypeById(int id)
    {
        try
        {
            return await _context.PermissionTypes.FindAsync(id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new DataException("An error occurred while fetching permission type", e);
        }
    }
    
    /**
     * Creates a new permission type in the database
     * @param permissionType - The permission type to create
     * @return PermissionType - The created permission type
     * @throws DataException - If an error occurs while creating the permission type
     */
    public async Task<PermissionType> CreatePermissionTypeAsync(PermissionType permissionType)
    {
        try
        {
            await _context.Set<PermissionType>().AddAsync(permissionType);
            await _context.SaveChangesAsync();
            return permissionType;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new DataException("An error occurred while creating permission type", e);
        }
    }
    
    /**
     * Updates an existing permission type in the database
     * @param permissionType - The permission type to update
     * @return PermissionType - The updated permission type
     * @throws DataException - If an error occurs while updating the permission type
     */
    public async Task<PermissionType> UpdatePermissionTypeAsync(PermissionType permissionType)
    {
        try
        {
            _context.Entry(permissionType).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return permissionType;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new DataException("An error occurred while updating permission type", e);
        }
    }

    /**
     * Deletes an existing permission type from the database
     * @param permissionType - The permission type to delete
     * @return bool - True if the permission type was deleted, false otherwise
     * @throws DataException - If an error occurs while deleting the permission type
     */
    public async Task<bool> DeletePermissionTypeAsync(PermissionType permissionType)
    {
        try
        {
            var storedPermissionType = await GetPermissionTypeById(permissionType.Id);
            if (storedPermissionType == null)
            {
                return false;
            }
            _context.Set<PermissionType>().Remove(permissionType);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new DataException("An error occurred while deleting permission type", e);
        }
    }
}