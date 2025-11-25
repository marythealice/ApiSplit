using ApiSplit.Repository;
using ApiSplit.Requests;
using ApiSplit.Services;
using ApiSplit.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;



var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApiDb>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("perfume_db")));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddScoped<PerfumeServices>();
builder.Services.AddScoped<BottleServices>();
builder.Services.AddScoped<SplitServices>();
builder.Services.AddScoped<UserRequestValidator>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options =>
    {
        options.RouteTemplate = "/openapi/{documentName}.json";
    });
    app.MapScalarApiReference();
}


app.MapPost("/perfumes", async (PerfumeServices perfumeServices, PerfumeRequest request) =>
{
    if (string.IsNullOrEmpty(request.Name))
        return Results.BadRequest("Name cannot be empty");

    var perfume = await perfumeServices.CreatePerfumeAsync(request);
    return perfume == null ? Results.BadRequest(perfume) : Results.Created($"/perfumes/{perfume.Id}", perfume);
});

app.MapGet("/perfumes/{id}", async (uint id, PerfumeRepository perfumeRepository) =>
{
    var perfume = await perfumeRepository.GetPerfume(id);
    return perfume is null ? Results.NotFound("Perfume was not found") : Results.Ok(perfume);
});


app.MapGet("/perfumes/", async (PerfumeRepository perfumeRepository) =>
{
    var perfumes = await perfumeRepository.GetAllPerfumes();
    return perfumes.Count == 0 ? Results.NotFound("There are no perfumes available") : Results.Ok(perfumes);
});

app.MapDelete("/perfumes/{id}", async (PerfumeRepository perfumeRepository, uint id) =>
{
    var perfumeWasRemoved = await perfumeRepository.RemovePerfume(id);
    return perfumeWasRemoved == 0 ? Results.NotFound("Perfume was not found") : Results.Ok(perfumeWasRemoved);
});

app.MapPost("/bottles", async (BottleRequest request, PerfumeRepository perfumeRepository, BottleServices bottleServices) =>
{
    var perfume = await perfumeRepository.GetPerfume(request.PerfumeId);
    if (perfume is null)
        return Results.NotFound("Perfume was not found");

    var bottle = await bottleServices.CreateBottle(request);
    return Results.Ok(bottle);
});

app.MapGet("/bottles/{id}", async (uint id, BottleRepository bottleRepository) =>
{
    var bottle = await bottleRepository.GetBottle(id);
    return bottle is null ? Results.NotFound("Bottle was not found.") : Results.Ok(bottle);
});

app.MapGet("/bottles/", async (BottleRepository bottleRepository) =>
{
    var bottles = await bottleRepository.GetAllBottles();
    return bottles.Count == 0 ? Results.NotFound("There are no bottles available") : Results.Ok(bottles);
});

app.MapPost("/users", async (UserServices userServices, UserRequestValidator userReqValidator, UserRequest request) =>
{
    var result = userReqValidator.Validate(request);
    if (!result.IsValid)
        return Results.ValidationProblem(result.ToDictionary());

    var user = await userServices.CreateUser(request);
    return Results.Ok(user);
});

app.MapGet("/users/{id}", async (UserServices userServices, uint id) =>
{
    var user = await userServices.GetUser(id);
    if (user is null)
        return Results.NotFound("No such user was found. ");

    return Results.Ok(user);
});

app.MapGet("/users/", async (UserServices userServices) =>
{
    var users = await userServices.GetAllUsers();
    return users.Count == 0 ? Results.NotFound("No user was found. ") : Results.Ok(users);
});

app.MapPost("/splits/", async (BottleRepository bottleRepository, SplitServices splitServices, SplitRequest request) =>
{
    var bottle = await bottleRepository.GetBottle(request.BottleId);
    if (bottle is null)
        return Results.NotFound("Bottle was not found");

    if (request.Volume <= bottle.CurrentVolume)
    {
        var split = await splitServices.CreateSplit(request);
        return Results.Ok(split);
    }

    return Results.UnprocessableEntity("The split volume is greater than the bottle volume ({bottle.CurrentVolume})");

});

app.Run();



 