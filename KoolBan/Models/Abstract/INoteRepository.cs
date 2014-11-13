namespace KoolBan.Models.Abstract
{
    public interface INoteRepository
    {
        Note Find(int id);
        void Delete(int id);
        void Create(Note note);
        void Edit(Note note);
        void Save();
    }
}
