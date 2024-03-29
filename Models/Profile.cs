﻿using System;
using System.Collections.Generic;

namespace LettercaixaAPI.Models;

public partial class Profile
{
    public int ProfileId { get; set; }

    public string Username { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string? ProfilePicture { get; set; }

    public DateTime Birth { get; set; }

    public virtual ICollection<FavoriteMovie> FavoriteMovies { get; set; } = new List<FavoriteMovie>();

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}
