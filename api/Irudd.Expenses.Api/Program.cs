using Irudd.Expenses.Api.Controllers;
using Irudd.Expenses.Api.Datamodel;
using Irudd.Expenses.Api.Services;
using Irudd.Expenses.Api.Support;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddControllers(options => options.Filters.Add(new ApiErrorActionFilter()));
services.AddEndpointsApiExplorer();
services.AddSwaggerGen(options =>
{
    //Drop namespace on model names
    options.CustomSchemaIds((Type x) => x.Name);
    //v1/identity/register -> identity
    options.TagActionsBy(x => new List<string> { x.RelativePath?.Split("/")?.Skip(1)?.FirstOrDefault() ?? "" });
    //Allow code comments as documentation
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{typeof(Program).Assembly.GetName().Name}.xml"));
});

services.AddDbContext<ExpensesContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("ExpensesDb")));

services.AddAuthorization();
services
    .AddIdentityApiEndpoints<User>()
    .AddEntityFrameworkStores<ExpensesContext>();
services.AddScoped<UserManager<User>>();
services.AddScoped<ICurrentUser, HttpContextCurrentUser>();

services.AddScoped<DatabaseSetupService>();
services.AddScoped<ExpensesService>();
services.AddScoped<CategoriesService>();
services.AddCors(options =>
{
    var uiBaseUrl = new Uri(builder.Configuration.GetRequiredSettingValue("Irudd.Expenses:UiBaseUrl"));
    options.AddPolicy(name: "CorsPolicy1",
        policy =>
        {
            policy.WithOrigins(uiBaseUrl.ToString(), uiBaseUrl.ToString().TrimEnd('/'));
            policy.AllowAnyHeader();
            policy.AllowAnyMethod();
        });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Middleware order: https://learn.microsoft.com/en-us/aspnet/core/fundamentals/middleware/?view=aspnetcore-8.0#middleware-order
app.UseHttpsRedirection();
app.UseCors("CorsPolicy1");
app.MapGroup($"{BaseController.ApiVersion}/identity").MapIdentityApi<User>();
app.MapControllers();

using (var serviceScope = app.Services.CreateScope())
{
    var service = serviceScope.ServiceProvider.GetRequiredService<DatabaseSetupService>();
    await service.CreateMigrateAndInitializeDatabaseAsync();    
}

app.Run();