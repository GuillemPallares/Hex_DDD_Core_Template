using Hex_DDD_Core_Template.Core.Application.Builders;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHex_DDD_Core_Template();

var app = builder.Build();

app.UseHex_DDD_Core_Template();

app.Run();
