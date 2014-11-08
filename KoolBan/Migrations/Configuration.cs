using KoolBan.Models;

namespace KoolBan.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<KoolBan.Models.KoolBanContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(KoolBan.Models.KoolBanContext context)
        {
            
            context.Projects.AddOrUpdate(
                p => p.ProjectId,
                new Project { ProjectId = "Demo", IsPrivate = false}
            );

            context.Columns.AddOrUpdate(
                c => c.ColumnId,
                new Column { ColumnId = 1, ColumnName = "To Do", Priority = 1, ProjectId = "Demo" },
                new Column { ColumnId = 2, ColumnName = "In Progress", Priority = 2, Capacity = 4, ProjectId = "Demo" },
                new Column { ColumnId = 3, ColumnName = "Test", Priority = 3, Capacity = 4, ProjectId = "Demo" },
                new Column { ColumnId = 4, ColumnName = "Done", Priority = 4, ProjectId = "Demo" }
            );

            context.Notes.AddOrUpdate(
                n => n.NoteId,
                new Note { NoteId = 1, ColumnId = 1, Description = "Eat a piece of cake", Logo = "heart", NoteColor = NoteColor.Green },
                new Note { NoteId = 2, ColumnId = 2, Description = "Cut the cake into pieces", Logo = "heart", NoteColor = NoteColor.Green },
                new Note { NoteId = 3, ColumnId = 3, Description = "Check if the cake is poisonous", Logo = "heart", NoteColor = NoteColor.Green },
                new Note { NoteId = 4, ColumnId = 4, Description = "Check if the cake is a lie. Note: It wasn't a lie!", Logo = "heart", NoteColor = NoteColor.Green },
                new Note { NoteId = 5, ColumnId = 4, Description = "Purchase cake using Siri", Logo = "heart", NoteColor = NoteColor.Green },
                new Note { NoteId = 6, ColumnId = 4, Description = "Teach a mouse to ride a bike", Logo = "plane", NoteColor = NoteColor.Orange },
                new Note { NoteId = 7, ColumnId = 2, Description = "Purchase a space rocket big enough to carry at least 40 mice to Mars", Logo = "plane", NoteColor = NoteColor.Orange },
                new Note { NoteId = 8, ColumnId = 1, Description = "Come up with a cool name for the mice. Note: Something with Biker...", Logo = "plane", NoteColor = NoteColor.Orange },
                new Note { NoteId = 9, ColumnId = 3, Description = "Figure out how to get enough cash for the trip using crowdfunding", Logo = "plane", NoteColor = NoteColor.Orange },
                new Note { NoteId = 10, ColumnId = 2, Description = "Research how to make all clouds in the world rainbow-coloured", Logo = "eye-open", NoteColor = NoteColor.Teal },
                new Note { NoteId = 11, ColumnId = 3, Description = "Research how to make all water in the world rainbow-coloured", Logo = "eye-open", NoteColor = NoteColor.Teal },
                new Note { NoteId = 12, ColumnId = 4, Description = "Research how to make all food in the world rainbow-coloured", Logo = "eye-open", NoteColor = NoteColor.Teal },
                new Note { NoteId = 13, ColumnId = 1, Description = "Research how to undo all rainbow-colour changes made", Logo = "eye-open", NoteColor = NoteColor.Teal },
                new Note { NoteId = 14, ColumnId = 1, Description = "Buy flowers for a loved one", Logo = "usd", NoteColor = NoteColor.Pink },
                new Note { NoteId = 15, ColumnId = 3, Description = "Buy a unicorn for your best friend", Logo = "usd", NoteColor = NoteColor.Red },
                new Note { NoteId = 16, ColumnId = 4, Description = "Purchase a jet-pack", Logo = "usd", NoteColor = NoteColor.Emerald },
                new Note { NoteId = 17, ColumnId = 2, Description = "Organise work desk", Logo = "tasks", NoteColor = NoteColor.Crimson },
                new Note { NoteId = 18, ColumnId = 3, Description = "Clean all windows", Logo = "tasks", NoteColor = NoteColor.Indigo },
                new Note { NoteId = 19, ColumnId = 4, Description = "Learn how to juggle", Logo = "tasks", NoteColor = NoteColor.Violet }
            );
            
        }
    }
}
