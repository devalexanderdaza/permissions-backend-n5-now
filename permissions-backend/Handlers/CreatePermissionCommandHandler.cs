using MediatR;
using permissions_backend.Data;
using permissions_backend.Models;

public class CreatePermissionCommandHandler : IRequestHandler<CreatePermissionCommand, Permission>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreatePermissionCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Permission> Handle(CreatePermissionCommand request, CancellationToken cancellationToken)
    {
        var permissionTypeStored = await _unitOfWork.PermissionTypes.GetPermissionTypeById(request.TipoPermiso);
        
        var permission = new Permission
        {
            NombreEmpleado = request.NombreEmpleado,
            ApellidoEmpleado = request.ApellidoEmpleado,
            TipoPermiso = permissionTypeStored,
            FechaPermiso = request.FechaPermiso
        };

        await _unitOfWork.Permissions.CreatePermissionAsync(permission);
        _unitOfWork.Complete();

        return permission;
    }
}