using System.Security.Claims;
using Backend.MinimalApi.Dtos.Identity;
using Microsoft.AspNetCore.Identity;

namespace Backend.MinimalApi.Endpoints;

public static class CustomAuthEndPoints
{
    public static IEndpointRouteBuilder MapCustomAuthEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("register-admin", RegisterAdmin);

        return app;
    }

    private static async Task<IResult> RegisterAdmin(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, RegisterDto dto)
    {
        var user = new IdentityUser(dto.Email);
        var result = await userManager.CreateAsync(user, dto.Password);

        if (!result.Succeeded)
        {
            return Results.BadRequest("Could not create user for whatever reason");
        }

        var roleExists = await roleManager.RoleExistsAsync("ADMIN");

        if (!roleExists)
        {
            await roleManager.CreateAsync(new IdentityRole("ADMIN"));
        }

        await userManager.AddToRoleAsync(user, "ADMIN");

        return Results.Ok();
    }

}