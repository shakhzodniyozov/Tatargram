using Microsoft.OpenApi.Models;
using Tatargram.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.RegisterServices(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new() { Title = "Tatargram", Version = "v1" });
    options.AddSecurityDefinition("Bearer", new()
    {
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        BearerFormat = "JWT",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Description = "Enter valid access token.",
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

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
app.UseAuthorization();

app.UseEndpoints(endpoint => endpoint.MapControllers());

app.Run();
