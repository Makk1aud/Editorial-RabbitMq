using System;
using System.Collections.Generic;

namespace Entitites.Models;

public partial class Articletype
{
    public Guid ArticleTypeId { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<Article> Articles { get; set; } = new List<Article>();
}
