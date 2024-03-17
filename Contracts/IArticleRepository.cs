using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IArticleRepository
    {
        Task<IEnumerable<ArticleDTO>> GetArticlesAsync();
        Task<IEnumerable<ArticleDTO>> SearchEngineArticleAsync(string searchSubstring);
        Task SaveChangesAsync();
    }
}
