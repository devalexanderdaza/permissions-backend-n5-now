using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) {}

    public DbSet<Permission> Permissions { get; set; }
    public DbSet<PermissionType> PermissionTypes { get; set; }
}

public class Permission
{
    public int Id { get; set; }
    public string NombreEmpleado { get; set; }
    public string ApellidoEmpleado { get; set; }
    public int TipoPermiso { get; set; }
    public DateTime FechaPermiso { get; set; }
}

public class PermissionType
{
    public int Id { get; set; }
    public string Descripcion { get; set; }
}