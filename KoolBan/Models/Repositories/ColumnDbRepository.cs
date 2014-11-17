using System.Data.Entity;
using KoolBan.Models.Abstract;

namespace KoolBan.Models.Repositories
{
    public class ColumnDbRepository : IColumnRepository
    {
        private readonly KoolBanContext _dbContext = new KoolBanContext();

        public Column Find(int id)
        {
            return _dbContext.Columns.Find(id);
        }

        public void Delete(int id)
        {
            _dbContext.Columns.Remove(Find(id));
        }

        public void Create(Column column)
        {
            _dbContext.Columns.Add(column);
        }

        public void Edit(Column column)
        {
            _dbContext.Entry(column).State = EntityState.Modified;
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}