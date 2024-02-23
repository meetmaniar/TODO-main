using Microsoft.OpenApi.Models;
using todo.api.common;
using todo.api.Filters;
using todo.api.Services;
using todo.data;

var builder = WebApplication.CreateBuilder(args);
AppSettings.Instance = builder.Configuration.Get<AppSettings>();

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.OperationFilter<AuthorizationOperationFilter>();

    options.AddSecurityDefinition("apikey", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.ApiKey,
        In = ParameterLocation.Header,
        Name = AppSettings.Instance.ApiKeyHeader
    });

    options.AddSecurityDefinition("todoUser", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.ApiKey,
        In = ParameterLocation.Header,
        Name = AppSettings.Instance.TodoUserHeader
    });

    options.EnableAnnotations();
});

builder.Services.AddTransient((s) =>
{
    var ctx = new TodoContext();
    ctx.Seed();
    return ctx;
});

builder.Services.AddTransient<TodoServices>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
