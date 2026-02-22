using CRUDDemo.Web.Models;


using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRUDDemo.Web.Controllers
{
    public class PersonController : Controller
    {
        private readonly ILogger<PersonController> _logger;
        private readonly AppDBContext _ctx;

        public PersonController(ILogger<PersonController> logger, AppDBContext ctx)
        {
            _logger = logger;
            _ctx = ctx;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var People = await _ctx
                      .People
                     .OrderBy(p => p.FirstName)
                     .ThenByDescending(p => p.LastName)
                      .ToListAsync();

                return View(People);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                TempData["error"] = "Error on saving Person record!!!";

                return View();

            }
        }
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Person _person)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                _ctx.People.Add(_person);//person is added to the context...........
                await _ctx.SaveChangesAsync();
                TempData["success"] = "Saved successfully!!!";
                return RedirectToAction("Add");
            }

            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                TempData["error"] = "Error on saving Person record!!!";

                return View();

            }
        }

        public async Task<IActionResult> Edit(int personID)
        {

            try
            {
                var People = await _ctx.People.AsNoTracking().SingleAsync(p => p.ID == personID);
                return View(People);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                TempData["error"] = "Error on saving Person record!!!";

                return View();

            }
        }
        public async Task<IActionResult> Delete(int personID)
        {

            try
            {
                var person = await _ctx.People.AsNoTracking().SingleAsync(p => p.ID == personID);
                _ctx.People.Remove(person);
                await _ctx.SaveChangesAsync();
                TempData["success"] = "Deleted successfully!!!";
            }

            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                TempData["error"] = "Error on saving Person record!!!";
            }
            return RedirectToAction(nameof(Add));
        }
    }
}

