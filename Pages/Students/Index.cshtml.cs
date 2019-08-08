using ContosoUniversity.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniversity.Pages.Students
{
    public class IndexModel : PageModel
    {
        // Для взаимодействия с базой данных в контроллере определяется переменная контекст данных
        private readonly ContosoUniversity.Models.SchoolContext _context;

        public IndexModel(ContosoUniversity.Models.SchoolContext context)
        {
            _context = context;
        }

        public PaginatedList<Student> Student { get;set; }

        // Параметр sortOrder из строки запроса в URL-адресе. URL-адрес
        // (включая строку запроса) формируется вспомогательной функцией тегов привязки
        public async Task OnGetAsync(
            string sortOrder, 
            string currentFilter, 
            string searchString, 
            int? pageIndex)
        {
            CurrentSort = sortOrder;

            // NameSort и DateSort используется как свойтво для фильтрации через asp-route-sortOrder
            // Присваивается противоположное значение относительно текущего параметра сотояния 
            // для дальнейшей сортировки при выборе сортировки
            NameSort = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            // Значение поиска
            CurrentFilter = searchString;

            // Запрос 
            IQueryable<Student> studentIQ = from s in _context.Student
                                            select s;

            // Выполняется, только если задано значение для поиска.
            if (!string.IsNullOrEmpty(searchString))
            {
                // Where отбирает только учащихся, чье имя или фамилия содержат строку поиска.
                studentIQ = studentIQ.Where(
                    s => s.LastName.Contains(searchString) || s.FirstMidName.Contains(searchString)
                    );
            }

            // Сортировка в зависимости от переданного параметра
            switch (sortOrder)
            {
                // Выборка по имени Порядок по убыванию
                case "name_desc":
                    studentIQ = studentIQ.OrderByDescending(s => s.LastName);
                    break;
                // Выборка по дате Порядок по возростанию
                case "Date":
                    studentIQ = studentIQ.OrderBy(s => s.EnrollmentDate);
                    break;
                // Выборка по дате порядок по убыванию
                case "date_desc":
                    studentIQ = studentIQ.OrderByDescending(s => s.EnrollmentDate);
                    break;
                // По умолчанию используется порядок сортировки по возрастанию.
                default:
                    studentIQ = studentIQ.OrderBy(s => s.LastName);
                    break;
            }

            // Количество пунктов на странице
            int pageSize = 3;

            // Запрос не выполнится, пока объект IQueryable не будет преобразован в коллекцию
            Student = await PaginatedList<Student>.CreateAsync(studentIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }

        /// <summary>
        /// Свойства для сортировки имени
        /// </summary>
        public string NameSort { get; set; }
        /// <summary>
        /// Свойства для сортировки по дате
        /// </summary>
        public string DateSort { get; set; }
        /// <summary>
        /// Свойства для поиска по строке ввведенной пользователем
        /// </summary>
        public string CurrentFilter { get; set; }
        /// <summary>
        /// Свойство сортировки
        /// </summary>
        public string CurrentSort { get; set; }
    }
}
