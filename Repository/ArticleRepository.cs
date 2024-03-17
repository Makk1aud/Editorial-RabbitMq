using AutoMapper;
using Contracts;
using Microsoft.EntityFrameworkCore;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly IMapper _mapper;
        private readonly RabbitEditorialContext _context;

        public ArticleRepository(IMapper mapper, RabbitEditorialContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<IEnumerable<ArticleDTO>> GetArticlesAsync()
        {
            var articles = await _context.Articles.ToListAsync();
            var articleDTOs = _mapper.Map<IEnumerable<ArticleDTO>>(articles);
            return articleDTOs;
        }

        public async Task SaveChangesAsync() =>
            await _context.SaveChangesAsync();

        public async Task<IEnumerable<ArticleDTO>> SearchEngineArticleAsync(string searchSubstring)
        {
            var articles = await GetArticlesAsync();
            return articles.Where(x => x.Text.Contains(searchSubstring, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
