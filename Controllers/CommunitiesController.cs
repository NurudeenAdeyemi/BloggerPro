using BloggerPro.Models;
using BloggerPro.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

public class CommunityController : Controller
{
    public ActionResult Index()
    {
        // Assuming we have the user ID in session
        var userId = HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier).Value;

        // Get communities that the user has access to
        var communities = CommunityRepository.GetCommunitiesForUser(int.Parse(userId));

        return View(communities);
    }

    [HttpGet]
    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Create(Community community)
    {
        if (ModelState.IsValid)
        {
            CommunityRepository.Add(community);
            return RedirectToAction("Index");
        }

        return View(community);
    }
    public ActionResult Join(int id)
    {
        var userId = HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier).Value;
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
