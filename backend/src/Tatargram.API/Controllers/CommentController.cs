using Microsoft.AspNetCore.Mvc;
using Tatargram.Comments.QueryModels;
using Tatargram.Comments.ViewModels;
using Tatargram.Interfaces.Services;

namespace Tatargram.Controllers;

[ApiController, Route("api/[controller]")]
public class CommentController : Controller
{
    private readonly ICommentService commentService;

    public CommentController(ICommentService commentService)
    {
        this.commentService = commentService;
    }

    #region GET

    [HttpGet("post/{postId}")]
    public async Task<ActionResult<IEnumerable<CommentBaseViewModel>>> GetPostComments(Guid postId)
    {
        return Ok(await commentService.GetAll<CommentBaseViewModel>(x => x.PostId == postId, x => x.User, true));
    }
    #endregion

    #region POST

    [HttpPost]
    public async Task<IActionResult> Create(CommentBaseQueryModel model)
    {
        await commentService.Create(model);
        return NoContent();
    }
    #endregion

    #region DELETE
    [HttpDelete("id")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await commentService.Delete(id);
        return NoContent();
    }

    #endregion

    #region PUT
    [HttpPut]
    public async Task<IActionResult> Update(UpdateCommentQueryModel model)
    {
        await commentService.Update(model);
        return NoContent();
    }
    #endregion
}