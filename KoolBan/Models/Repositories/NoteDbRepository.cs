using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using KoolBan.Models.Abstract;

namespace KoolBan.Models.Repositories
{
    public class NoteDbRepository : INoteRepository
    {
        private readonly KoolBanContext _dbContext = new KoolBanContext();

        public Note Find(int id)
        {
            return _dbContext.Notes.Find(id);
        }

        public void Delete(int id)
        {
            _dbContext.Notes.Remove(Find(id));
        }

        public void Create(Note note)
        {
            _dbContext.Notes.Add(note);
        }

        public void Edit(Note note)
        {
            _dbContext.Entry(note).State = EntityState.Modified;
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}