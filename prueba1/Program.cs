using VelazquezYahir.Services.IServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using VelazquezYahir.Context;
using VelazquezYahir.Services.Services;
using VelazquezYahir.Middleware;

var builder = WebApplication.CreateBuilder(args);
// Otros servicios
builder.Services.AddDbContext<ApplicationDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IBookService, BookService>();
builder.Services.AddTransient<ICategoriaService, CategoriaService>();
builder.Services.AddTransient<IComentariosService, ComentariosService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

// 2. Configurar JWT (ya lo tienes)
var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = "Cookies"; // Añadir soporte para autenticación basada en cookies
})
.AddCookie("Cookies") // Configurar autenticación con cookies
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero // Elimina la tolerancia en la expiración del token
    };
});

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseJwtMiddleware(); // Middleware JWT (debe estar antes de autenticación)
app.UseAuthentication();
app.UseAuthorization();


// Configuración del pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Index}/{id?}");

app.Run();
