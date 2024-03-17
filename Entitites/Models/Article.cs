using System;
using System.Collections.Generic;

namespace Entitites.Models;

public partial class Article
{
    public Guid ArticleId { get; set; }

    public string Title { get; set; } = null!;

    public string Text { get; set; } = null!;

    public Guid ArticleTypeId { get; set; }

    public virtual Articletype ArticleType { get; set; } = null!;
}
