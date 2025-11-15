

public static class ProfileAssistantEndpoints
{
    public static void MapProfileAssistantEndpoints(this IEndpointRouteBuilder app)
    {
        var profileGroup = app.MapGroup("/api/profile");

        profileGroup.MapGet("/{id}", (string id) =>
        {
            return Results.Ok("FROM PROFILE");
        });

    }
}