using NipSearcher.Entities;
using NipSearcher.Db;
using Microsoft.EntityFrameworkCore;

namespace NipSearcher
{
    public interface IRepository
    {
        void SaveSubject(Subject subject);
        List<Subject> GetSubjects();
    }

    public class Repository : IRepository
    {
        public void SaveSubject(Subject subject)
        {
            using (CompanyDbContext context = new CompanyDbContext())
            {
                context.Subjects.Add(subject);
                context.SaveChanges();
            }
        }

        public List<Subject> GetSubjects()
        {
            using (CompanyDbContext context = new CompanyDbContext())
            {
            var query = context.Subjects.Include(s => s.Accounts).Include(s => s.Representatives);
            return query.ToList();
            }
        }

    }
}
