using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniversity.Models
{
    public class Student
    {
        // Свойство ID используется в качестве столбца первичного ключа в таблице базы данных
        public int ID { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1)]
        [Display(Name = "Имя")]
        public string LastName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        [Column("FirstName")]
        [Display(Name = "Фамилия")]
        public string FirstMidName { get; set; }

        /// <summary>
        /// Дата поступления
        /// </summary>
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата поступления")]
        public DateTime EnrollmentDate { get; set; }

        /// <summary>
        /// Полное имя
        /// </summary>
        [Display(Name = "Полное имя")]
        public string FullName
        {
            get => LastName + ", " + FirstMidName;
        }

        /// <summary>
        /// Список учащихся на курсах и их оценки
        /// </summary>
        public ICollection<Enrollment> Enrollments { get; set; }
        // Свойство Enrollments является свойством навигации. 
        // Свойства навигации ссылаются на другие сущности, связанные с этой сущностью
        // Если используется ICollection<T>, платформа EF Core по умолчанию создает коллекцию 
        // HashSet<T>.Свойства навигации, содержащие несколько сущностей, поступают по связям 
        // многие ко многим и один ко многим.
    }
}
