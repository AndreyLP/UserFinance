using Infrastructure.Clients;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Worker;

var builder = Host.CreateApplicationBuilder(args);
Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
var configuration = builder.Configuration;

builder.Services.AddDbContext<CurrencyUpdaterDbContext>(options =>
    options.UseNpgsql(
        configuration.GetConnectionString("FinanceDb")
    ));

builder.Services.AddHttpClient();

builder.Services.AddHostedService<CurrencyUpdateWorker>();
builder.Services.AddHttpClient<ICbrClient, CbrClient>(client =>
{
    client.BaseAddress = new Uri(
        builder.Configuration["CbrApi:Url"]!
    );
});

var app = builder.Build();
app.Run();