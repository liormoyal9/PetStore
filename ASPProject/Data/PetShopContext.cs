using ASPProject.Models;
using Microsoft.EntityFrameworkCore;

namespace ASPProject.Data
{
    public class PetShopContext : DbContext
    {
        public PetShopContext(DbContextOptions<PetShopContext> options) : base(options) { }

        public DbSet<Animal> Animals { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Animal>().HasData(
                new Animal
                {
                    AnimalId = 1,
                    Name = "Birdy",
                    Age = 2,
                    PictureName = "birdy.jpg",
                    Description = "a beautiful special bird that is in love with crackers",
                    CategoryId = 1,

        },

                new Animal { AnimalId = 2, Name = "Mitzi", Age = 1, PictureName = "mitzi.jpg", Description = "a cute dog that loves toys", CategoryId = 2 }
                );
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "Birds" },
                new Category { CategoryId = 2, Name = "Mammals" },
                new Category { CategoryId = 3, Name = "Fishes" }
                );
            modelBuilder.Entity<Comment>().HasData(new Comment { CommentId = 1, AnimalId = 1, Text = "birdy is the best" });
            modelBuilder.Entity<Comment>().HasData(new Comment { CommentId = 2, AnimalId = 2, Text = "mitzy always barks bet he is lovley" });

        }

    }

}
