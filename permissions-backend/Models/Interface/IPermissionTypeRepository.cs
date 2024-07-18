namespace permissions_backend.Models.Interface
{
    /**
    * The permission type repository interface
    */
    public interface IPermissionTypeRepository
    {
        /**
         * Fetches all permission types from the database
         * @return IEnumerable<PermissionType> - A list of all permission types
         * @throws DataException - If an error occurs while fetching permission types
         */
        IEnumerable<PermissionType> GetPermissionTypes();

        /**
         * Fetches a permission type by its ID from the database
         * @param id - The ID of the permission type to fetch
         * @return PermissionType - The fetched permission type
         * @throws DataException - If an error occurs while fetching the permission type
         */
        Task<PermissionType> GetPermissionTypeById(int id);

        /**
         * Creates a new permission type in the database
         * @param permissionType - The permission type to create
         * @return PermissionType - The created permission type
         * @throws DataException - If an error occurs while creating the permission type
         */
        Task<PermissionType> CreatePermissionTypeAsync(PermissionType permissionType);

        /**
         * Updates a permission type in the database
         * @param permissionType - The permission type to update
         * @return PermissionType - The updated permission type
         * @throws DataException - If an error occurs while updating the permission type
         */
        Task<PermissionType> UpdatePermissionTypeAsync(PermissionType permissionType);

        /**
         * Deletes a permission type from the database
         * @param permissionType - The permission type to delete
         * @return bool - True if the permission type was deleted, false otherwise
         * @throws DataException - If an error occurs while deleting the permission type
         */
        Task<bool> DeletePermissionTypeAsync(PermissionType permissionType);
    }
}