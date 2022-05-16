using Tatargram.Data;
using Tatargram.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.RegisterServices(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();
DbInitializer.Init(app.Services);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("ClientPolicy");
app.UseMiddleware<ExceptionHandler>();
app.UseRouting();

app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoint => endpoint.MapControllers());

app.Run();
