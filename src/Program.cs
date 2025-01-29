using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PersonalExpenseTrackerSystem.Services;
using PersonalExpenseTrackerSystem.Services.Contract;
using Serilog;
using System.Reflection;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Azure.Core;
using FluentValidation;
using PersonalExpenseTrackerSystem.Entities;
using System;
using PersonalExpenseTrackerSystem.Pages.Shared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddScoped<IValidator<ExpenseModel>, ExpenseValidator>();

builder.Services.AddSingleton<IExpenseService, InMemoryExpenseService>();
builder.Services.AddMvc();

builder.Configuration.Sources.Clear();

// using the file and environment settings
IHostEnvironment env = builder.Environment;
builder.Configuration
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true)
    .AddEnvironmentVariables();

// Logging in a file
var logFile = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
logFile.Information("App has started at Serilog.");


// Cannot view it in the console
var serLogConsole = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();
serLogConsole.Information("Hello, world! Serilog Console ");

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logFile);

TransientFaultHandlingOptions options = new();
builder.Configuration.GetSection(nameof(TransientFaultHandlingOptions))
    .Bind(options);

// using Bind hierarchical configuration/ Options Pattern
Console.WriteLine($"TransientFaultHandlingOptions.Enabled={options.Enabled}");
Console.WriteLine($"TransientFaultHandlingOptions.AutoRetryDelay={options.AutoRetryDelay}");

// Access configuration directly
using IHost host = Host.CreateApplicationBuilder(args).Build();
IConfiguration config = host.Services.GetRequiredService<IConfiguration>();
// Get values from the config given their key and their target type.
int keyOneValue = config.GetValue<int>("Settings:KeyOne");
bool keyTwoValue = config.GetValue<bool>("Settings:KeyTwo");
string? keyThreeNestedValue = config.GetValue<string>("Settings:KeyThree:Message");
string? SecretKey = config.GetValue<string>("SecretKey");
string? TransientFaultHandlingOptionsEnabled = config.GetValue<string>("TransientFaultHandlingOptions:Enabled");
string? TransientFaultHandlingOptionsAutoRetryDelay = config.GetValue<string>("TransientFaultHandlingOptions:AutoRetryDelay");

// Write the values to the console.
Console.WriteLine($"KeyOne = {keyOneValue}");
Console.WriteLine($"KeyTwo = {keyTwoValue}");
Console.WriteLine($"KeyThree:Message = {keyThreeNestedValue}");
Console.WriteLine($"TransientFaultHandlingOptions:Enabled = {TransientFaultHandlingOptionsEnabled}");
Console.WriteLine($"TransientFaultHandlingOptions:AutoRetryDelay = {TransientFaultHandlingOptionsAutoRetryDelay}");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();


//Authentication
var KVURL = config.GetValue<string>("KeyVault:VaultUri");
// builder.Configuration["KeyVault:VaultUri"];
var credential = new DefaultAzureCredential();
try
{
    var secretClient = new SecretClient(new Uri(KVURL), credential);

    //var KVSecret = await secretClient.GetSecretAsync("client-secret");

    //Console.WriteLine(KVSecret.Value.Value);
    //app.MapGet("/", () => KVSecret.Value.Value);
}
catch (Exception e1)
{
    throw new Exception("Authenticaiton Problem.");
}



//app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();


//List the commandline args
string argsString = string.Join(",", args);


//Read Environment variable values
String getTestEnvUser = Environment
                .GetEnvironmentVariable("TestEnv", EnvironmentVariableTarget.User);
String getTestEnvSystem = Environment
                .GetEnvironmentVariable("TestEnvSystem", EnvironmentVariableTarget.Machine);

//Read env variables set in VS
String getASPNETCORE_ENVIRONMENT = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");


// Logging in console.
app.Logger.LogInformation("App has started via Ilogger.");



app.Run();

public sealed class TransientFaultHandlingOptions
{
    public bool Enabled { get; set; }
    public TimeSpan AutoRetryDelay { get; set; }
}