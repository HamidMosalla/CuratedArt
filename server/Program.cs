using CuratedArt.Data;
using CuratedArt.Data.Seeds;
using CuratedArt.FrontEndArchitecture;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CuratedArt.Services;
using Microsoft.AspNetCore.Mvc.Versioning;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<CuratedArtDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<CuratedArtDbContext>();

//ATTENTION: AddNewtonsoftJson is only here to support JsonPatch
builder.Services.AddControllersWithViews().AddNewtonsoftJson(); ;

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddScoped<FrontEndAssetRenderer>();
}
else
{
    builder.Services.AddSingleton<FrontEndAssetRenderer>();
}

builder.Services.AddApiVersioning(o =>
{
    o.AssumeDefaultVersionWhenUnspecified = true;
    o.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    o.ReportApiVersions = true;
    o.ApiVersionReader = ApiVersionReader.Combine(
        new QueryStringApiVersionReader("api-version"),
        new HeaderApiVersionReader("X-Version"),
        new MediaTypeApiVersionReader("ver"));
});

builder.Services.AddVersionedApiExplorer(
    options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IArtWorkService, ArtWorkService>();
builder.Services.AddTransient<SeedArtWorks>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    // TODO: need to add check here to only run migrations if it was applicable
    var db = scope.ServiceProvider.GetRequiredService<CuratedArtDbContext>();
    await db.Database.MigrateAsync();

    var seeder = new SeedArtWorks(db);

    if (!db.ArtWorks.Any())
    {
        await seeder.Seed();
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
};

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

// TODO: might use minimal APIs later
//app.MapArtWorkEndpoints();

app.Run();

public partial class Program { }