using BloggerPro.Repositories;
using Microsoft.AspNetCore.Mvc;

public class CommunityController : Controller
{
    public ActionResult Index()
    {
        // Assuming we have the user ID in session
        var userId = HttpContext?.User?.Identity?.Name;

        // Get communities that the user has access to
        var communities = CommunityRepository.GetCommunitiesForUser(int.Parse(userId));

        return View(communities);
    }

    public ActionResult Join(int id)
    {
        var userId = HttpContext?.User?.Identity?.Name;
        CommunityRepository.JoinCommunity(int.Parse(userId), id);
        return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
        var community = CommunityRepository.GetById(id);
        return View(community);
    }

    public ActionResult AllCommunities()
    {
        var communities = CommunityRepository.GetAll();
        return View(communities);
    }

}
