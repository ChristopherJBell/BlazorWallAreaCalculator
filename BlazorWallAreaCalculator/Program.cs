using BlazorWallAreaCalculator.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Popups;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSyncfusionBlazor();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<IWallService, WallService>();
builder.Services.AddScoped<IDeductionService, DeductionService>();
builder.Services.AddScoped<SfDialogService>();

var app = builder.Build();

//Register Syncfusion license
var SyncfusionLicenceKey = builder.Configuration["SyncfusionLicenceKey"];
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(SyncfusionLicenceKey);



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

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
