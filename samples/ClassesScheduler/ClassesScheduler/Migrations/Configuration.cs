namespace ClassesScheduler.Migrations
{
    using Models;
    using System;
    using System.Collections.Generic;    
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            //if (System.Diagnostics.Debugger.IsAttached == false)
            //{
            //    System.Diagnostics.Debugger.Launch();
            //}

            context.Proffesors.AddOrUpdate(
                p=>p.FirstName,
                new Proffesor { FirstName = "Nathan", LastName = "Marshall" },
                new Proffesor { FirstName = "Homer", LastName = "Munoz" },
                new Proffesor { FirstName = "Melissa", LastName = "Lindsey" },
                new Proffesor { FirstName = "Adrian", LastName = "Foster" },
                new Proffesor { FirstName = "Joyce", LastName = "Hoffman" },
                new Proffesor { FirstName = "Priscilla", LastName = "Maxwell" },
                new Proffesor { FirstName = "Lillian", LastName = "Riley" },
                new Proffesor { FirstName = "Terry", LastName = "Mcgee" },
                new Proffesor { FirstName = "Jacqueline", LastName = "Poole" },
                new Proffesor { FirstName = "William", LastName = "Sanchez" },
                new Proffesor { FirstName = "Dennis", LastName = "Armstrong" },
                new Proffesor { FirstName = "Sally", LastName = "Hopkins" },
                new Proffesor { FirstName = "Ian", LastName = "Crawford" },
                new Proffesor { FirstName = "Rogelio", LastName = "Floyd" },
                new Proffesor { FirstName = "Adrienne", LastName = "Welch" },
                new Proffesor { FirstName = "Laura", LastName = "Rodgers" },
                new Proffesor { FirstName = "Darren", LastName = "Bush" },
                new Proffesor { FirstName = "Leona", LastName = "Mcguire" },
                new Proffesor { FirstName = "Monique", LastName = "Roberson" }
                );

            context.SaveChanges();

            int numberOfProffesors = context.Proffesors.Count();

            Random rnd = new Random();
            Course course;
            int rndProfId;
            bool isMandatorySubject;
            Array semestars = Enum.GetValues(typeof(Enums.Semester));

            Enums.Semester semsterType;

            var courses = new List<Course>();

            //Courses
            for(int i = 0; i < 80; i++)
            {
                rndProfId = rnd.Next(1, numberOfProffesors);
                isMandatorySubject = (rnd.Next(2) == 0);
                semsterType = (Enums.Semester)semestars.GetValue(rnd.Next(semestars.Length));
                course = new Course { Name = String.Format("Course {0}{1}", semsterType, i), Description = "Some description for course.", IsMandatorySubject = isMandatorySubject, NumberOfCredits = rnd.Next(5, 7), SemesterType = semsterType, ProffesorId = context.Proffesors.FirstOrDefault(t => t.Id == rndProfId).Id };
                courses.Add(course);
            }
            
            context.Courses.AddOrUpdate(
                p => p.Name,
                courses.ToArray()                
                );

            context.SaveChanges();

            Array studyfields = Enum.GetValues(typeof(Enums.StudyField));
            foreach(var sf in studyfields)
            {
                for(int i = 1; i < 5; i++)
                {
                    GenerateSemesterSchedule(context, Enums.Semester.Winter, (Enums.StudyField)sf, i);
                    GenerateSemesterSchedule(context, Enums.Semester.Summer, (Enums.StudyField)sf, i);
                }
            }
        }

        private void GenerateSemesterSchedule(ApplicationDbContext context, Enums.Semester semesterType, Enums.StudyField sf, int yearOfStudy)
        {
            SemestarSchedule ss;
            Array studyfields = Enum.GetValues(typeof(Enums.StudyField));
            int calendarYear = 2017;

            ss = new SemestarSchedule();
            ss.StudyField = (Enums.StudyField)sf;
            ss.SemesterType = semesterType;
            ss.Year = calendarYear;
            ss.YearOfStudy = yearOfStudy.ToString();


            if (!context.SemestarSchedules.Any(t => t.StudyField == ss.StudyField && t.SemesterType == ss.SemesterType && t.Year == ss.Year && t.YearOfStudy == ss.YearOfStudy))
            {
                var coursesForThisSchedule = context.Courses.Where(t => t.SemesterType == ss.SemesterType).ToList();

                context.SemestarSchedules.Add(ss);
                context.SaveChanges();

                var recordedSSId = context.SemestarSchedules.FirstOrDefault(t => t.StudyField == ss.StudyField && t.SemesterType == ss.SemesterType && t.Year == ss.Year && t.YearOfStudy == ss.YearOfStudy).Id;

                GenerateClassSchedules(context, recordedSSId, coursesForThisSchedule, semesterType);
            }
        }

        private void GenerateClassSchedules(ApplicationDbContext context, int semestarScheduleId, List<Course> courses, Enums.Semester semesterType)
        {
            Array weekDays = Enum.GetValues(typeof(Enums.WeekDay));
            ClassSchedule cs;
            int rndCourseId;
            var rnd = new Random();

            foreach (var day in weekDays)
            {
                List<ClassSchedule> classesForThisDay = new List<ClassSchedule>();
                //classes schedule from 8:00h to 18:00h 5 classes            
                for (int j = 0; j < 5; j++)
                {
                    var rndHour = rnd.Next(8, 19);

                    while (classesForThisDay.Any(t => t.FromHour == rndHour))
                    {
                        rndHour = rnd.Next(8, 19);
                    }

                    cs = new ClassSchedule();
                    cs.SemestarScheduleId = semestarScheduleId;
                    cs.WeekDay = (Enums.WeekDay)day;
                    cs.ClassromCode = rnd.Next(100, 350).ToString();
                    cs.FromHour = rndHour;
                    cs.ToHour = rndHour + 1;
                    rndCourseId = rnd.Next(courses.Count);
                    cs.CourseId = courses.FirstOrDefault(t => t.Id >= rndCourseId && t.SemesterType == semesterType).Id;

                    context.ClassSchedules.Add(cs);
                }
            }
        }
    }
}
