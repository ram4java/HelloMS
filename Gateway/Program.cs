using Gateway.Middleware;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Cache;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi();

builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Services.AddOcelot(builder.Configuration);

builder.Services.AddCors(options => {
    options.AddDefaultPolicy(builder => {
        builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseMiddleware<GatewayMiddleware>();
app.UseAuthorization();
await app.UseOcelot();


app.Run();

