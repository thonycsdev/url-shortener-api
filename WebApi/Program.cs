using CrossCutting.Injections;
using CrossCutting;
var builder = WebApplication.CreateBuilder(args);

//Load EnvVars
var rootPath = Directory.GetCurrentDirectory();
var dotenvLocation = Path.Combine(rootPath, ".env");
DotEnv.LoadEnvironmentVariables(dotenvLocation);
builder.Configuration.AddEnvironmentVariables();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddRepositories();
builder.Services.AddServices();
builder.Services.AddCors(opt =>
    opt.AddPolicy(
        "AllowAll",
        policy =>
        {
            policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
        }
    )
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
