using System.Data.Entity;
using KoolBan.Models.Abstract;

namespace KoolBan.Models.Repositories
{
    public class ProjectDbRepository : IProjectRepository
    {
        private readonly KoolBanContext _dbContext = new KoolBanContext();

        public Project Find(string name)
        {
            return _dbContext.Projects.Find(name);
        }

        public void Create(Project project)
        {
            _dbContext.Projects.Add(project);
        }

        public void Edit(Project project)
        {
            _dbContext.Entry(project).State = EntityState.Modified;
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
    
}