using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Models;
using ContosoUniversity.Models.SchoolViewModels;

namespace ContosoUniversity.Pages.Instructors
{
    public class IndexModel : PageModel
    {
        private readonly ContosoUniversity.Models.SchoolContext _context;

        public IndexModel(ContosoUniversity.Models.SchoolContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Свойство для преподователя
        /// </summary>
        public InstructorIndexData Instructor { get; set; }
        public int InstructorID { get; set; }
        public int CourseID { get; set; }

        public async Task OnGetAsync(int? id, int? courseID)
        {
            Instructor = new InstructorIndexData
            {
                Instructors = await _context.Instructors
                .Include(i => i.OfficeAssignment)
                .Include(i => i.CourseAssignments)
                .ThenInclude(i => i.Course)
                .ThenInclude(i => i.Department)
                .Include(i => i.CourseAssignments)
                .ThenInclude(i => i.Course)
                .ThenInclude(i => i.Enrollments)
                .ThenInclude(i => i.Student)
                .AsNoTracking()
                .OrderBy(i => i.LastName)
                .ToListAsync()
            };

            if (id != null)
            {
                // Метод Where возвращает коллекцию. Where возвращается всего 
                // одна сущность Instructor. Метод Single преобразует коллекцию в отдельную сущность
                // Instructor. Сущность Instructor предоставляет доступ к свойству
                // CourseAssignments.CourseAssignments предоставляет доступ к связанным сущностям Course.
                // Метод Single используется для коллекции, когда она содержит всего один элемент.
                // Метод Single вызывает исключение, если коллекция пуста или содержит больше одного
                // элемента.Альтернативным вариантом является метод SingleOrDefault, который возвращает
                // значение по умолчанию(в данном случае null), если коллекция пуста.
                InstructorID = id.Value;
                // Можно использовать два варианта Single или Where
                // Instructor instructor = Instructor.Instructors.Where(i => i.ID == id.Value).Single();
                Instructor instructor = Instructor.Instructors.Single(i => i.ID == id.Value);
                Instructor.Courses = instructor.CourseAssignments.Select(s => s.Course);
            }

            if (courseID != null)
            {
                // Заполняет свойство Enrollments модели представления при выборе курса:
                CourseID = courseID.Value;
                // Можно использовать два варианта Single или Where
                Instructor.Enrollments = Instructor.Courses.Where(x => x.CourseID == courseID).Single().Enrollments;
                // Instructor.Enrollments = Instructor.Courses.Single(x => x.CourseID == courseID).Enrollments;
            }
        }
    }
}