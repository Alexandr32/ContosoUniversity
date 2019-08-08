using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Models;

namespace ContosoUniversity.Pages.Students
{
    public class DetailsModel : PageModel
    {
        // Для взаимодействия с базой данных в контроллере определяется переменная контекст данных
        private readonly ContosoUniversity.Models.SchoolContext _context;

        public DetailsModel(ContosoUniversity.Models.SchoolContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Свойство для отображения выбранного студента 
        /// </summary>
        public Student Student { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Методы Include и ThenInclude инструктируют контекст для загрузки свойства 
            // навигации Student.Enrollments,
            // а также свойства навигации Enrollment.Course в пределах каждой регистрации
            Student = await _context.Student
                        .Include(s => s.Enrollments)
                        .ThenInclude(e => e.Course)
                        .AsNoTracking() // повышает производительность в тех сценариях, где возвращаемые сущности не обновляются в текущем контексте.
                        .FirstOrDefaultAsync(m => m.ID == id);

            if (Student == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
