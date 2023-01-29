using System;
using System.Collections.Generic;

namespace LettercaixaAPI.Models;

public partial class Favorite
{
    public int? MovieOne { get; set; }

    public int? MovieTwo { get; set; }

    public int? MovieThree { get; set; }

    public int? MovieFour { get; set; }

    public int? MovieFive { get; set; }

    public int? MovieSix { get; set; }

    public int? MovieSeven { get; set; }

    public int? MovieEight { get; set; }

    public int? MovieNine { get; set; }

    public int? MovieTen { get; set; }

    public int ProfileId { get; set; }

    public int FavoriteId { get; set; }

    public virtual Profile Profile { get; set; } = null!;
}
