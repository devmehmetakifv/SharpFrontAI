using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
			//GeminiAIClass myAI = new GeminiAIClass
			//{
			//    Model = "gemini-1.5-flash",
			//    FixedPrompt = $"",
			//};

			//await myAI.CodeAsync(new Prompt($"Create a header section with a logo and a navigation bar. Logo should be aligned to left and navigation bar should be aligned to right. Navigation bar should contain the followings: Home, Privacy Policy, Social Media, My Posts, Projects, Contact Me. {myAI.FixedPrompt}"));
		}
        public IActionResult Result()
        {
			Dictionary<string, string> areaPromptDictionary = null; // Create a dictionary to store the area prompts
			if (TempData["AreaPrompts"] != null) // Check if the TempData contains the area prompts
			{
				areaPromptDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(TempData["AreaPrompts"].ToString()); // Retrieve the area prompts from TempData
			}
            string mainPrompt = $"You are a website builder. Generate a website code according to the descriptions of the entered parts: ";
            foreach (var key in areaPromptDictionary.Keys)
            {
                mainPrompt = mainPrompt + $" for the {key} part: {areaPromptDictionary[key]}";
            }


            ViewBag.mainPrompt = mainPrompt;
            return View(areaPromptDictionary); // Return the view with the area prompts
		}

        [HttpPost]
        public IActionResult ReceivePrompts(IFormCollection form)
        {
            Dictionary<string, string> areaPromptDictionary = new Dictionary<string, string>(); // Create a dictionary to store the area prompts
            foreach (var key in form.Keys)
			{
				areaPromptDictionary.Add(key, form[key]); // Add the area prompt to the dictionary
			}
			TempData["AreaPrompts"] = JsonConvert.SerializeObject(areaPromptDictionary); // Store the dictionary in TempData

            
            
            return Json(new { redirectToUrl = Url.Action("Result") }); // Redirect to the Result action
        }

        public async Task<IActionResult> GenerateAIContent(string mainPrompt)
        {
            

            GeminiAIClass myAI = new GeminiAIClass
            {
                Model = "gemini-1.5-flash",
                FixedPrompt = mainPrompt,   //fixed prompt gerekmeyebilir
                SystemPrompt = "You are an AI that generates HTML code for website sections." // Set the system prompt here
            };


            List<PromptResult> promptResults = new List<PromptResult>();

            Prompt aiPrompt = new Prompt(mainPrompt);
            string result = await myAI.CodeAsync(aiPrompt);
            ViewBag.result = result;

            return View("GeminiResult", result);
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
