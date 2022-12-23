using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AugnosBlog.Models;

namespace AugnosBlog.Controllers;

public class BlogController : Controller
{
    // ********** Fields **********
    private readonly ILogger<BlogController> _logger;
    private MyContext _context;


    // ********** Constructor **********
    public BlogController(ILogger<BlogController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }


    // ********** Routes & Methods **********
    // ********** Index (Show All) **********
    [HttpGet("")]
    public IActionResult Index()
    {
        List<Blog> AllBlogs = _context.Blogs.ToList();

        return View("Index", AllBlogs);
    }

    // ********** New **********
    [HttpGet("/blogs/new")]
    public IActionResult NewBlog()
    {
        return View("NewBlog");
    }

    // ********** Create - POST **********
    [HttpPost("blogs/create")]
    public IActionResult CreateBlog(Blog newBlog)
    {
        if (ModelState.IsValid)
        {
            _context.Add(newBlog);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        return NewBlog();
    }

    // ********** Show **********
    [HttpGet("/blogs/{id}")]
    public IActionResult ShowBlog(int id)
    {
        Blog ViewBlog = _context.Blogs.First(blog => blog.BlogId == id);
        if (ViewBlog == null)
        {
            return Index();
        }
        return View("ShowBlog", ViewBlog);
    }

    // ********** Edit **********
    [HttpGet("/blogs/{id}/edit")]
    public IActionResult EditBlog(int id)
    {
        Blog ViewBlog = _context.Blogs.First(blog => blog.BlogId == id);
        if (ViewBlog == null)
        {
            return Index();
        }
        return View("EditBlog", ViewBlog);
    }

    // ********** Update - POST **********
    [HttpPost("/blogs/{id}/update")]
    public IActionResult UpdateBlog(Blog newBlog, int id)
    {
        if (ModelState.IsValid)
        {
            Blog OldBlog = _context.Blogs.First(blog => blog.BlogId == id);

            // Creates new Blog if current blog doesn't actually exist.
            if (OldBlog == null)
            {
                return CreateBlog(newBlog);
            }
            // Update all OldBlog fields with newBlog values
            // OldBlog.Name = newBlog.Name;
            // OldBlog.Description = newBlog.Description;
            // OldBlog.Price = newBlog.Price;
            // OldBlog.UpdatedAt = DateTime.Now;
            _context.SaveChanges();
            return ShowBlog(id);
        }
        return EditBlog(id);
    }

    // ********** Destroy - POST **********
    [HttpPost("/blogs/{id}/destroy")]
    public IActionResult DestroyBlog(int id)
    {
        Blog oneBlog = _context.Blogs.Single(d => d.BlogId == id);
        _context.Blogs.Remove(oneBlog);
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
