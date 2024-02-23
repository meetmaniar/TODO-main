using todo.web;
using todo.web.Extensions;
using todo.web.Services;

var builder = WebApplication.CreateBuilder(args);
AppSettings.Instance = builder.Configuration.Get<AppSettings>();

// Add services to the container.
builder.Services.AddControllersWithViews(o => o.EnableEndpointRouting = false);
builder.Services.AddLocalization();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(AppSettings.Instance.SessionIdleTimeoutMinutes);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddScoped<ITodoClient>(sp => new TodoClient(new HttpClient()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseLocalizedRouteExceptions();
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UsePathLocalization();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseMvc();

app.Run();
