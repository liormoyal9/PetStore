using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using ASPProject.Data;
using ASPProject.Models;

namespace ASPProject.Repository
{
    public class AnimalRepository : IAnimalRepository
    {
        private readonly PetShopContext _context;

        public AnimalRepository(PetShopContext context)
        {
            _context = context;
        }

        public void AddAnimal(Animal animal)
        {

            _context.Animals.Add(animal);
            _context.SaveChanges();

        }

        public void Edit(Animal animal)
        {
            var existingAnimal = _context.Animals.FirstOrDefault(a => a.AnimalId == animal.AnimalId);
            existingAnimal.Name = animal.Name;
            existingAnimal.Description = animal.Description;
            existingAnimal.Age = animal.Age;
            existingAnimal.PictureName = animal.PictureName;
            existingAnimal.CategoryId = animal.CategoryId;
            
            _context.SaveChanges();
        }

        public void Delete(Animal animal)
        {
            var animalToDelete = _context.Animals.FirstOrDefault(a => a.AnimalId == animal.AnimalId);

            if (animalToDelete != null)
            {
                _context.Animals.Remove(animalToDelete);
                _context.SaveChanges();
            }

        }

        public IEnumerable<Animal> GetAllAnimals()
        {
            return _context.Animals.ToList();
        }
        public void AddComment(Comment comment)
        {
            _context.Comments.Add(comment);
            _context.SaveChanges();
        }
        public IEnumerable<Comment> GetAllComments()
        {
            return _context.Comments.ToList();
        }
        public IEnumerable<Category> GetAllCategories()
        {
            return _context.Categories.ToList();
        }
    }
}
