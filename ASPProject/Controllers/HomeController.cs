using Microsoft.AspNetCore.Mvc;
using ASPProject.Data;
using System.Linq;
using ASPProject.Repository;

namespace ASPProject.Controllers
{

    public class HomeController : Controller
    {
        private readonly IAnimalRepository animalRepository;

        public HomeController(IAnimalRepository animalRepository)
        {
            this.animalRepository = animalRepository;
        }

        public IActionResult Index()
        {
            var topAnimals = animalRepository.GetAllAnimals()
                .OrderByDescending(a => a.Comments.Count)
                .Take(2)
                .ToList();

            return View(topAnimals);
        }
    }
}
