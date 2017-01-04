using System.ComponentModel.DataAnnotations;

namespace ClassesScheduler.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Име")]
        public string Name { get; set; }

        [Display(Name = "Опис")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Задолжителен предмет")]
        public bool IsMandatorySubject { get; set; }

        [Required]
        [Display(Name = "ЕКТС кредити")]
        public int NumberOfCredits { get; set; }
    }
}
