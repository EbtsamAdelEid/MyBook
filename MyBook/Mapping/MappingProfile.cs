using AutoMapper;
using MyBook.Core.Models;
using MyBook.Core.ViewModel;

namespace MyBook.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Category
            CreateMap<Category, CategoryVM>();
            CreateMap<CategoryViewModel, Category>().ReverseMap();

            //Author
            CreateMap<Author,AuthorVM>();
            CreateMap<AuthorViewModel, Author>().ReverseMap();

        }
    }
}
