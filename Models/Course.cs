using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniversity.Models
{
    /// <summary>
    /// Курс
    /// </summary>
    public class Course
    {
        // Атрибут DatabaseGenerated позволяет приложению указать первичный ключ, 
        // а не использовать созданный базой данных.
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Номер курса")]
        public int CourseID { get; set; }
        /// <summary>
        /// Название
        /// </summary>
        [StringLength(50, MinimumLength = 3)]
        public string Title { get; set; }

        [Range(0, 5)]
        public int Credits { get; set; }

        public int DepartmentID { get; set; }

        // Cущность Department имеет значение null, если она не загружена явно;
        public Department Department { get; set; }
        public ICollection<CourseAssignment> CourseAssignments { get; set; }

        // Свойство Enrollments является свойством навигации.
        // На курс может быть зачислено любое количество учащихся, 
        // поэтому свойство навигации Enrollments является коллекцией
        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
