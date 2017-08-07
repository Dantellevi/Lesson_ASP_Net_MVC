using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Metanit_Many_to_Many.Models
{
    public class StudentsContext:DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }

        public StudentsContext() : base("ManyToManyConnection")
        { }


        /*
         * Для построения этой таблицы мы переопределяем метод OnModelCreating,
         *в котором с помощью объекта modelBuilder создаем новую таблицу и определяем ее поля.
         *Одно ее поле - CourseId - будет ссылаться на таблицу Courses и хранить в себе id курса.
         *А второе поле - StudentId - будет ссылаться на таблицу студентов и хранить id студента.
         *В итоге у нас получится набор пар id курса - id студента, благодаря этому мы сможем определить связь
         *многие-ко-многим.
         * */
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().HasMany(c => c.Students)
                .WithMany(s => s.Courses)
                .Map(t => t.MapLeftKey("CourseId")
                .MapRightKey("StudentId")
                .ToTable("CourseStudent"));
        }
    }
}