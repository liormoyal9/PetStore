using ASPProject.Data;
using ASPProject.Models;
using ASPProject.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASPProject.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IAnimalRepository animalRepository;

        public CatalogController(IAnimalRepository animalRepository)
        {
            this.animalRepository = animalRepository;
        }

        public IActionResult Index(string category = "All")
        {
            IEnumerable<Animal> animalsQuery = animalRepository.GetAllAnimals();
            if (category != "All")
            {
                animalsQuery = animalsQuery.Where(a => a.Category.Name == category);
            }

            if (!string.IsNullOrEmpty(category))
            {
                ViewData["SelectedCategory"] = category;
            }
            else
            {
                ViewData["SelectedCategory"] = "All";
            }

            return View(animalsQuery);
        }

        public IActionResult Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animal = animalRepository.GetAllAnimals()
                .FirstOrDefault(a => a.AnimalId == id);

            if (animal == null)
            {
                return NotFound();
            }

            return View(animal);
        }

        public IActionResult AddComment(int animalId, string commentText)
        {
            if (animalId == null)
            {
                return NotFound();
            }

            var animal = animalRepository.GetAllAnimals()
                .FirstOrDefault(a => a.AnimalId == animalId);

            if (animal == null)
            {
                return NotFound();
            }
            var comment = new Comment
            {
                AnimalId = animalId, Text = commentText
            };
            animalRepository.AddComment(comment);
            return RedirectToAction("Details", new {id=animalId});
        }

    }
}
