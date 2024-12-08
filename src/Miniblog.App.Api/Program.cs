using Miniblog.App.Application;
using Miniblog.App.Application.UseCases.Authorization;
using Miniblog.App.Application.UseCases.Posts;
using Miniblog.App.Application.UseCases.Users;
using Miniblog.App.Application.WebSocket;
using Miniblog.App.Data;
using Miniblog.App.Domain.Shared;
using Miniblog.App.Security;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "Insira o token abaixo, não é necessário incluir Bearer no início",
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Id = "Bearer",
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }
    });
});

builder.Services.AddSignalR();

builder.Services.AddSecurity(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddData(builder.Configuration);

builder.Services.AddCors();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseDatabaseInitialization<MiniblogDbContext>();

app.MapHub<NotificationHub>("notifications");

app.MapPost("token", async (ISender mediatr, [FromBody] LoginCommand request, CancellationToken cancellationToken) =>
{
    var loginResult = await mediatr.Send(request, cancellationToken);

    if (loginResult.IsFailed)
        return Results.BadRequest(loginResult.Errors);

    return Results.Ok(loginResult.Value);
})
.WithTags("Autorização")
.WithOpenApi();

app.MapPost("usuario", async (ISender mediatr, [FromBody] CreateNewUserCommand request, CancellationToken cancellationToken) =>
{
    var newUserResult = await mediatr.Send(request, cancellationToken);

    if (newUserResult.IsFailed)
        return Results.BadRequest(newUserResult.Errors);

    return Results.Created();
})
.WithTags("Usuários")
.WithOpenApi();

app.MapPost("post", [Authorize] async (ISender mediatr, [FromBody] CreateNewPostCommand request, CancellationToken cancellationToken) =>
{
    var newPostResult = await mediatr.Send(request, cancellationToken);

    if (newPostResult.IsFailed)
        return Results.BadRequest(newPostResult.Errors);

    return Results.Created();
})
.WithTags("Posts")
.WithOpenApi();

app.MapGet("post", async (ISender mediatr, CancellationToken cancellationToken) =>
{
    var query = new RetornaPostsQuery();

    var postsResult = await mediatr.Send(query, cancellationToken);

    if (postsResult.IsFailed)
    {
        var notFoundErrorResult = postsResult.Errors
            .OfType<CustomError>()
            .FirstOrDefault(e => e.ErrorCode == 404);

        if (notFoundErrorResult is not null)
            return Results.NotFound(notFoundErrorResult);


        return Results.BadRequest(postsResult.Errors);
    }

    return Results.Ok(postsResult.Value);
})
.WithTags("Posts")
.WithOpenApi();

app.MapPut("post/{id}", [Authorize] async (ISender mediatr, [FromRoute] Guid id, [FromBody] UpdatePostRequest request, CancellationToken cancellationToken) =>
{
    var command = new UpdatePostCommand
    {
        Id = id,
        UserId = request.UserId,
        Title = request.Title,
        Description = request.Description
    };

    var postsResult = await mediatr.Send(command, cancellationToken);

    if (postsResult.IsFailed)
    {
        var notFoundErrorResult = postsResult.Errors
            .OfType<CustomError>()
            .FirstOrDefault(e => e.ErrorCode == 404);

        if (notFoundErrorResult is not null)
            return Results.NotFound(notFoundErrorResult);


        return Results.BadRequest(postsResult.Errors);
    }

    return Results.NoContent();
})
.WithTags("Posts")
.WithOpenApi();

app.MapDelete("post/{id}", [Authorize] async (ISender mediatr, [FromRoute] Guid id, CancellationToken cancellationToken) =>
{
    var command = new DeletePostCommand
    {
        Id = id
    };

    var postsResult = await mediatr.Send(command, cancellationToken);

    if (postsResult.IsFailed)
    {
        var notFoundErrorResult = postsResult.Errors
            .OfType<CustomError>()
            .FirstOrDefault(e => e.ErrorCode == 404);

        if (notFoundErrorResult is not null)
            return Results.NotFound(notFoundErrorResult);


        return Results.BadRequest(postsResult.Errors);
    }

    return Results.Ok();
})
.WithTags("Posts")
.WithOpenApi();

app.Run();