using AutoMapper;
using Tatargram.Comments.QueryModels;
using Tatargram.Comments.ViewModels;
using Tatargram.Models;

namespace Tatargram.MappingProfiles;

public class CommentProfile : Profile
{
    public CommentProfile()
    {
        CreateMap<CommentBaseQueryModel, Comment>();

        CreateMap<Comment, CommentBaseViewModel>()
            .ForMember(x => x.UserFullName, opt => opt.MapFrom(x => x.User.FirstName + " " + x.User.LastName))
            .ForMember(x => x.UserProfileImage, opt => opt.MapFrom(x => x.User.ProfileImage))
            .ForMember(x => x.PublishDate, opt => opt.MapFrom(src => src.PublishDate.ToString("dd.MM.yyyy hh:mm")));

        CreateMap<UpdateCommentQueryModel, Comment>();
    }
}