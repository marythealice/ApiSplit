using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApiDb>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("perfume_db")));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddScoped<PerfumeServices>();
builder.Services.AddScoped<BottleServices>();
builder.Services.AddScoped<UserServices>();
builder.Services.AddScoped<SplitServices>();
builder.Services.AddScoped<UserRequestValidator>();

var app = builder.Build();

app.MapPost("/perfumes", async (PerfumeServices perfumeServices, PerfumeRequest request) =>
{
    if (string.IsNullOrEmpty(request.Name))
        return Results.BadRequest("Name cannot be empty");

    var perfume = await perfumeServices.CreatePerfume(request.Name, request.Volume);

    return Results.Created($"/perfumes/{perfume.Id}", perfume);
});

app.MapGet("/perfumes/{id}", async (uint id, PerfumeServices perfumeServices) =>
{
    var perfume = await perfumeServices.GetPerfume(id);
    if (perfume is null)
        return Results.NotFound("Perfume was not found");

    return Results.Ok(perfume);
});


app.MapGet("/perfumes/", async (PerfumeServices perfumeServices) =>
{
    var perfumes = await perfumeServices.GetAllPerfumes();
    if (perfumes is null)
        return Results.NotFound("There are no perfumes available");

    return Results.Ok(perfumes);
});

app.MapDelete("/perfumes/{id}", async (PerfumeServices perfumeServices, uint id) =>
{
    var perfume = await perfumeServices.GetPerfume(id);
    if (perfume is null)
        return Results.NotFound("Perfume was not found");

    await perfumeServices.RemovePerfume(perfume);
    return Results.Ok(perfume);
});

app.MapPost("/bottles", async (BottleRequest request, PerfumeServices perfumeServices, BottleServices bottleServices) =>
{
    var perfume = await perfumeServices.GetPerfume(request.PerfumeId);
    if (perfume is null)
        return Results.NotFound();

    var bottle = await bottleServices.CreateBottle(request);
    return Results.Ok(bottle);
});

app.MapGet("/bottles/{id}", async (uint id, BottleServices bottleServices) =>
{
    var bottle = await bottleServices.BottleExists(id);
    if (bottle is null)
        return Results.NotFound();

    return Results.Ok(bottle);
});

app.MapGet("/bottles/", async (BottleServices bottleServices) =>
{
    var bottles = await bottleServices.GetAllBottles();
    if (bottles is null)
        return Results.NotFound("There are no bottles available");

    return Results.Ok(bottles);
});

app.MapPost("/users", async (UserServices userServices, UserRequest request, UserRequestValidator userReqValidator) =>
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
    if (users is null)
        return Results.NotFound("No user was found. ");

    return Results.Ok(users);
});

app.MapPost("/splits/", async (SplitServices splitservices, SplitRequest request) =>
{
    var split = await splitservices.CreateSplit(request);
    return Results.Ok(split);
});

app.Run();



