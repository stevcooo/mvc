using ClassesScheduler.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClassesScheduler.Models
{
    public class ClassSchedule
    {
        [Key]
        public int Id { get; set; }


        [Required]
        [DisplayName("Просторија")]
        public string ClassromCode { get; set; }

        [Required]
        [DisplayName("Од саат")]
        [Range(8,20)]
        public int FromHour { get; set; }

        [Required]
        [DisplayName("До Саат")]
        [Range(8, 20)]
        public int ToHour { get; set; }

        [Required]
        [Display(Name = "Ден")]
        public WeekDay WeekDay { get; set; }

        [DisplayName("Предмет")]
        public int CourseId { get; set; }

        [ForeignKey("CourseId")]
        public virtual Course Course { get; set; }

        [DisplayName("Распоред")]
        public int SemestarScheduleId { get; set; }

        [ForeignKey("SemestarScheduleId")]
        public virtual SemestarSchedule SemestarSchedule { get; set; }
    }
}
