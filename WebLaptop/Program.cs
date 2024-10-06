using BusinessLogic;
using DataAccess;
using WebLaptop;

var builder = WebApplication.CreateBuilder(args);

var connStr = builder.Configuration.GetConnectionString("TelegramBotDb")!;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCustomDbContext(connStr);
builder.Services.AddRepositories();

builder.Services.AddCustomServices();
builder.Services.AddScoped<IBotUserService, BotUserService>();




builder.Services.AddScoped<TelegramBotService>(provider => 
{
    var token = "7724052344:AAGHW_28SNYcFLCXfbwEiUaI5CQ2UagW9t0";
    var scopeFactory = provider.GetRequiredService<IServiceScopeFactory>();
    return new TelegramBotService(token, scopeFactory);
});

builder.Services.AddHostedService<TelegramBotHostedService>();





var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
