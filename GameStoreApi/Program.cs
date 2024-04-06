using GameStoreApi.Data;
using GameStoreApi.EndPoints;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("GameStore");
builder.Services.AddSqlite<GameStoreContext>(connectionString);

var app = builder.Build();

app.MapGamesEndPoints();

app.MigrateDb();

app.Run();
