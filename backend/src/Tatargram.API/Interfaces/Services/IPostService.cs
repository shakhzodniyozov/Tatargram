using Tatargram.Models;

namespace Tatargram.Interfaces.Services;

public interface IPostService : IService<Post, PostBaseQueryModel>
{
    Task<IEnumerable<PostViewModel>> GetPagedFeedList(int page = 1, int pageSize = 30);
    Task LikeThePost(Guid id);
    Task UnlikeThePost(Guid id);
    Task<IEnumerable<object>> GetLikedUsers(Guid postId);
}