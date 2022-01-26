using Doosy.Domain.Extensions;
using Doosy.Infrastructure.Extensions;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using System.Net;




var configuration = new ConfigurationBuilder()
.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
.Build();


Log.Logger = new LoggerConfiguration()
.Enrich.FromLogContext()
.WriteTo.Debug()
.WriteTo.Console()
.WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(configuration["ElasticConfiguration:Uri"]))
{
    AutoRegisterTemplate = true,
    NumberOfReplicas = 0,
    AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv7,
    ModifyConnectionSettings = x => x.BasicAuthentication("elastic", "Password12@!"),
    IndexFormat = $"doosyserverLog_{DateTime.Now:yyyy.MM.dd}"
})
.ReadFrom.Configuration(configuration)
.CreateLogger();


try
{
    Log.Information("Starting  the w api host");
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddInfrastructure(builder.Configuration);
    builder.Services.AddDomain();
    //builder.Services.AddAuthentication();
    //builder.Services.AddAuthorization();




    var app = builder.Build();
    app.UseSwagger();
    app.UseSwaggerUI();
    // Configure the HTTP request pipeline.
    //if (app.Environment.IsDevelopment())
    //{

    //}




    //app.UseHttpsRedirection();
    app.UseCors(builders => builders.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
} catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
    return 1;
}
finally
{
    Log.CloseAndFlush(); 
    
   

}
return 0;

