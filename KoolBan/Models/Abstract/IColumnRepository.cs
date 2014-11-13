namespace KoolBan.Models.Abstract
{
    public interface IColumnRepository
    {
        Column Find(int id);
        void Delete(int id);
        void Create(Column column);
        void Edit(Column column);
        void Save();
    }
}
