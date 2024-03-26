using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RESTwebAPI.Models.Authorization;
using RESTwebAPI.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

var appBuilder = WebApplication.CreateBuilder(args);

// DI registrations
appBuilder.Services.AddSingleton<IProductManagementService, ProductManagementService>();
appBuilder.Services.AddSingleton<IOrderManagementService, OrderManagementService>();
appBuilder.Services.AddSingleton<ICategoryManagementService, CategoryManagementService>();
appBuilder.Services.AddSingleton<IUserAuthenticationService, UserAuthenticationService>();

// MVC and Swagger setup
appBuilder.Services.AddControllers();
appBuilder.Services.AddEndpointsApiExplorer();
appBuilder.Services.AddSwaggerGen(config =>
{
    config.SwaggerDoc("v1", new OpenApiInfo { Title = "RESTful API", Version = "v1" });
    
    config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Please insert 'Bearer' followed by a space and your token.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });

    config.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// Authorization and Authentication setup
appBuilder.Services.AddAuthorization();
appBuilder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opts =>
    {
        opts.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = AuthConfigurations.ISSUER,
            ValidateAudience = true,
            ValidAudience = AuthConfigurations.AUDIENCE,
            ValidateLifetime = true,
            IssuerSigningKey = AuthConfigurations.GetSecurityKey(),
            ValidateIssuerSigningKey = true,
        };
    });

var application = appBuilder.Build();

// Authentication middleware
application.UseAuthentication();

// Development environment setup
if (application.Environment.IsDevelopment())
{
    application.UseSwagger();
    application.UseSwaggerUI(c =>
    {
        c.OAuthClientId("swagger_ui_client_id");
        c.OAuthAppName("RESTful API - Swagger UI");
    });
}
else
{
    application.UseExceptionHandler("/Error");
    application.UseHsts();
}

application.UseHttpsRedirection();

application.UseAuthorization();

application.MapControllers();
application.Map("/authenticate/{userId}", (string userId) =>
{
    var userClaims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, userId) };
    var jwtToken = new JwtSecurityToken(
            issuer: AuthConfigurations.ISSUER,
            audience: AuthConfigurations.AUDIENCE,
            claims: userClaims,
            expires: DateTime.UtcNow.AddMinutes(2), // Token validity set to 2 minutes
            signingCredentials: new SigningCredentials(AuthConfigurations.GetSecurityKey(), SecurityAlgorithms.HmacSha256));

    return new JwtSecurityTokenHandler().WriteToken(jwtToken);
});

application.Run();
