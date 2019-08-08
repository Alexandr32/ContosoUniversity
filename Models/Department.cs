using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniversity.Models
{
    /// <summary>
    /// Учебная кафедра
    /// </summary>
    public class Department
    {
        public int DepartmentID { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal Budget { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата начала")]
        public DateTime StartDate { get; set; }

        // Вопросительный знак (?) в приведенном выше коде указывает, что свойство допускает значение null.
        public int? InstructorID { get; set; }


        /// <summary>
        /// Последовательный номер, увеличивающийся при каждом обновлении строки.
        /// Используется для исключения паралельной записи данных
        /// </summary>
        [Timestamp] // Указывает, что этот столбец входит в предложение Where для команд Update и Delete
        public byte[] RowVersion { get; set; }

        public Instructor Administrator { get; set; }

        // Кафедра может иметь несколько курсов, поэтому доступно свойство навигации Courses:
        public ICollection<Course> Courses { get; set; }
    }
}
