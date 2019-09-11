namespace SoftUniBabies.Controllers
{
    using Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;

    public class BabyController : Controller
    {
        private readonly BabyDbContext dbContext;

        public BabyController(BabyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            var babies = dbContext.Babies.ToList();
            return View(babies);
        }

        [HttpGet]
        [Route("/create")]
        public IActionResult Create()
        {
            //TODO
            return View();
        }

        [HttpPost]
        [Route("/create")]
        public IActionResult Create(Baby baby)
        {
            dbContext.Babies.Add(baby);
            dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("/edit/{id}")]
        public IActionResult Edit(int id)
        {
            var proj = dbContext
                 .Babies
                 .Where(p => p.Id == id)
                 .FirstOrDefault();

            return View(proj);
        }

        [HttpPost]
        [Route("/edit/{id}")]
        public IActionResult Edit(Baby baby)
        {
            dbContext.Babies.Update(baby);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("/delete/{id}")]
        public IActionResult Delete(int id)
        {
            var proj = dbContext
                 .Babies
                 .Where(p => p.Id == id)
                 .FirstOrDefault();

            return View(proj);
        }

        [HttpPost]
        [Route("/delete/{id}")]
        public IActionResult Delete(int id, Baby baby)
        {
            var proj = dbContext
                .Babies
                .Where(p => p.Id == id)
                .FirstOrDefault();

            dbContext.Remove(proj);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
