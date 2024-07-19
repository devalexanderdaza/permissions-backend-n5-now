using Microsoft.EntityFrameworkCore;
using Nest;
using permissions_backend.Models.Interface;
using permissions_backend.Models.Repository;
using permissions_backend.Services;
using permissions_backend.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

var settings = new ConnectionSettings(new Uri("http://localhost:9200"))
    .DefaultIndex("permissions");
var client = new ElasticClient(settings);

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

builder.Services.AddSingleton<IElasticClient>(client);

builder.Services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddTransient<IElasticsearchService, ElasticsearchService>();
builder.Services.AddTransient<IPermissionTypeRepository, PermissionTypeRepository>();
builder.Services.AddTransient<IPermissionRepository, PermissionRepository>();
builder.Services.AddTransient<IPermissionTypeService, PermissionTypeService>();
builder.Services.AddTransient<IPermissionService, PermissionService>();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middleware para enrutar las peticiones a los controladores
app.UseRouting();

app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

app.Run();