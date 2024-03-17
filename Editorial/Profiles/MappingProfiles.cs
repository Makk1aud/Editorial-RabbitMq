using AutoMapper;
using Entitites.Models;
using Shared.DataTransferObjects;

namespace Editorial.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Article, ArticleDTO>().ReverseMap();
        }
    }
}
