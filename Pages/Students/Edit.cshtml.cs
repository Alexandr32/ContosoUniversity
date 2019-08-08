using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Models;

namespace ContosoUniversity.Pages.Students
{
    public class EditModel : PageModel
    {
        // Для взаимодействия с базой данных в контроллере определяется переменная контекст данных
        private readonly ContosoUniversity.Models.SchoolContext _context;

        public EditModel(ContosoUniversity.Models.SchoolContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Свойство для отображения выбранного студента 
        /// </summary>
        [BindProperty]
        public Student Student { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Извлекаем данные  по id
            Student = await _context.Student.FindAsync(id);

            if (Student == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            // Проверка есть ли ошибки
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Извлекается текущий студент по id
            var studentToUpdate = await _context.Student.FindAsync(id);

            // Обновляет указанный экземпляр модели, (Обновляет emptyStudent с свойствами из cshtml Student)
            // Второй параметр не чувствителен к регистру букв (!)
            if (await TryUpdateModelAsync(studentToUpdate, "Student", s => s.FirstMidName, s => s.LastName, s => s.EnrollmentDate))
            {
                // Если изменение произошло удачно сохраняем данные в БД и переходим на гланую страницу
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}
