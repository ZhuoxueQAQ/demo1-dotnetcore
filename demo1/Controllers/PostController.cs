using Microsoft.AspNetCore.Mvc;
using demo1.Model;
using System.Threading.Tasks;
using demo1.Service;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;

namespace demo1.Controllers
{
    /// <summary>
    /// 用户管理
    /// </summary>
    [Route("[controller]/[action]")]
    [ApiController]
    [Produces("application/json")]

    public class PostController : ControllerBase
    {
        public PostService postService = new PostService();

        ///<summary>
        ///新增Post
        ///</summary>
        ///<param name="post"></param>
        ///<returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddPost(Post post)
        {
            bool success = await postService.Add(post);
            // 如果成功，返回status201
            return CreatedAtAction(nameof(GetPostByCreatedDate), new { createdDate = post.createdDate }, post);
        }
        ///<summary>
        ///删除user
        ///</summary>
        ///<param name="createdDate"></param>
        ///<returns></returns>
        [HttpDelete("{createdDate}")]
        public async Task<IActionResult> DeletePost(string createdDate)
        {
            bool success = await postService.DeleteByPrimaryKey(createdDate);
            if(!success)
            {
                return NotFound();
            }
            // 成功删除，返回200
            return Ok(success);
        }

        ///<summary>
        ///修改User
        ///</summary>
        ///<param name="post"></param>
        ///<returns></returns>
        [HttpPut("{createdDate}")]
        public async Task<IActionResult> UpdatePost(Post post)
        {
            bool success = await postService.Update(post);
            return Ok(success);
        }

        /// <summary>
        /// 根据创建日期获取Post
        /// </summary>
        /// <param name="createdDate"></param>
        /// <returns></returns>
        [HttpGet("{createdDate}")]
        public async Task<IActionResult> GetPostByCreatedDate(string createdDate)
        {
            Post post = await postService.QueryByPrimaryKey(createdDate);
            if(post == null)
            {
                // status 404
                return NotFound();
            }
            // status 200
            return Ok(post);
        }

        /// <summary>
        /// 获取全部Post
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllPost()
        {
            List<Post> getAll = await postService.QueryAll();
            // 逆序，先创建的排前面
            getAll.Reverse();
            return Ok(getAll);
        }
    }
}
