using Application.Commands.MovieCommand;
using Application.Queries.MovieQueries;
using Application.Queries.OmdbQueries;
using Domain.DTOs;
using Domain.Interfaces;
using Domain.Models;
using Domain.Services;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Infrastructure.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("MovieConnection") ?? throw new InvalidOperationException("Connection string 'MovieConnection' not found.");

builder.Services.AddDbContext<MovieDbContext>(options =>
            options.UseSqlServer(connectionString));
builder.Services.AddControllers();

// Register repositories
builder.Services.AddScoped<IMovieRepository, MovieRepository>(); // Register your movie repository
                                                                 // Register HttpClient
builder.Services.AddHttpClient<IOmdbApiClient, OmdbApiClient>();

// Register command and query handlers
//builder.Services.AddScoped<IAddMovieHandler, AddMovieHandler>();
//builder.Services.AddScoped<IUpdateMovieHandler, UpdateMovieHandler>();
//builder.Services.AddScoped<IDeleteMovieHandler, DeleteMovieHandler>();
//builder.Services.AddScoped<IListMoviesHandler, ListMoviesHandler>();
builder.Services.AddTransient<IRequestHandler<MovieSearchByTitleQuery, MovieSearchResultDto>, MovieSearchByTitleQuery.MovieSearchByTitleHandler>();
builder.Services.AddTransient<IRequestHandler<MovieGetByTitleQuery, MovieDetailsDto>, MovieGetByTitleQuery.MovieGetByTitleHandler>();
builder.Services.AddTransient<IRequestHandler<MovieGetByIdQuery, MovieDetailsDto>, MovieGetByIdQuery.MovieGetByIdHandler>();

// Register AddMovieCommandHandler
builder.Services.AddScoped<IRequestHandler<AddMovieCommand>>(sp =>
{
    // Resolve IMovieRepository
    var movieRepository = sp.GetRequiredService<IMovieRepository>();
    // Create AddMovieCommandHandler with IMovieRepository dependency
    return new AddMovieCommandHandler(movieRepository);
});

// Register AddMoviesCommandHandler
builder.Services.AddScoped<IRequestHandler<AddMoviesCommand>>(sp =>
{
    // Resolve IMovieRepository
    var movieRepository = sp.GetRequiredService<IMovieRepository>();
    // Create AddMoviesCommandHandler with IMovieRepository dependency
    return new AddMoviesCommandHandler(movieRepository);
});

// Register DeleteMovieCommandHandler
builder.Services.AddScoped<IRequestHandler<DeleteMovieCommand>>(sp =>
{
    // Resolve IMovieRepository
    var movieRepository = sp.GetRequiredService<IMovieRepository>();
    // Create DeleteMovieCommandHandler with IMovieRepository dependency
    return new DeleteMovieCommandHandler(movieRepository);
});

// Register UpdateMovieCommandHandler
builder.Services.AddScoped<IRequestHandler<UpdateMovieCommand>>(sp =>
{
    // Resolve IMovieRepository
    var movieRepository = sp.GetRequiredService<IMovieRepository>();
    // Create UpdateMovieCommandHandler with IMovieRepository dependency
    return new UpdateMovieCommandHandler(movieRepository);
});
// Register ListMoviesQueryHandler
builder.Services.AddScoped<IRequestHandler<ListMoviesQuery, List<Movie>>, ListMoviesQuery.ListMoviesQueryHandler>();



builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// Register Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Omdbapi.com Service API", Version = "v1" });
});
builder.Services.AddCors(options =>
{
options.AddDefaultPolicy(
   policy =>
            {
    policy.AllowAnyOrigin()
          .AllowAnyHeader()
            .AllowAnyMethod();
});
    });


 


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Enable Swagger UI in development environment
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Omdbapi.com Service API v1"));
}



app.UseHttpsRedirection();
app.UseCors();
app.UseAuthorization();

app.MapControllers();

app.Run();
