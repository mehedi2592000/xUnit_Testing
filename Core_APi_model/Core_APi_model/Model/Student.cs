using System.ComponentModel.DataAnnotations;

namespace Core_APi_model.Model
{
    public class Student
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Name must be between 5 and 20 characters.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }
        public int Age { get; set; }
    }
}
