using System.Diagnostics;
using System.Threading.Tasks;
using AnswersApp.Data;
using Microsoft.AspNetCore.Mvc;
using AnswersApp.Models;
using AnswersApp.Services;

namespace AnswersApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly AnswersService _service;
        
        public HomeController(AnswersContext context)
        {
            _service = new AnswersService(context);
        }

        public IActionResult Index(string question)
        {
            return View(_service.AnswerForQuestion(question));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}