using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace permissions_backend.Models;

/**
 * Class that represents a permission in the database
 */
public class Permission
{
    [Key]
    public int Id { get; set; }
        
    [Required]
    [MaxLength(100)]
    public string NombreEmpleado { get; set; }
        
    [Required]
    [MaxLength(100)]
    public string ApellidoEmpleado { get; set; }
        
    [Required]
    [ForeignKey("PermissionType")]
    public PermissionType TipoPermiso { get; set; }
        
    [Required]
    public DateTime FechaPermiso { get; set; }
}