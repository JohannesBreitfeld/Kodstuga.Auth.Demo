using Backend.MinimalApi.DTO;

namespace Backend.MinimalApi.Endpoints;

public static class AnimalEndpoints
{
    private static List<Animal> _animals = new()
    {
        new Animal(Type: "dog", Sound:"Wof wof"),
        new Animal(Type: "cat", Sound:"Meow"),
        new Animal(Type: "duck", Sound:"Quack")
    };

    public static IEndpointRouteBuilder MapAnimalEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/animals");

        group.MapGet("", GetAll);

        group.MapGet("{type}", GetByType);

        group.MapPost("{animal}", AddAnimal);
        
        group.MapDelete("{type}", DeleteByType); 

        return app;
    }

    private static IResult GetAll()
    {
        return Results.Ok(_animals);
    }

    private static IResult GetByType(string type)
    {
        var animal = _animals.FirstOrDefault(a => a.Type == type);

        return animal is not null 
            ? Results.Ok(animal) 
            : Results.NotFound();
    }

    private static IResult AddAnimal(Animal animal)
    {
        if(animal is null || animal.Type is null || animal.Sound is null)
        {
            return Results.BadRequest("Properties can not be null");
        }

        if(_animals.Any(a => a.Type == animal.Type))
        {
            return Results.BadRequest("Type already exists");
        }

        _animals.Add(animal);
        return Results.Created();
    }

    private static IResult DeleteByType(string type)
    {
        var animalToDelete = _animals.FirstOrDefault(a => a.Type == type);

        if(animalToDelete is null)
        {
            return Results.NotFound("Type not found");
        }

        _animals.Remove(animalToDelete);
        return Results.Ok();

    }

}
