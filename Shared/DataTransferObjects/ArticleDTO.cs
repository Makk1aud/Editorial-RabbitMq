using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record ArticleDTO
    {
        public Guid ArticleId { get; init; }

        public string Title { get; init; }

        public string Text { get; init; }

        public Guid ArticleTypeId { get; init; }
    }
}
