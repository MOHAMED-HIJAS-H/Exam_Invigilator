using Exam_Invigilator.DataAccess;
using Microsoft.EntityFrameworkCore;

using Exam_Invigilator.Domain.Interfaces;
using Exam_Invigilator.Service;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ExamDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IInvigilatorService, InvigilatorService>();
builder.Services.AddScoped<AdminRepository>();              
builder.Services.AddScoped<InvigilatorRepository>();
builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSession();

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Admin}/{action=Login}/{id?}")
    .WithStaticAssets();


app.Run();
