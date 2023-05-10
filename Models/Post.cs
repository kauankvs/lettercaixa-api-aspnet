using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LettercaixaAPI.Models;

public partial class Post
{
    public int ProfileId { get; set; }

    public int MovieId { get; set; }

    public string? Comment { get; set; }

    public int PostId { get; set; }

    [JsonIgnore]
    public virtual Profile Profile { get; set; } = null!;
}
