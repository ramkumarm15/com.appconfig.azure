using com.app.testing.Controllers;
using com.appconfig.azure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAzureAppConfiguration();

var settings = builder.Configuration.GetSection("Azure").Get<AzureSettings>();

builder.Configuration.AddAzure(settings);

//builder.Configuration.AddAzureKeyVault()

var sec = builder.Configuration.GetSection("Testing:Settings:Keyvault");
builder.Services.Configure<Settings>(builder.Configuration.GetSection("Testing:Settings"));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAzureApp();

app.UseAuthorization();

app.MapControllers();

app.Run();
