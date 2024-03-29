﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity.Models
{
    public class Instructor
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Имя")]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [Column("FirstName")]
        [Display(Name = "Фамилия")]
        [StringLength(50)]
        public string FirstMidName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата приема на работу")]
        public DateTime HireDate { get; set; }

        [Display(Name = "Полное имя")]
        public string FullName
        {
            get => LastName + ", " + FirstMidName; 
        }
        /// <summary>
        /// Курсовые задания
        /// </summary>
        public ICollection<CourseAssignment> CourseAssignments { get; set; }

        /// <summary>
        /// Кабинет
        /// </summary>
        public OfficeAssignment OfficeAssignment { get; set; }
    }
}