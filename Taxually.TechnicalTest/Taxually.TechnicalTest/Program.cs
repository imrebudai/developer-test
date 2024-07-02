using Taxually.TechnicalTest;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureAppConfiguration((configurationBuilder) =>
{
    IHostEnvironment environment = builder.Environment;

    configurationBuilder.AddJsonFile($"appsettings.{environment.EnvironmentName}.json");
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTaxuallyServices();

builder.Services.AddTaxuallySettings(builder.Configuration);

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
