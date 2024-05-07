using EntityFrameworkSP_Demo.Data;
using EntityFrameworkSP_Demo.Helpers.FileHelpers;
using EntityFrameworkSP_Demo.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using ConfigurationManager = EntityFrameworkSP_Demo.ConfigurationManager;

var builder = WebApplication.CreateBuilder(args);

{
    // Replace "Your_Trial_License_Key_Here" with the actual trial license key you received
    License.LicenseKey = "IRONSUITE.BRAJESH.KUMAR17405.MARWADIEDUCATION.EDU.IN.26743-1F55272C0C-AIZOULG-UXRCYLO75FHF-R2DQCVZHNB56-THB7FGYZRGUG-TJAJPTL44OQ3-MJVX233KOE6I-OCSNKI5PVIJ2-HB3CYW-TRERQWVN7FCMUA-DEPLOYMENT.TRIAL-HQBHIN.TRIAL.EXPIRES.29.MAY.2024";
}


// Add services to the container.
builder.Services.AddScoped< IProductService ,ProductService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IImageUploadDal, ImageUploadDal>();
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<IOrder, OrderService>();
builder.Services.AddScoped<IFileHelper, FileHelper>();
builder.Services.AddDbContext<DbContextClass>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("V1", new OpenApiInfo
    {
        Version = "V1",
        Title = "EntityFrameworkSP_Demo",
        Description = "Product,Contact,Employee,ImageUpload EntityFrameworkSP_Demo"
    });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Description = "Bearer Authentication with JWT Token",
        Type = SecuritySchemeType.Http
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }
    });
});
builder.Services.AddAuthentication(opt => {
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = ConfigurationManager.AppSetting["JWT:ValidIssuer"],
            ValidAudience = ConfigurationManager.AppSetting["JWT:ValidAudience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationManager.AppSetting["JWT:Secret"]))
        };
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => {
        options.SwaggerEndpoint("/swagger/V1/swagger.json", "Product,Contact,Employee,ImageUpload EntityFrameworkSP_Demo");
    });
}
app.UseHttpsRedirection();
app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();





