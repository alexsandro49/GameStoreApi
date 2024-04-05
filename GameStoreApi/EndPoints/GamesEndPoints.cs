using GameStoreApi.Dtos;

namespace GameStoreApi.EndPoints;

public static class GamesEndPoints
{
    const string GetGameEndPointName = "GetGame"; 

    private static readonly List<GameDto> games = [
        new(0, "Mortal Kombat", "Fighting",
            59.99m, new DateOnly(1992, 8, 1)),
        new(1, "J-League Winning Eleven", "Sports",
            44.99m, new DateOnly(1995, 7, 21)),
        new(2, "Castlevania: Symphony of the Night", "Action",
            51.99m, new DateOnly(1997, 3, 20))
    ];

    public static RouteGroupBuilder MapGamesEndPoints(this WebApplication app)
    {
        var group = app.MapGroup("games").WithParameterValidation();

        group.MapPost("/", (CreateGameDto newGame) => 
        {
            GameDto game = new(
                games.Any() ? games.Count : 0,
                newGame.Name,
                newGame.Genre,
                newGame.Price,
                newGame.ReleaseDate
            );

            games.Add(game);

            return Results.CreatedAtRoute(GetGameEndPointName, new { id = game.Id }, game);
        });

        group.MapGet("/", () => games);

        group.MapGet("games/{id}",(int id) => 
        {
            GameDto? game = games.Find(game => game.Id == id);

            return game is null ? Results.NotFound() : Results.Ok(game);

        }).WithName(GetGameEndPointName);

        group.MapPut("/{id}", (int id, UpdateGameDto updatedGame) => 
        {
            var index = games.FindIndex(game => game.Id == id);
            
            if (index == -1)
            {
                return Results.NotFound();
            }

            games[index] = new GameDto(
                id, 
                updatedGame.Name, 
                updatedGame.Genre, 
                updatedGame.Price, 
                updatedGame.ReleaseDate
            );

            return Results.NoContent();
        });

        group.MapDelete("/{id}", (int id) => 
        {
            games.RemoveAll(game => game.Id == id);

            return Results.NoContent();
        });

        return group;
    }
}
