using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Builder.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json.Serialization;
using MongoDB.Driver;
using MCSApiInterface.Interfaces;
using MCSApiData.Repositories;

var builder = WebApplication.CreateBuilder(args);


FirebaseApp.Create(new AppOptions()
{
    Credential = GoogleCredential.FromFile(builder.Configuration["CertificatePath"])


});

// firebase auth
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(opt =>
{
    opt.Authority = builder.Configuration["Jwt:Firebase:ValidIssuer"];
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Firebase:ValidIssuer"],
        ValidAudience = builder.Configuration["Jwt:Firebase:ValidAudience"],
    };

});

builder.Services.AddAuthorization(opt =>
{
    opt.AddPolicy("IsGuest", policy =>
        policy.RequireRole(new string[] { "isGuest", "isAdmin", "isPlayer" }));

    opt.AddPolicy("IsPlayer", policy =>
        policy.RequireRole(new string[] { "isAdmin", "isPlayer" }));

    opt.AddPolicy("IsAdmin", policy =>
        policy.RequireRole("isAdmin"));

    opt.AddPolicy("IsService", policy =>
        policy.RequireRole(new string[] { "isAdmin", "isService" }));

});


// Add services to the container.
builder.Services.AddSingleton(sp => new MongoClient(builder.Configuration.GetConnectionString("MongoDb")).GetDatabase(builder.Configuration["MongoDb:DbName"]));
builder.Services.AddTransient<IItemRepository, ItemRepository>();
builder.Services.AddTransient<IServiceRepository, ServiceRepository>();
builder.Services.AddTransient<ISystemRepository, SystemRepository>();


builder.Services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();
app.UseStaticFiles();

app.Run();

