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
    public class DeleteModel : PageModel
    {
        // Для взаимодействия с базой данных в контроллере определяется переменная контекст данных
        private readonly ContosoUniversity.Models.SchoolContext _context;

        public DeleteModel(ContosoUniversity.Models.SchoolContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Свойство для отображения выбранного студента 
        /// </summary>
        [BindProperty]
        public Student Student { get; set; }
        /// <summary>
        /// Сообщение об ошибке
        /// </summary>
        public string ErrorMessage { get; set; }

        // параметр saveChangesError указывает, был ли метод вызван после того, 
        // как произошел сбой при удалении объекта учащегося. При ошибке удаления OnPostAsync делает 
        // редирект на страницу с параметром saveChangesError = true
        public async Task<IActionResult> OnGetAsync(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Извлекаем данные
            Student = await _context.Student
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (Student == null)
            {
                return NotFound();
            }

            // Если произошла ошибка выводиться сообщение
            if (saveChangesError.GetValueOrDefault())
            {
                ErrorMessage = "Delete failed. Try again";
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Находим нужного студента
            var student = await _context.Student
                            .AsNoTracking()
                            .FirstOrDefaultAsync(m => m.ID == id);

            if (student == null)
            {
                return NotFound();
            }

            try
            {
                // Удаляем студента
                _context.Student.Remove(student);

                // Сохраняем удаление
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            catch (DbUpdateException /* ex */)
            {
                // Редирект с уведомлением о ошибке saveChangesError = true означет 
                // что OnGetAsync выведет ErrorMessage = "Delete failed. Try again";
                return RedirectToAction("./Delete", new { id, saveChangesError = true });
            }
        }

    }
}
