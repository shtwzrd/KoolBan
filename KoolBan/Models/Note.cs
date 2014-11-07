using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Web;

namespace KoolBan.Models
{
    public class Note
    {
        public int NoteId { get; set; }
        [Required]
        public String Description { get; set; }
        [Required]
        public int ColumnId { get; set; } // Foreign key
        public Column Column { get; set; }
        public String Logo { get; set; }

        [Display(Name = "Color")]
        public NoteColor NoteColor { get; set; }
    }

    public enum NoteColor
    {
        black,
        lime,
        green,
        emerald,
        teal,
        cyan,
        cobalt,
        indigo,
        violet,
        pink,
        magenta,
        crimson,
        red,
        orange,
        amber,
        yellow,
        brown,
        olive,
        steel,
        mauve,
        taupe,
        gray
    }

}