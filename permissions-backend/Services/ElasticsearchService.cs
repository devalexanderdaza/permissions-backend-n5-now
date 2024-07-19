using Nest;
using permissions_backend.Models.Dto;
using permissions_backend.Services.Interface;

namespace permissions_backend.Services;

public class ElasticsearchService: IElasticsearchService
{
    private readonly IElasticClient _elasticClient;
    
    public ElasticsearchService(IElasticClient client)
    {
        _elasticClient = client;
    }
    
    public async Task IndexPermissionAsync(PermissionDto permission)
    {
        var response = await _elasticClient.IndexDocumentAsync(permission);
        
        if (!response.IsValid)
        {
            throw new Exception(response.DebugInformation);
        }
    }
    
    public async Task<IEnumerable<PermissionDto>> SearchPermissionsAsync(string query)
    {
        var searchResponse = await _elasticClient.SearchAsync<PermissionDto>(s => s
            .Query(q => q
                .MultiMatch(m => m
                    .Fields(f => f
                        .Field(p => p.NombreEmpleado)
                        .Field(p => p.ApellidoEmpleado)
                        .Field(p => p.FechaPermiso.ToString()))
                    .Query(query)
                )
            )
        );

        if (!searchResponse.IsValid)
        {
            // Manejar error
            throw new Exception("Error searching documents in Elasticsearch");
        }

        return searchResponse.Documents;
    }
}