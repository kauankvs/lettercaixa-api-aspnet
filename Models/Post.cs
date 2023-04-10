using System;
using System.Collections.Generic;

namespace LettercaixaAPI.Models;

public partial class Post
{
    public int ProfileId { get; set; }
    public int MovieId { get; set; }
    public string Comment { get; set; } = null!;
    public int PostId { get; set; }
    public virtual Profile Profile { get; set; } = null!;
}
