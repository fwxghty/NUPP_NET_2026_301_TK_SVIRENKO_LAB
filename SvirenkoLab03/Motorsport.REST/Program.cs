using ClassLibrary1;
using ClassLibrary1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Сервисы
builder.Services.AddDbContext<ClassLibrary1.MotorsportContext>();
builder.Services.AddIdentityApiEndpoints<ClassLibrary1.Models.ApplicationUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ClassLibrary1.MotorsportContext>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); // Цей рядок відповідає за саму сторінку в браузері
}

// Инициализация БД (EnsureCreated)
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ClassLibrary1.MotorsportContext>();
    context.Database.EnsureCreated();
}

// Middleware (Порядок критичен!)
app.UseHttpsRedirection();

app.UseAuthentication(); // Кто ты?
app.UseAuthorization();  // Что тебе можно?

// Конечные точки Identity (Login, Register и т.д.)
app.MapIdentityApi<ClassLibrary1.Models.ApplicationUser>();

app.MapControllers();

app.Run();