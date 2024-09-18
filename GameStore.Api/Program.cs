using GameStore.Api.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

const string GetNameEndpointName = "GetGame";

List<GameDto> games =
[
    new GameDto(1, "Street Fighter 2", "Fighting", 19.99M, new DateOnly(1992, 7, 15)),
    new GameDto(2, "Narita", "Roleplaying", 59.99M, new DateOnly(2010, 7, 15)),
    new GameDto(3, "FIFA 23", "Sports", 63.99M, new DateOnly(2022, 9, 27)),
];

//GET /games
app.MapGet("games", () => games);

//GET /games/1
app.MapGet("games/{id}", (int id)=> games.Find(game =>game.Id == id)).WithName(GetNameEndpointName);

app.MapPost("games", (CreateGameDto newGame) =>{
    GameDto gameDto = new (
        games.Count + 1,
        newGame.Name,
        newGame.Genre,
        newGame.Price,
        newGame.ReleaseDate
    );
    games.Add(gameDto);

    return Results.CreatedAtRoute(GetNameEndpointName, new{ id = gameDto.Id}, gameDto);   
});

app.Run();
