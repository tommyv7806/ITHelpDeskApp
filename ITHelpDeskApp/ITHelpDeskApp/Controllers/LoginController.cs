using ITHelpDeskApp.Models.Repository;
using ITHelpDeskApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace ITHelpDeskApp.Controllers
{
    public class LoginController : Controller
    {
        private Repository<User> userData { get; set; }

        public LoginController(HelpDeskContext ctx) => userData = new Repository<User>(ctx);

        [HttpGet]
        public IActionResult LoginPage()
        {
            ViewData["ErrorMessage"] = TempData["ErrorMessage"];
            TempData.Remove("ErrorMessage");
            return View();
        }

        public RedirectToActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public RedirectToActionResult ValidateLogin(LoginUser loginUser)
        {
            var userWithMatchingUsername = userData.List(new QueryOptions<User>
            { Where = u => u.Username.ToLower().Equals(loginUser.Username.ToLower()) }).FirstOrDefault();

            if (userWithMatchingUsername == null)
            {
                // Add error message to tempdata that no matching username was found
                TempData["ErrorMessage"] = "No matching username found";
                return RedirectToAction("LoginPage");
            }

            if (!userWithMatchingUsername.Password.Equals(loginUser.Password))
            {
                // Add error to tempdata that password is incorrect
                TempData["ErrorMessage"] = "Incorrect password";
                return RedirectToAction("LoginPage");
            }

            // Set the logged in username in the session for checking which user is currently logged in 
            HttpContext.Session.SetString("LoggedInUsername", userWithMatchingUsername.Username);
            return RedirectToAction("Index", "Home");
        }
    }
}
