using BudgetApp;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

const string pathBase = "/api/v1";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<BudgetAppDbContext>(options => {
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    options.AddServer(new OpenApiServer { Url = pathBase });
});

var app = builder.Build();

app.UsePathBase(pathBase);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<BudgetAppDbContext>();
    dbContext.Database.Migrate();
}

app.Run();
