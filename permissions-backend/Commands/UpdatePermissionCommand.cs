/**
 * Class UpdatePermissionCommand that represents the command to update a permission.
 */
public class UpdatePermissionCommand
{
    public int Id { get; set; }
    public string NombreEmpleado { get; set; }
    public string ApellidoEmpleado { get; set; }
    public int TipoPermiso { get; set; }
    public DateTime FechaPermiso { get; set; }
}