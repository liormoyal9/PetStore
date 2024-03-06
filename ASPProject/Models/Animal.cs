using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ASPProject.Models
{
    public class Animal
    {

        public int AnimalId { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Range(0, 120, ErrorMessage = "Age must be between 0 and 120.")]
        public int Age { get; set; }

        public string PictureName { get; set; }

        [MaxLength(120), Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }

        [Range(1, 3, ErrorMessage = "Category is required.")]
        public int CategoryId { get; set; }

        public virtual Category? Category { get; set; }

        public virtual ICollection<Comment>? Comments { get; set; }
    }
}
