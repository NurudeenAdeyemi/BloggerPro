using BloggerPro.Models;
using BloggerPro.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BloggerPro.Controllers
{
    public class PostController : Controller
    {
        public ActionResult Create(int communityId)
        {
            var community = CommunityRepository.GetById(communityId);
            return View(new Post { CommunityId = communityId });
        }

        [HttpPost]
        public ActionResult Create(Post post)
        {
            if (ModelState.IsValid)
            {
                var userId = HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier).Value;
                post.UserId = int.Parse(userId);
                post.CreatedAt = DateTime.Now;
                PostRepository.AddPost(post);
                return RedirectToAction("Details", "Community", new { id = post.CommunityId });
            }
            return View(post);
        }

        public ActionResult ViewPosts(int communityId)
        {
            var posts = PostRepository.GetPostsByCommunity(communityId);
            return View(posts);
        }
    }

}
