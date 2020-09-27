using System.Threading.Tasks;
using AnswersApp.Data;
using AnswersApp.Models;
using AnswersApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace AnswersApp.Controllers
{
    public class QuestionsController : Controller
    {
        private QuestionsService Service { get; }

        public QuestionsController(AnswersContext context)
        {
            Service = new QuestionsService(context);
        }
        
        public IActionResult Index()
        {
            return View(Service.IndexPage());
        }
        
        [HttpGet]
        public IActionResult Add(int? answerId)
        {
            var model = Service.AddPage(answerId);
            return PartialView(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Question question)
        {
            await Service.Add(question);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            return PartialView(Service.EditPage(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Question question)
        {
            if (!ModelState.IsValid) return RedirectToAction(nameof(Edit));
            await Service.Edit(question);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            await Service.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}