using CMC.TS.FT.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace CMC.TS.FT.Api.Data
{
    public class SQLServerDbContext : DbContext
    {
        DbSet<User> users;
        DbSet<Role> roles;
        public SQLServerDbContext(DbContextOptions builder) : base(builder)
        {
        }

        public SQLServerDbContext() { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.HasOne
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(x => x.Id);
            });
        }
    }
}
