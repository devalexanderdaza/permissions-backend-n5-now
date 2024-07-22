/**
 * Class that represents the command to create a permission
 */
public class CreatePermissionCommand
{
    public string NombreEmpleado { get; set; }
    public string ApellidoEmpleado { get; set; }
    public int TipoPermiso { get; set; }
    public DateTime FechaPermiso { get; set; }
}