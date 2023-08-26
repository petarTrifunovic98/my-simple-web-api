using Microsoft.AspNetCore.HttpOverrides;
using my_simple_web_api.DatabaseClient;
using my_simple_web_api.Extensions;
using NLog;

var builder = WebApplication.CreateBuilder(args);

LogManager.Setup().LoadConfigurationFromFile(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

// Add services to the container.

builder.Services.ConfigureCors();
builder.Services.ConfigureIISIntegration();
builder.Services.ConfigureLoggerService();
builder.Services.ConfigureMySimpleDatabaseConnection(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

// var dbClient = new MySimpleDatabaseClient<Row>("localhost", 9988);
// builder.Services.AddSingleton(dbClient);


var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }
if (app.Environment.IsDevelopment()) 
    app.UseDeveloperExceptionPage(); 
else 
    app.UseHsts();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseForwardedHeaders(new ForwardedHeadersOptions 
{ 
    ForwardedHeaders = ForwardedHeaders.All 
}); 

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
