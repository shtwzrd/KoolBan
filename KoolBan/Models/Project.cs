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
        public String Name { get; set; }

        [Required]
        public bool IsPublic { get; set; }

        public String Password { get; set; }

        public IList<Column> Columns { get; set; }
    }
}