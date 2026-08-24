using CMC.TS.FT.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace CMC.TS.FT.Api.Data
{
    public class SQLServerDbContext : DbContext
    {
        DbSet<User> User;
        DbSet<Role> Role;
        DbSet<RolePermission> RolePermission;
        DbSet<UserRole> UserRole;


        public SQLServerDbContext(DbContextOptions builder) : base(builder)
        {
        }

        public SQLServerDbContext() { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(x => x.UserId);

                entity.Property(d=>d.Email).IsRequired();

                entity.HasIndex(d=>d.Email).IsUnique();

                entity.Property(d => d.PasswordHash).IsRequired();
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(x => x.RoleId);

                entity.HasIndex(x => x.RoleName).IsUnique();

                entity.Property(x => x.RoleName).IsRequired();
            });

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.HasKey(x => x.PermissionId);

                entity.HasIndex(x => x.PermissionName).IsUnique();

                entity.Property(x => x.PermissionName).IsRequired();
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(dt => new { dt.UserId, dt.RoleId});
                entity.HasOne<User>().WithMany().HasForeignKey(u=> u.UserId);
                entity.HasOne<Role>().WithMany().HasForeignKey(r => r.RoleId);

            });

            modelBuilder.Entity<RolePermission>(entity =>
            {
                entity.HasKey(dt => new { dt.PermissionId, dt.RoleId });
                entity.HasOne<Role>().WithMany().HasForeignKey(r=>r.RoleId);
                entity.HasOne<Permission>().WithMany().HasForeignKey(p=>p.PermissionId);
            });
        }
    }
}
