using Microsoft.AspNetCore.Mvc;
using permissions_backend.Models;
using permissions_backend.Models.Dto;
using permissions_backend.Services.Interface;

namespace permissions_backend.Controller;

[Route("api/permission")]
[ApiController]
public class PermissionController : ControllerBase
{
    private readonly IPermissionService _permissionService;
    private readonly IElasticsearchService _elasticsearchService;
    private readonly IKafkaProducerService _kafkaProducerService;

    public PermissionController(
        IPermissionService permissionService, 
        IElasticsearchService elasticsearchService,
        IKafkaProducerService kafkaProducerService)
    {
        _permissionService = permissionService;
        _elasticsearchService = elasticsearchService;
        _kafkaProducerService = kafkaProducerService;
    }
    
    [HttpGet("search")]
    public async Task<IActionResult> SearchPermissions([FromQuery] string query)
    {
        if (string.IsNullOrEmpty(query))
        {
            return BadRequest("Query parameter is required");
        }

        var results = await _elasticsearchService.SearchPermissionsAsync(query);
        return Ok(results);
    }

    /**
     * Get all permissions
     * GET: /api/permission
     * Return: List of PermissionDto
     */
    [HttpGet]
    [ActionName("GetPermissions")]
    public async Task<ActionResult<IEnumerable<PermissionDto>>> GetPermissions()
    {
        var permissions = await _permissionService.GetAllPermissionsAsync();
        return Ok(permissions);
    }

    /**
     * Get permission by id
     * GET: /api/permission/{id}
     * Return: PermissionDto
     */
    [HttpGet("{id}")]
    [ActionName("GetPermissionByIdAsync")]
    public async Task<ActionResult<PermissionDto>> GetPermissionByIdAsync(int id)
    {
        var storedPermission = await _permissionService.GetPermissionByIdAsync(id);
        if (storedPermission == null)
        {
            return NotFound();
        };
        
        // kafka producer
        await _kafkaProducerService.SendMessageAsync("get");
        
        return Ok(storedPermission);
    }

    /**
     * Create a new permission
     * POST: /api/permission
     * Return: PermissionDto
     */
    [HttpPost]
    [ActionName("CreatePermissionAsync")]
    public async Task<ActionResult<PermissionDto>> CreatePermissionAsync(CreatePermissionDto permission)
    {
        try
        {
            var createdPermission = await _permissionService.CreatePermissionAsync(permission);
            
            // kafka producer
            await _kafkaProducerService.SendMessageAsync("create");

            return createdPermission;

        } catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
    }

    /**
     * Update a permission
     * PUT: /api/permission/{id}
     * Return: PermissionDto
     */
    [HttpPut("{id}")]
    [ActionName("UpdatePermissionAsync")]
    public async Task<ActionResult<Permission>> UpdatePermissionAsync(int id, UpdatePermissionDto permission)
    {
        try
        {
            var updatedPermission = await _permissionService.UpdatePermissionAsync(id, permission);
            if (updatedPermission == null)
            {
                return NotFound();
            }
            
            return Ok(updatedPermission);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(e.Message);
        }
    }

    /**
     * Delete a permission
     * DELETE: /api/permission/{id}
     * Return: bool
     */
    [HttpDelete("{id}")]
    [ActionName("DeletePermissionAsync")]
    public async Task<ActionResult<bool>> DeletePermissionAsync(int id)
    {
        var deleted = await _permissionService.DeletePermissionAsync(id);
        if (!deleted)
        {
            return NotFound("Permission not found");
        }

        return Ok(deleted);
    }
}