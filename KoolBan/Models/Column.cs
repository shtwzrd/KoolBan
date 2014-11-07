using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KoolBan.Models
{
    public class Column
    {
        public int ColumnId { get; set; }

        [Required(ErrorMessage = "Column name required")]
        [Display(Name = "Column Name")]
        public String ColumnName { get; set; }

        [Required]
        public int ProjectId { get; set; } // Foreign key
        public Project Project { get; set; }

        public int Capacity { get; set; }
        public int MaxCapacity { get; set; }

        public IList<Note> Notes { get; set; }
    }
}