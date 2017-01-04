using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClassesScheduler.Models
{
    public class Proffesor
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Име")]
        public string FirstName { get; set; }

        [Display(Name = "Презиме")]
        public string LastName { get; set; }

        public virtual ICollection<Course> TeachingCourses { get; set; }
    }
}
