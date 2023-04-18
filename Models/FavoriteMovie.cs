using System;
using System.Collections.Generic;

namespace LettercaixaAPI.Models;

public partial class FavoriteMovie
{
    public int Id { get; set; }

    public int ProfileId { get; set; }

    public int MovieId { get; set; }

    public virtual Profile Profile { get; set; } = null!;
}
