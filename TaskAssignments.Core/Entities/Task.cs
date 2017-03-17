using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskAssignments.Core.Entities
{

    [Table("Task")]
    public class Task
    {
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string Title { get; set; }
        public string Description { get; set; }
        [DataType(DataType.Date), Display(Name = "Due Date"), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime DueDate { get; set; }
        public string Status { get; set; }
        public virtual ICollection<ApplicationUser> Users { get; set; }
    }
   
}
