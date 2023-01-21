using System;
using System.Collections.Generic;

namespace LettercaixaAPI.Models;

public partial class FavoriteMovie
{
    public int FavoriteMovies { get; set; }

    public int ProfileId { get; set; }

    public string? MovieOne { get; set; }

    public string? MovieTwo { get; set; }

    public string? MovieThree { get; set; }

    public string? MovieFour { get; set; }

    public string? MovieFive { get; set; }

    public string? MovieSix { get; set; }

    public string? MovieSeven { get; set; }

    public string? MovieEight { get; set; }

    public string? MovieNine { get; set; }

    public string? MovieTen { get; set; }

    public virtual Profile Profile { get; set; } = null!;
}
