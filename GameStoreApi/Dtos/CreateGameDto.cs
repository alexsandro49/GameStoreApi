using System.ComponentModel.DataAnnotations;

namespace GameStoreApi.Dtos;

public record class CreateGameDto(
	[Required][StringLength(40)] string Name,
	[Required] int GenreId,
    [Required][Range(1, 300)] decimal Price,
    [Required] DateOnly ReleaseDate
);
