using CBCID_APPLICATION.Context;
using CBCID_APPLICATION.Data;
using CBCID_APPLICATION.ICBCID;
using CBCID_APPLICATION.IMPLEMENTATION;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);
var ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddSingleton<DapperContext>(Provider => new DapperContext(ConnectionString));
builder.Services.AddScoped<ILGVIEW, LGSERVICE>();
builder.Services.AddScoped<IDET_CRIME_FEMALE_CHILDREN, DET_CRIME_FEMALE_CHILDREN_SERVICE>();
builder.Services.AddScoped<IGAVAHA,GAVAHA_SERVICE>();
builder.Services.AddScoped<IAddSunvahi, SunvahiService>();
builder.Services.AddScoped<IChangePassword, ChgPassword>();
builder.Services.AddScoped<ISAHAYA_ABIYUKT, SAHAYAK_SERVICE>();
builder.Services.AddScoped<IInvestigation_officer, Investigation_Service>();
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddDistributedMemoryCache();
// Configure session options
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20); // Set session timeout
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication("Cookies")
    .AddCookie("Cookies", options =>
    {
        options.LoginPath = "/Home/Index";
        options.LogoutPath = "/Home/Index";
    });
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddMvc().AddRazorRuntimeCompilation();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseHttpsRedirection();
app.UseSession();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
