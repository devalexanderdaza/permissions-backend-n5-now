namespace permissions_backend.Models.Dto;

/**
 * PermissionTypeDto class
 */
public class PermissionTypeDto
{
    public int Id { get; set; }
    public string Descripcion { get; set; }
}

/**
 * CreatePermissionTypeDto class
 */
public class CreatePermissionTypeDto
{
    public string Descripcion { get; set; }
}

/**
 * UpdatePermissionTypeDto class
 */
public class UpdatePermissionTypeDto
{
    public int Id { get; set; }
    public string Descripcion { get; set; }
}