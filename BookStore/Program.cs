using BookStore.Data;
using BookStore.Repositories;
using BookStore.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

/*
string dbHost = Environment.GetEnvironmentVariable("DATABASE_HOST");
string dbName = Environment.GetEnvironmentVariable("DATABASE_NAME");
string dbUser = Environment.GetEnvironmentVariable("DATABASE_USER");
string dbPassword = Environment.GetEnvironmentVariable("DATABASE_PASSWORD"); 
string connectionString = $"Host={dbHost};Database={dbName};Username={dbUser};Password={dbPassword};";
*/

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddTransient<IBookRepository, BookRepository>();
builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();

builder.Services.AddTransient<IBookService, BookService>();
builder.Services.AddTransient<ICategoryService, CategoryService>();


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.MapControllers();

app.UseStaticFiles();

app.Run();
