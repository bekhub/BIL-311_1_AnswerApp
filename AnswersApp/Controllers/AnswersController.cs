using System.Threading.Tasks;
using AnswersApp.Data;
using AnswersApp.Models;
using AnswersApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace AnswersApp.Controllers
{
    public class AnswersController : Controller
    {
        private readonly AnswersService _service;

        public AnswersController(AnswersContext answersContext)
        {
            _service = new AnswersService(answersContext);
        }
        
        public IActionResult Index()
        {
            return View(_service.List());
        }

        [HttpGet]
        public IActionResult Add()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Answer answer)
        {
            if (!ModelState.IsValid) return RedirectToAction(nameof(Index));
            await _service.Add(answer);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            return View(await _service.ById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Answer answer)
        {
            if (!ModelState.IsValid) return RedirectToAction(nameof(Edit));
            await _service.Edit(answer);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            await _service.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}