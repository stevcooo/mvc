using ClassesScheduler.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [Required]
        [Display(Name = "Семестар")]
        public Semester SemesterType { get; set; }

        [DisplayName("Тип")]
        public int ProffesorId { get; set; }

        [ForeignKey("ProffesorId")]
        public virtual Proffesor Proffesor { get; set; }
    }
}
