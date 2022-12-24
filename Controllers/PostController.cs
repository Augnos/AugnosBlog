using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AugnosBlog.Models;

namespace AugnosBlog.Controllers;

public class PostController : Controller
{
    // ********** Fields **********
    private readonly ILogger<PostController> _logger;
    private MyContext _context;


    // ********** Constructor **********
    public PostController(ILogger<PostController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }


    // ********** Routes & Methods **********
    // ********** Index (Show All) **********
    [HttpGet("")]
    public IActionResult Index()
    {
        List<Post> AllPosts = _context.Posts.ToList();

        return View("Index", AllPosts);
    }

    // ********** New **********
    [HttpGet("/posts/new")]
    public IActionResult NewPost()
    {
        return View("NewPost");
    }

    // ********** Create - POST **********
    [HttpPost("posts/create")]
    public IActionResult CreatePost(Post newPost)
    {
        if (ModelState.IsValid)
        {
            _context.Add(newPost);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        return NewPost();
    }

    // ********** Show **********
    [HttpGet("/posts/{id}")]
    public IActionResult ShowPost(int id)
    {
        Post ViewPost = _context.Posts.First(post => post.PostId == id);
        if (ViewPost == null)
        {
            return Index();
        }
        return View("ShowPost", ViewPost);
    }

    // ********** Edit **********
    [HttpGet("/posts/{id}/edit")]
    public IActionResult EditPost(int id)
    {
        Post ViewPost = _context.Posts.First(post => post.PostId == id);
        if (ViewPost == null)
        {
            return Index();
        }
        return View("EditPost", ViewPost);
    }

    // ********** Update - POST **********
    [HttpPost("/posts/{id}/update")]
    public IActionResult UpdatePost(Post newPost, int id)
    {
        if (ModelState.IsValid)
        {
            Post OldPost = _context.Posts.First(post => post.PostId == id);

            // Creates new Post if current post doesn't actually exist.
            if (OldPost == null)
            {
                return CreatePost(newPost);
            }
            // Update all OldPost fields with newPost values
            // OldPost.Name = newPost.Name;
            // OldPost.Description = newPost.Description;
            // OldPost.Price = newPost.Price;
            // OldPost.UpdatedAt = DateTime.Now;
            _context.SaveChanges();
            return ShowPost(id);
        }
        return EditPost(id);
    }

    // ********** Destroy - POST **********
    [HttpPost("/posts/{id}/destroy")]
    public IActionResult DestroyPost(int id)
    {
        Post onePost = _context.Posts.Single(d => d.PostId == id);
        _context.Posts.Remove(onePost);
        _context.SaveChanges();
        return Index();
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

// public class SessionCheckAttribute : ActionFilterAttribute
// {
//     public override void OnActionExecuting(ActionExecutingContext context)
//     {
//         // Find the session, but remember it may be null so we need int?
//         int? userId = context.HttpContext.Session.GetInt32("UserId");
//         // Check to see if we got back null
//         if (userId == null)
//         {
//             // Redirect to the Index page if there was nothing in session
//             // "Home" here is referring to "HomeController", you can use any controller that is appropriate here
//             context.Result = new RedirectToActionResult("Index", "Home", null);
//         }
//     }
// }
