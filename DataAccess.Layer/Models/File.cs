using System;
using System.Collections.Generic;

namespace DataAccess.Layer.Models;

public partial class File
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Fpath { get; set; }

    public string? ImgUrl { get; set; }
}
