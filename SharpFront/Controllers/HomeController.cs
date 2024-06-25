using Microsoft.AspNetCore.Mvc;
using SharpFront.Models;
using System.Diagnostics;

namespace SharpFront.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Result()
        {
            GeminiAIClass myAI = new GeminiAIClass
            {
                Model = "gemini-1.5-flash",
                FixedPrompt = $"",
            };

            await myAI.CodeAsync(new Prompt($"Create a header section with a logo and a navigation bar. Logo should be aligned to left and navigation bar should be aligned to right. Navigation bar should contain the followings: Home, Privacy Policy, Social Media, My Posts, Projects, Contact Me. {myAI.FixedPrompt}"));




            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
