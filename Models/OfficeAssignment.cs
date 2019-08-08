using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniversity.Models
{
    /// <summary>
    /// Аудитория (кабинет)
    /// </summary>
    public class OfficeAssignment
    {
        // [Key] используется для идентификации свойства в качестве первичного ключа (PK), 
        // когда имя свойства отличается от classnameID или ID.
        [Key] 
        public int InstructorID { get; set; }

        [StringLength(50)]
        [Display(Name = "Расположение кабинета")]
        public string Location { get; set; }

        [Required]
        public Instructor Instructor { get; set; }
    }
}
