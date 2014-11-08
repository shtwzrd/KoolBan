using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KoolBan.Models
{
    public class Project
    {
        [Key]
        [Required(ErrorMessage = "Project name required")]
        public String ProjectId { get; set; }

        [Required]
        [Display(Name = "Private?")]
        public bool IsPrivate { get; set; }

        public String Password { get; set; }

        public virtual IList<Column> Columns { get; set; }
    }
}