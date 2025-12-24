using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TgBotTest.Api;

var builder = Host.CreateApplicationBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

builder.Services
    .AddAppOptions(builder.Configuration)
    .AddDatabase(builder.Configuration)
    .AddApplicationServices()
    .AddExternalApis(builder.Configuration)
    .AddTelegramBot();

builder.Services.AddHostedService<TelegramBotHostedService>();

var app = builder.Build();
app.Run();
