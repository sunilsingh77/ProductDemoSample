using CommonLibrary;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ProductDemo.CustomMiddlewares;
using ProductDemo.Middlewares;
using ProductDemo.Models;
using System.IO.Compression;

var builder = WebApplication.CreateBuilder(args);

string connString = builder.Configuration.GetConnectionString("ProductDB");

builder.Logging.ClearProviders();
builder.Logging.AddLog4Net();

builder.Services.AddDbContext<ProductDbContext>(options =>
{
    options.UseSqlServer(connString);

});

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddMemoryCache();
builder.Services.Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Optimal);
builder.Services.AddResponseCompression(options => options.Providers.Add<GzipCompressionProvider>());
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<IConfigManager, ConfigManager>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<LoggerFactory>();
//builder.Services.AddSingleton<ExceptionMiddleware>();

var app = builder.Build();

//app.Services.GetService<ILoggerFactory>(builder =>
//{
//    builder.AddFile("logs/app-{Date}.json", isJson: true);
//});

//var loggerFactory = app.Services.GetService<ILoggerFactory>();
//loggerFactory.AddFile(builder.Configuration["Logging:LogFilePath"].ToString());

/*using (var scope = app.Logger.Services.CreateScope())
{
    var loggerFactory = scope.ServiceProvider.GetRequiredService<ILoggerFactory>();

    loggerFactory.AddFile("Logs/mylog-{Date}.txt");
}*/

// create a logger factory
/*var loggerFactory = LoggerFactory.Create(
    builder => builder
                // add console as logging target
                .AddConsole()
                // add debug output as logging target
                .AddDebug()
                // set minimum level to log
                .SetMinimumLevel(LogLevel.Debug)
                
);

// create a logger
var logger = loggerFactory.CreateLogger<Program>();
//logger.AddFile(builder.Configuration["Logging:LogFilePath"].ToString());

// logging
logger.LogTrace("Trace message");
logger.LogDebug("Debug message");
logger.LogInformation("Info message");
logger.LogWarning("Warning message");
logger.LogError("Error message");
logger.LogCritical("Critical message");*/

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
