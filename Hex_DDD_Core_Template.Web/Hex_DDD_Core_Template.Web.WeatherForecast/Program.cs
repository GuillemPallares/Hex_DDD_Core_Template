using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Hex_DDD_Core_Template.UI.Client;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHex_DDD_Core_TemplateUI();

var app = builder.Build();

app.UseHex_DDD_Core_TemplateUI();

app.Run();
