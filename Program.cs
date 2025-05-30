using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApiDb>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("perfume_db")));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddScoped<PerfumeServices>();
builder.Services.AddScoped<BottleServices>();
builder.Services.AddScoped<UserServices>();

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
        return Results.NotFound();

    return Results.Ok(perfume);
});


app.MapGet("/perfumes/", async (PerfumeServices perfumeServices) =>
{
    var perfumes = await perfumeServices.GetAllAsync();
    if (perfumes is null)
        return Results.NotFound("There are no perfumes available");

    return Results.Ok(perfumes);
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

app.MapPost("/users", async (UserServices userServices, UserRequest request) =>
{
    var user = await userServices.CreateUser(request);
    return Results.Ok(user);
});

app.Run();



