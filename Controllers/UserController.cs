using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AugnosBlog.Models;

namespace AugnosBlog.Controllers;

public class UserController : Controller
{
    // ********** Fields **********
    private readonly ILogger<UserController> _logger;
    private MyContext _context;


    // ********** Constructor **********
    public UserController(ILogger<UserController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }


    // ********** Routes & Methods **********
    // ********** Index (Show All) **********
    [HttpGet("")]
    public IActionResult Index()
    {
        List<User> AllUsers = _context.Users.ToList();

        return View("Index", AllUsers);
    }

    // ********** New **********
    [HttpGet("/users/new")]
    public IActionResult NewUser()
    {
        return View("NewUser");
    }

    // ********** Create - POST **********
    [HttpPost("users/create")]
    public IActionResult CreateUser(User newUser)
    {
        if (ModelState.IsValid)
        {
            _context.Add(newUser);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        return NewUser();
    }

    // ********** Show **********
    [HttpGet("/users/{id}")]
    public IActionResult ShowUser(int id)
    {
        User ViewUser = _context.Users.First(user => user.UserId == id);
        if (ViewUser == null)
        {
            return Index();
        }
        return View("ShowUser", ViewUser);
    }

    // ********** Edit **********
    [HttpGet("/users/{id}/edit")]
    public IActionResult EditUser(int id)
    {
        User ViewUser = _context.Users.First(user => user.UserId == id);
        if (ViewUser == null)
        {
            return Index();
        }
        return View("EditUser", ViewUser);
    }

    // ********** Update - POST **********
    [HttpPost("/users/{id}/update")]
    public IActionResult UpdateUser(User newUser, int id)
    {
        if (ModelState.IsValid)
        {
            User OldUser = _context.Users.First(user => user.UserId == id);

            // Creates new User if current user doesn't actually exist.
            if (OldUser == null)
            {
                return CreateUser(newUser);
            }
            // Update all OldUser fields with newUser values
            // OldUser.Name = newUser.Name;
            // OldUser.Description = newUser.Description;
            // OldUser.Price = newUser.Price;
            // OldUser.UpdatedAt = DateTime.Now;
            _context.SaveChanges();
            return ShowUser(id);
        }
        return EditUser(id);
    }

    // ********** Destroy - POST **********
    [HttpPost("/users/{id}/destroy")]
    public IActionResult DestroyUser(int id)
    {
        User oneUser = _context.Users.Single(d => d.UserId == id);
        _context.Users.Remove(oneUser);
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
