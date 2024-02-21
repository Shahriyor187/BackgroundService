using Background;
using Background.Service;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton(c => new TodoDb(
    builder.Configuration.GetConnectionString("MongoDB")!,
    builder.Configuration.GetConnectionString("Database")!
));

builder.Services.AddSingleton<IMongoDatabase>(c =>
{
    var dbContext = c.GetRequiredService<TodoDb>();
    return dbContext._database;
});
builder.Services.AddSingleton<EmailService>();
builder.Services.AddScoped<ReminderService>();
//IHostedService 
builder.Services.AddHostedService<ReminderService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
