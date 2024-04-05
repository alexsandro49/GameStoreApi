using System.ComponentModel.DataAnnotations;

namespace GameStoreApi.Dtos;

public record class UpdateGameDto(
	[Required][StringLength(40)] string Name,
	[Required] string Genre,
    [Required][Range(1, 300)] decimal Price,
    [Required] DateOnly ReleaseDate
);
