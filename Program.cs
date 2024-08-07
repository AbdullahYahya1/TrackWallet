using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Net.Mail;
using System.Net;
using System.Text;
using TrackWallet.IRepo;
using TrackWallet.Mappings;
using TrackWallet.Repo;
using TrackWallet.Services;
using TrakWallet.Data;
using TrakWallet.Models;
using TrackWallet.Background;
//using TaskMangmentAPI.Filters;
//using TaskMangmentAPI.Mappings;
//using TaskMangmentAPI.Repo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(
    op => op.UseSqlServer(builder.Configuration.GetConnectionString("default")));

builder.Services.AddAuthorizationBuilder();
builder.Services.AddIdentityApiEndpoints<AppUser>()
    .AddEntityFrameworkStores<AppDbContext>();






//builder.Services.AddScoped<AdminFilter>();



builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ITransactionRepo, TransactionRepo>();
builder.Services.AddScoped<IWalletRepo, WalletRepo>();
builder.Services.AddScoped<ICatagoryRepo, CatagoryRepo>();




/*
builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
{
    options.TokenLifespan = TimeSpan.FromDays(365 * 100); 
});

*/
//builder.Services.AddScoped<IAppUserRepo, AppUserRepo>();
//builder.Services.AddScoped<IRoomRepo, RoomRepo>();

//builder.Services.AddAutoMapper(typeof(MappingProfile));


builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        policyBuilder => policyBuilder.WithOrigins("http://localhost:4200")
                                      .AllowAnyHeader()
                                      .AllowAnyMethod()
                                      .AllowCredentials());
});

builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri("https://localhost:4200") });
builder.Services.AddScoped<ImageUploadService>();

//custom user init 
builder.Services.AddScoped<WalletInitializationService>();
builder.Services.AddScoped<IUserStore<AppUser>, CustomUserStore>();

builder.Services.AddAutoMapper(typeof(MappingProfile));


builder.Services.AddTransient<IEmailSender, IEmailSender2>();








builder.Services.AddHangfire(configuration => configuration
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSqlServerStorage(builder.Configuration.GetConnectionString("default"), new Hangfire.SqlServer.SqlServerStorageOptions
    {
        CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
        SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
        QueuePollInterval = TimeSpan.Zero,
        UseRecommendedIsolationLevel = true,
        DisableGlobalLocks = true
    }));

builder.Services.AddHangfireServer();
builder.Services.AddHostedService<EmailBackgroundService>();




var app = builder.Build();
app.UseHttpsRedirection();
app.MapIdentityApi<AppUser>();

app.UseCors("AllowSpecificOrigin");



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseStaticFiles();
app.UseHangfireDashboard(); 

app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();



app.Run();
