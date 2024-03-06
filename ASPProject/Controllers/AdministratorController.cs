using ASPProject.Models;
using ASPProject.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ASPProject.Controllers
{

    public class AdministratorController : Controller
    {
        private readonly IAnimalRepository animalRepository;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public AdministratorController(IAnimalRepository animalRepository, IWebHostEnvironment hostingEnvironment)
        {
            this.animalRepository = animalRepository;
            _hostingEnvironment = hostingEnvironment;
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

        public IActionResult Delete(int id)
        {
            var animal = animalRepository.GetAllAnimals()
                .FirstOrDefault(a => a.AnimalId == id);
            if (animal == null)
            {
                return NotFound();
            }
            animalRepository.Delete(animal);
            return RedirectToAction("Index");
        }

        public IActionResult Add()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Add(string Name, int Age, string Description, int category, IFormFile Photo)
        {
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Description) || category <= 0 || Photo == null || Photo.Length <= 0 || Age > 120 || Age <= 0)
            {
                ViewBag.errors = "Please fill in all the required fields.";
                if (Age > 120 || Age <= 0)
                {
                    ViewBag.errors = "Age must be less than or equal to 120 and more then 0";
                }
            }

            else
            {
                var fileName = Guid.NewGuid().ToString() + Photo.FileName;
                var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "Photos", fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Photo.CopyTo(fileStream);
                }

                var animal = new Animal
                {
                    Name = Name,
                    Age = Age,
                    Description = Description,
                    CategoryId = category,
                    PictureName = fileName
                };

                animalRepository.AddAnimal(animal);

                return RedirectToAction("index");
            }
            return View();
        }
        public IActionResult Edit(int id)
        {
            var animal = animalRepository.GetAllAnimals().FirstOrDefault(a => a.AnimalId == id);
            return View(animal);
        }
        [HttpPost]
        public IActionResult Edit(string Name, int Age, string Description, int category, IFormFile Photo, int id)
        {
            var animal = animalRepository.GetAllAnimals().FirstOrDefault(a => a.AnimalId == id);
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Description) || category <= 0)
            {
                ViewBag.errors = "Please fill in all the required fields.";
            }

            else if (Age > 120 || Age <= 0)
            {
                ViewBag.errors = "Age must be less than or equal to 120 and more then 0";
            }
            else
            {

                if (Photo != null)
                {
                    var fileName = Guid.NewGuid().ToString() + Photo.FileName;
                    var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "Photos", fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        Photo.CopyTo(fileStream);
                    }
                    animal.PictureName = fileName;
                }
                animal.Name = Name;
                animal.Age = Age;
                animal.Description = Description;
                animal.CategoryId = category;

                animalRepository.Edit(animal);
                return RedirectToAction("index");
            }
            return View(animal);
        }


    }

}
