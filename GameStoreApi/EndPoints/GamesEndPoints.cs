using GameStoreApi.Dtos;
using GameStoreApi.Data;
using GameStoreApi.Entities;
using GameStoreApi.Mapping;
using Microsoft.EntityFrameworkCore;

namespace GameStoreApi.EndPoints;

public static class GamesEndPoints
{
    public static RouteGroupBuilder MapGamesEndPoints(this WebApplication app)
    {
        var group = app.MapGroup("games").WithParameterValidation();

        group.MapPost("/", (CreateGameDto newGame, GameStoreContext dbContext) =>
        {
            Game game = newGame.ToEntity();
            
            dbContext.Games.Add(game);
            dbContext.SaveChanges();

            return Results.CreatedAtRoute(
                GetGameEndPointName, 
                new { id = game.Id }, 
                game.ToGameDetailsDto()
            );
        });

        group.MapGet("/", (GameStoreContext dbContext) => 
            dbContext.Games.Include(game => game.Genre)
                .Select(game => game.ToGameSummaryDto())
                .AsNoTracking());

        group.MapGet("/{id}",(int id, GameStoreContext dbContext) => 
        {
            Game? game = dbContext.Games.Find(id);

            return game is null ? Results.NotFound() 
                : Results.Ok(game.ToGameDetailsDto());

        }).WithName(GetGameEndPointName);

        group.MapPut("/{id}", (int id, UpdateGameDto updatedGame, GameStoreContext dbContext) => 
        {
            var existingGame = dbContext.Games.Find(id);

            if (existingGame is null)
                return Results.NotFound();

            dbContext.Entry(existingGame)
                .CurrentValues.SetValues(updatedGame.ToEntity(id));
            dbContext.SaveChanges();

            return Results.NoContent();
        });

        group.MapDelete("/{id}", (int id, GameStoreContext dbContext) => 
        {
            dbContext.Games.Where(game => game.Id == id).ExecuteDelete();

            return Results.NoContent();
        });

        return group;
    }
}
