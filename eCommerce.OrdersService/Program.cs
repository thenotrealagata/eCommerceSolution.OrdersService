using DataAccess;
using BusinessLogic;
using eCommerce.OrdersService.Middleware;
using BusinessLogic.HttpClients;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDataAccess(builder.Configuration);
builder.Services.AddBusinessLogic();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(opts =>
{
   opts.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:3000")
         .AllowAnyHeader()
         .AllowAnyMethod();
    });
});

builder.Services.AddHttpClient<UsersMicroserviceClient>(client =>
{
    client.BaseAddress = new Uri($"http://{
        Environment.GetEnvironmentVariable("UsersMicroserviceName")
        }:{Environment.GetEnvironmentVariable("UsersMicroservicePort")
        }");
});

var app = builder.Build();

app.UseExceptionHandlingMiddleware();
app.UseRouting();

app.UseCors();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
