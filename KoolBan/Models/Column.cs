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
        public virtual String ProjectId { get; set; } // Foreign key
        public virtual Project Project { get; set; }

        public int Priority { get; set; }
        public int Capacity { get; set; }

        // Navigation property
        public virtual IList<Note> Notes { get; set; }
    }
}