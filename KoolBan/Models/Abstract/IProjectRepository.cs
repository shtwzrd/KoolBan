using System;

namespace KoolBan.Models.Abstract
{
    public interface IProjectRepository
    {
        Project Find(String name);
//        void Delete(int id);
        void Create(Project project);
        void Edit(Project project);
        void Save();
    }
}
