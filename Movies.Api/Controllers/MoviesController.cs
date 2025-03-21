﻿using Microsoft.AspNetCore.Mvc;
using Movies.Application.Models;
using Movies.Application.Repositories;
using Movies.Contracts.Requests;
using Movies.Contracts.Responses;

namespace Movies.Api.Controllers;

[ApiController]
[Route("api")]
public class MoviesController : ControllerBase
{
    private readonly IMovieRepository _movieRepository;

    public MoviesController(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }
    
    [HttpPost("movies")]
    public async Task<IActionResult> Create([FromBody] CreateMovieRequest request)
    {
        var movie = new Movie()
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            YearOfRelease = request.YearOfRelease,
            Genres = request.Genres.ToList(),
        };

        var response = new MovieResponse()
        {
            Id = movie.Id,
            Title = movie.Title,
            YearOfRelease = movie.YearOfRelease,
            Genres = movie.Genres,
        };

        await _movieRepository.CreateAsync(movie);
        return Created($"api/movies/{response.Id}", response);
    }
}