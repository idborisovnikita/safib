var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration
    .ReadFrom.Configuration(hostingContext.Configuration)
    .Enrich.FromLogContext()
    .Enrich.WithProperty("Host", Environment.MachineName)
    .WriteTo.Console(Serilog.Events.LogEventLevel.Information)
    //.WriteTo.File("log/log.txt", rollingInterval: RollingInterval.Day)
);

// Add services to the container.
builder.Services
    .AddControllersWithViews()
    .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles); 

builder.Services.AddTransient<IDepartmentsService, DepartmentsService>();

builder.Services.AddHttpClient<IDepartmentsService, DepartmentsService>(x => {
    x.BaseAddress = new Uri(builder.Configuration.GetSection("API")["DepartmentsUrl"]);
    x.DefaultRequestHeaders.Add("x-api-key", builder.Configuration.GetSection("Api")["ApiKey"]);
}).ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler {
    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }//исключительно только в тестовых целях :)
}); 

builder.Services.AddAutoMapper(typeof(DepartmentProfile));

builder.Services.AddSignalR(options => {
    options.EnableDetailedErrors = true;
    options.ClientTimeoutInterval = TimeSpan.FromSeconds(120);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseEndpoints(endpoints => {
    endpoints.MapHub<ApplicationHub>("/app");
});

app.Run();
