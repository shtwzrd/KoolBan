using System;
using System.ComponentModel.DataAnnotations;

namespace KoolBan.Models
{
    public class Note
    {
        public int NoteId { get; set; }
        [Required]
        public String Description { get; set; }

        [Required]
        public virtual int ColumnId { get; set; } // Foreign key

        public String Logo { get; set; }

        public string Color { get; set; }
    }

    public enum NoteColor
    {
        Green,
        Emerald,
        Teal,
        Cyan,
        Cobalt,
        Indigo,
        Violet,
        Pink,
        Magenta,
        Crimson,
        Red,
        Orange,
        Brown,
    }


}