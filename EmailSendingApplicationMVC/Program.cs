using EmailSendingApplication.Data;
using EmailSendingApplication.IServices;
using EmailSendingApplication.Mapping;
using EmailSendingApplication.Services;
using EmailSendingApplicationMVC.IServices;
using EmailSendingApplicationMVC.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
 
builder.Services.AddAutoMapper(typeof(MapProfile));

builder.Services.AddScoped<IMailSenderAPIService, MailSenderAPIService>();
builder.Services.AddScoped<IMailRecipientAPIService,MailRecipientAPIService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IMailReportAPIService, MailReportAPIService>();
builder.Services.AddScoped<IMailSendingAPIService, MailSendingAPIService>();

builder.Services.AddHttpClient();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql("PostgreSqlConnection"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
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
    pattern: "{controller=MailSenderMvc}/{action=Index}");

app.Run();
