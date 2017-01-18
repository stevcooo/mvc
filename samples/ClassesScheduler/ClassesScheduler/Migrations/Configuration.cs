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

            SemestarSchedule ss;
            Array studyfields = Enum.GetValues(typeof(Enums.StudyField));
            int calendarYear = 2017;
            foreach(var sf in studyfields)
            {
                for(int i = 1; i < 5; i++)
                {
                    //Winter
                    ss = new SemestarSchedule();
                    ss.StudyField = (Enums.StudyField)sf;
                    ss.SemesterType = Enums.Semester.Winter;
                    ss.Year = calendarYear;
                    ss.YearOfStudy = i.ToString();


                    if (!context.SemestarSchedules.Any(t => t.StudyField == ss.StudyField && t.SemesterType == ss.SemesterType && t.Year == ss.Year && t.YearOfStudy == ss.YearOfStudy))
                    {
                        //for everyday
                        //classes schedule from 8:00h to 18:00h 5 classes            

                        context.SemestarSchedules.Add(ss);
                    }                    

                    //Summer
                    ss = new SemestarSchedule();
                    ss.StudyField = (Enums.StudyField)sf;
                    ss.SemesterType = Enums.Semester.Summer;
                    ss.Year = calendarYear;
                    ss.YearOfStudy = i.ToString();
                    if (!context.SemestarSchedules.Any(t => t.StudyField == ss.StudyField && t.SemesterType == ss.SemesterType && t.Year == ss.Year && t.YearOfStudy == ss.YearOfStudy))
                    {
                        context.SemestarSchedules.Add(ss);
                    }
                }
            }

                       

            ClassSchedule cs = new ClassSchedule();
            cs.ClassromCode = "SomeRandomCode";
            cs.CourseId = context.Courses.FirstOrDefault().Id;
        }
    }
}
