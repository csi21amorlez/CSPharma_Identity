using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CSPharma.Data;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("CSPharmaContextConnection") ?? throw new InvalidOperationException("Connection string 'CSPharmaContextConnection' not found.");

builder.Services.AddDbContext<CSPharmaContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQL")));

//builder.Services.AddEntityFrameworkNpgsql()
//    .AddDbContext<CSPharmaContext>(options =>
//    {
//        options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQL"));
//    });

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<CSPharmaContext>();

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapRazorPages();

app.Run();
