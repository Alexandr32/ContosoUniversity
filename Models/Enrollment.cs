using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniversity.Models
{

    /// <summary>
    /// Оценка
    /// </summary>
    public enum Grade
    {
        A, B, C, D, F
    }

    /// <summary>
    /// Список учащихся на курсах и их оценки
    /// </summary>
    public class Enrollment
    {
        // Первичный ключ сущности
        public int EnrollmentID { get; set; }

        // Свойство CourseID представляет собой внешний ключ. 
        // Ему соответствует свойство навигации Course. 
        // Сущность Enrollment связана с одной сущностью Course.
        public int CourseID { get; set; }

        // Свойство StudentID представляет собой внешний ключ. 
        // Ему соответствует свойство навигации Student
        public int StudentID { get; set; }

        // Оценка со значением null отличается от нулевой оценки тем, что при 
        // таком значении оценка еще не известна или не назначена.
        /// <summary>
        /// Оценка
        /// </summary>
        [DisplayFormat(NullDisplayText = "No grade")]
        public Grade? Grade { get; set; } // ? указывает что свойство Grade допускает значение null
        
        // Сущность Enrollment связана с одной сущностью Course.
        public Course Course { get; set; }

        // Сущность Enrollment связана с одной сущностью Student.
        public Student Student { get; set; }
    }
}
