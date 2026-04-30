using ClassLibrary1;
using ClassLibrary1.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Додаємо контролери
builder.Services.AddControllers();

// Налаштування Swagger для зручного тестування API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// --- ВПРОВАДЖЕННЯ ЗАЛЕЖНОСТЕЙ (Dependency Injection) ---

// 1. Реєструємо DbContext
builder.Services.AddDbContext<MotorsportContext>();

// 2. Реєструємо Репозиторій (Дженерік)
builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

// 3. Реєструємо CRUD Сервіс (Дженерік)
builder.Services.AddScoped(typeof(ICrudService<>), typeof(CrudService<>));

// -------------------------------------------------------

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<MotorsportContext>(); // Укажите правильное имя вашего DbContext
    context.Database.EnsureCreated(); // Создает базу и таблицы, если их нет
}

// Налаштування HTTP конвеєра
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();