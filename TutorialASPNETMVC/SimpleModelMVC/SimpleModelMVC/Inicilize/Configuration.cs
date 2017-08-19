using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using SimpleModelMVC.Models;

namespace SimpleModelMVC.Inicilize
{
    public class Configuration:DropCreateDatabaseAlways<UniversityContext>
    {
        protected override void Seed(UniversityContext context)
        {
            var students = new List<Student>
           {
               new Student {FirstMidName="Лепотенко",LastName="Александр",EnrollmentDate=DateTime.Parse("2002-09-01") },
               new Student {FirstMidName="Лепотенко",LastName="Павел",EnrollmentDate=DateTime.Parse("2012-01-01") },
               new Student {FirstMidName="Яцинович",LastName="Виктор",EnrollmentDate=DateTime.Parse("2002-10-11") },
               new Student {FirstMidName="Виткевич",LastName="Иван",EnrollmentDate=DateTime.Parse("2013-01-01") },
               new Student {FirstMidName="Потребко",LastName="Денис",EnrollmentDate=DateTime.Parse("2011-09-01") },
               new Student {FirstMidName="Потребко",LastName="Ирина",EnrollmentDate=DateTime.Parse("2012-01-01") },
               new Student {FirstMidName="Мигуро",LastName="Алексей",EnrollmentDate=DateTime.Parse("2010-10-11") },
               new Student {FirstMidName="Рак",LastName="Виктор",EnrollmentDate=DateTime.Parse("2014-01-01") },
               new Student {FirstMidName="Потребко",LastName="Иван",EnrollmentDate=DateTime.Parse("2002-09-01") },
               new Student {FirstMidName="Корсак",LastName="Анастасия",EnrollmentDate=DateTime.Parse("2012-01-01") },
               new Student {FirstMidName="Гайкевич",LastName="Светлана",EnrollmentDate=DateTime.Parse("2002-10-11") },
               new Student {FirstMidName="Юрченко",LastName="Глеб",EnrollmentDate=DateTime.Parse("2013-01-01") },



           };
            students.ForEach(s => context.Students.Add(s));
            context.SaveChanges();

            var courses = new List<Course>()
            {
                new Course { CourseID=1050,Title="Химия",Credits=3, },
                new Course { CourseID=4022,Title="Микроэлектроника",Credits=3, },
                new Course { CourseID=4041,Title="Электронные приборы",Credits=3, },
                new Course { CourseID=1045,Title="Теория вероятности",Credits=3, },
                new Course { CourseID=3141,Title="Тригонометрия",Credits=4, },
                new Course { CourseID=2021,Title="Музыка",Credits=3, },
                new Course { CourseID=2042,Title="Физика",Credits=4, },
            };
            courses.ForEach(s => context.Courses.Add(s));

            var Enrollments = new List<Enrollment>()
            {
            new Enrollment{StudentID=1,CourseID=1050,Grade=Grade.A},
            new Enrollment{StudentID=1,CourseID=4022,Grade=Grade.C},
            new Enrollment{StudentID=1,CourseID=4041,Grade=Grade.B},
            new Enrollment{StudentID=2,CourseID=1045,Grade=Grade.B},
            new Enrollment{StudentID=2,CourseID=3141,Grade=Grade.F},
            new Enrollment{StudentID=2,CourseID=2021,Grade=Grade.F},
            new Enrollment{StudentID=3,CourseID=1050},
            new Enrollment{StudentID=4,CourseID=1050,},
            new Enrollment{StudentID=4,CourseID=4022,Grade=Grade.F},
            new Enrollment{StudentID=5,CourseID=4041,Grade=Grade.C},
            new Enrollment{StudentID=6,CourseID=1045},
            new Enrollment{StudentID=7,CourseID=3141,Grade=Grade.A},
            };

        }
    }
}