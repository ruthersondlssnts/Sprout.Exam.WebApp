using System;
using System.ComponentModel.DataAnnotations;

namespace Sprout.Exam.Business.DataTransferObjects
{
    public abstract class BaseSaveEmployeeDto
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Tin { get; set; }
        [Required]
        public DateTime Birthdate { get; set; }
        [Required]
        public int TypeId { get; set; }
    }
}
