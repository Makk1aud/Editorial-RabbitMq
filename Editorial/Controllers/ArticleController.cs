using Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Internal;

namespace Editorial.Controllers
{
    [ApiController]
    [Route("api/articles")]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleRepository _articleRepository;

        public ArticleController(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetArticles()
        {
            var articles = await _articleRepository.GetArticlesAsync();
            return Ok(articles);
        }

        [HttpGet("search")]
        public async Task<IActionResult> FindArticles([FromQuery] string substring)
        {
            var articles = await _articleRepository.SearchEngineArticleAsync(substring);
            return Ok(articles);
        }
    }
}
