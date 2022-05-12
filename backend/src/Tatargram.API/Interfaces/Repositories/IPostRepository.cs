using System.Linq.Expressions;
using Tatargram.Models;

namespace Tatargram.Interfaces.Repositories;

public interface IPostRepository : IRepository<Post>
{
    Task<IEnumerable<Post>> GetPagedFeedList(int page = 1, int pageSize = 30);
}