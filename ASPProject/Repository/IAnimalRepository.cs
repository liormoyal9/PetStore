using ASPProject.Models;

namespace ASPProject.Repository
{
    public interface IAnimalRepository
    {
        void AddAnimal(Animal animal);
        void Edit(Animal animal);
        void Delete(Animal animal);
        IEnumerable<Animal> GetAllAnimals();
        void AddComment(Comment comment);
        IEnumerable<Comment> GetAllComments();
        IEnumerable<Category> GetAllCategories();

    }
}
