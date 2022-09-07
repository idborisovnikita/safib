var builder = WebApplication.CreateBuilder(args);

builder.Host.UseDefaultServiceProvider(provider => provider.ValidateScopes = false);

builder.Host.UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration
    .ReadFrom.Configuration(hostingContext.Configuration)
    .Enrich.FromLogContext()
    .Enrich.WithProperty("Host", Environment.MachineName)
    .WriteTo.Console(Serilog.Events.LogEventLevel.Information)
    //.WriteTo.File("log/log.txt", rollingInterval: RollingInterval.Day)
);

builder.Services.AddDbContext<DepartmentContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("Safib.Departments")));

builder.Services.AddScoped<DepartmentRepository>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(DepartmentProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ApiKeyMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.EnsurePopulated();

app.Run();
