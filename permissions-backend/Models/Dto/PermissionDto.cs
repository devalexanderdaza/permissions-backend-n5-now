namespace permissions_backend.Models.Dto;

/**
 * PermissionDto class
 */
public class PermissionDto
{
    public int Id { get; set; }
    public string NombreEmpleado { get; set; }
    public string ApellidoEmpleado { get; set; }
    public string TipoPermiso { get; set; }
    public DateTime FechaPermiso { get; set; }
}

/**
 * CreatePermissionDto class
 */
public class CreatePermissionDto
{
    public string NombreEmpleado { get; set; }
    public string ApellidoEmpleado { get; set; }
    public int TipoPermiso { get; set; }
    public DateTime FechaPermiso { get; set; }
}

/**
 * UpdatePermissionDto class
 */
public class UpdatePermissionDto
{
    public int Id { get; set; }
    public string NombreEmpleado { get; set; }
    public string ApellidoEmpleado { get; set; }
    public int TipoPermiso { get; set; }
    public DateTime FechaPermiso { get; set; }
}