using Microsoft.OpenApi.Models;
using Tatargram.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.RegisterServices(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandler>();
app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseStaticFiles(new StaticFileOptions()
{
    OnPrepareResponse = (ctx) =>
    {
        if (ctx.Context.Request.Path.StartsWithSegments("/images"))
        {
            ctx.Context.Response.Headers.Add("Cache-Control", "no-store");
            if (!ctx.Context.User.Identity!.IsAuthenticated)
            {
                ctx.Context.Response.StatusCode = 401;
                ctx.Context.Response.ContentLength = 0;
                ctx.Context.Response.Body = Stream.Null;
            }
        }
    }
});

app.UseAuthorization();

app.UseEndpoints(endpoint => endpoint.MapControllers());

app.Run();
