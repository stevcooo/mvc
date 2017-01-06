using ClassesScheduler.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassesScheduler.Models
{
    public class SemestarSchedule
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Година")]
        [Range(2016, 2030)]
        public int Year { get; set; }

        [Required]
        [Display(Name = "Семестар")]
        public Semester SemesterType { get; set; }

        [Required]
        [Display(Name = "Година на студии")]
        [Range(1,4,ErrorMessage ="Година на студии мора да биде помеѓу 1 и 4")]
        public string YearOfStudy { get; set; }

        [Required]
        [Display(Name = "Насока")]
        public StudyField StudyField { get; set; }

        public virtual ICollection<ClassSchedule> ClassSchedules { get; set; }
    }
}
