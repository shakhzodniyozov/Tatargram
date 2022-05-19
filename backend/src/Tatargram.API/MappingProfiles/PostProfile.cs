using AutoMapper;
using Humanizer;
using Tatargram.Models;
using Tatargram.Posts.QueryModels;

namespace Tatargram.MappingProfiles;

public class PostProfile : Profile
{
    public PostProfile()
    {
        CreateMap<CreatePostQueryModel, Post>()
            .ForMember(d => d.Photos, opt => opt.Ignore());
        CreateMap<Post, PostBaseViewModel>()
            .Include<Post, PostViewModel>();

        CreateMap<Post, PostViewModel>()
            .ForMember(d => d.PublishDate, opt => opt.MapFrom(src => src.PublishDate.Humanize(null, null, new("ru-RU"))))
            .ForMember(d => d.Likes, opt => opt.MapFrom(src => src.LikedUsers.Count))
            .ForMember(d => d.Images, opt => opt.MapFrom(src => src.Photos.Select(x => x.RelativePaths)))
            .ForMember(d => d.AuthorFullName, opt => opt.MapFrom(src => src.Author.FirstName + " " + src.Author.LastName))
            .ForMember(d => d.AuthorPhoto, opt => opt.MapFrom(src => src.Author.ProfileImage));

        CreateMap<UpdatePostQueryModel, Post>();
    }
}