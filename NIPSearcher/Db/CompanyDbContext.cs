using Microsoft.EntityFrameworkCore;
using NipSearcher.Entities;

namespace NipSearcher.Db
{
    public class CompanyDbContext : DbContext
    {
        public DbSet<Subject> Subjects { get; set; } = null!;
        public DbSet<Person> Persons { get; set; } = null!;
        public DbSet<Account> AccountNumbers { get; set; } = null!;

        public string DbPath { get; }

        public CompanyDbContext()
        {
            DbPath = "nip.db";
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Subject>(entity =>
            {
                entity.HasKey(e => e.SubjectId);
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasKey(e => e.PersonId);

                entity.HasOne(e => e.Subject)
                .WithMany(e => e.Representatives);
            });

            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.AccountId);

                entity.HasOne(e => e.Subject)
                .WithMany(e => e.Accounts);
            });
        }
    }
}
