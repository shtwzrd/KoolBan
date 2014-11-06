using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace KoolBan.Models
{
    public class KoolBanContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<Column> Columns { get; set; }
        public DbSet<Note> Notes { get; set; }

        public KoolBanContext() : base("KoolBan")
        {
            
        }
    }
}