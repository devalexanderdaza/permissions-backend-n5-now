using System.ComponentModel.DataAnnotations;

namespace permissions_backend.Models;

/**
 * Represents a permission type.
 */
public class PermissionType
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(200)]
    public string Descripcion { get; set; }
}