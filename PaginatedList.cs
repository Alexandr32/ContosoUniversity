using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniversity
{
    /// <summary>
    /// Класс для указателя и разбиения на страницы
    /// </summary>
    public class PaginatedList<T> : List<T>
    {
        /// <summary>
        /// Номер страницы
        /// </summary>
        public int PageIndex { get; private set; }
        /// <summary>
        /// Общее кол-во страниц
        /// </summary>
        public int TotalPages { get; private set; }

        
        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            // Номер страницы
            PageIndex = pageIndex;
            // Общее количество страниц
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            this.AddRange(items);
        }

        /// <summary>
        /// Включение кнопки листание Назад 
        /// </summary>
        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 1);
            }
        }

        /// <summary>
        /// Включение кнопки листание вперед
        /// </summary>
        public bool HasNextPage
        {
            get
            {
                return (PageIndex < TotalPages);
            }
        }

        // принимает размер и номер страницы
        public static async Task<PaginatedList<T>> CreateAsync(
            IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync();

            // Возвращает список, содержащий только запрошенную страницу
            var items = await source.Skip(
                (pageIndex - 1) * pageSize)
                .Take(pageSize).ToListAsync(); 

            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }
    }
}
