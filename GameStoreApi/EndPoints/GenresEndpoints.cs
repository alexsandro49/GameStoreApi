using GameStoreApi.Data;
using GameStoreApi.Entities;
using GameStoreApi.Mapping;
using Microsoft.EntityFrameworkCore;

namespace GameStoreApi.Endpoints;

public static class GenresEndpoints
{
    public static RouteGroupBuilder MapGenresEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("genres");

        group.MapGet("/", async (GameStoreContext dbContext) =>    
            await dbContext.Genres.Select(genre => genre.ToDto())
                .AsNoTracking().ToListAsync()
        );

        return group;
    }
}