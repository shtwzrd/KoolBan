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
        public virtual int ColumnId { get; set; } // Foreign key
        public virtual Column Column { get; set; }

        public String Logo { get; set; }

        [Display(Name = "Color")]
        public NoteColor NoteColor { get; set; }

        public static string NoteColorToHex(NoteColor color)
        {
            switch (color)
            {
                case Models.NoteColor.Green:
                    return "#60a917";
                case Models.NoteColor.Emerald:
                    return "#008a00";
                case Models.NoteColor.Teal:
                    return "#00aba9";
                case Models.NoteColor.Cyan:
                    return "#1ba1e2";
                case Models.NoteColor.Cobalt:
                    return "#0050ef";
                case Models.NoteColor.Indigo:
                    return "#6a00ff";
                case Models.NoteColor.Violet:
                    return "#aa00ff";
                case Models.NoteColor.Pink:
                    return "#dc4fad";
                case Models.NoteColor.Magenta:
                    return "#d80073";
                case Models.NoteColor.Red:
                    return "#e51400";
                case Models.NoteColor.Orange:
                    return "#fa6800";
                case Models.NoteColor.Brown:
                    return "#825a2c";
                case Models.NoteColor.Crimson:
                    return "#a20025";
                default:
                    return "#ffffff";
            }
    
        }
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