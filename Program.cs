using license.Entity;
using license;
using Microsoft.AspNetCore.Mvc;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEntityFrameworkSqlite().AddDbContext<UserDbContext>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapPost("/add", async ([FromBody] UserAddModel User, [FromServices] UserDbContext db) =>
{
    var user = new User { Name = User.Name, Key = User.Key };
    db.Users.Add(user);
    db.SaveChanges();
}).WithOpenApi();

app.MapPost("/check",
        ([FromBody] string key, [FromServices] UserDbContext db) => db.Users.FirstOrDefault(x => x.Key == key) != null)
    .WithOpenApi();

app.MapGet("/delete", async ([FromBody] string key, [FromServices] UserDbContext db) =>
    {
        var user = db.Users.First(x => x.Key == key);
        db.Users.Remove(user);
        db.SaveChanges();
    })
    .WithOpenApi();

app.MapGet("/all", async ([FromServices] UserDbContext db) => db.Users)
    .WithOpenApi();

app.Run();