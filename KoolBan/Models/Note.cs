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

        public static string NoteColorToHex(NoteColor color)
        {
            switch (color)
            {
                case NoteColor.Green:
                    return "#60a917";
                case NoteColor.Emerald:
                    return "#008a00";
                case NoteColor.Teal:
                    return "#00aba9";
                case NoteColor.Cyan:
                    return "#1ba1e2";
                case NoteColor.Cobalt:
                    return "#0050ef";
                case NoteColor.Indigo:
                    return "#6a00ff";
                case NoteColor.Violet:
                    return "#aa00ff";
                case NoteColor.Pink:
                    return "#dc4fad";
                case NoteColor.Magenta:
                    return "#d80073";
                case NoteColor.Red:
                    return "#e51400";
                case NoteColor.Orange:
                    return "#fa6800";
                case NoteColor.Brown:
                    return "#825a2c";
                case NoteColor.Crimson:
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