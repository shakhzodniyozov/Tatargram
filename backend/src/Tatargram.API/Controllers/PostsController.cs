using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tatargram.Interfaces.Services;
using Tatargram.Posts.QueryModels;

namespace Tatargram.Contollers;

[Authorize, ApiController, Route("api/[controller]")]
public class PostsController : Controller
{
    private readonly IPostService postService;

    public PostsController(IPostService postService)
    {
        this.postService = postService;
    }

    #region GET
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PostViewModel>>> GetPagedFeedList(int page = 1, int pageSize = 30)
    {
        var posts = await postService.GetPagedFeedList(page, pageSize);
        return Ok(posts);
    }

    #endregion

    #region POST
    [HttpPost]
    public async Task<IActionResult> Create(CreatePostQueryModel model)
    {
        await postService.Create(model);

        return NoContent();
    }

    [HttpPost("like/{postId}")]
    public async Task<IActionResult> LikeThePost(Guid postId)
    {
        await postService.LikeThePost(postId);
        return NoContent();
    }

    #endregion

    #region PUT
    [HttpPut]
    public async Task<IActionResult> Update(UpdatePostQueryModel model)
    {
        await postService.Update(model);
        return NoContent();
    }

    #endregion

    #region DELETE

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await postService.Delete(id);

        return NoContent();
    }
    #endregion
}