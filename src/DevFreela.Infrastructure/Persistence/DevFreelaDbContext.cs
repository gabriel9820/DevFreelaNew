using Microsoft.EntityFrameworkCore;
using DevFreela.Core.Entities;

namespace DevFreela.Infrastructure.Persistence;

public class DevFreelaDbContext : DbContext
{
    public DbSet<Project> Projects { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Skill> Skills { get; set; }
    public DbSet<UserSkill> UsersSkills { get; set; }
    public DbSet<ProjectComment> ProjectsComments { get; set; }

    public DevFreelaDbContext(DbContextOptions<DevFreelaDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Skill>(e => e.HasKey(x => x.Id));

        builder.Entity<UserSkill>(e =>
        {
            e.HasKey(x => x.Id);
            e.HasOne(x => x.Skill).WithMany().HasForeignKey(x => x.SkillId).OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<ProjectComment>(e =>
        {
            e.HasKey(x => x.Id);
            e.HasOne(x => x.Project).WithMany(x => x.Comments).HasForeignKey(x => x.ProjectId).OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<User>(e =>
        {
            e.HasKey(x => x.Id);
            e.HasMany(x => x.Skills).WithOne(x => x.User).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<Project>(e =>
        {
            e.HasKey(x => x.Id);
            e.HasOne(x => x.Freelancer).WithMany(x => x.FreelanceProjects).HasForeignKey(x => x.FreelancerId).OnDelete(DeleteBehavior.Restrict);
            e.HasOne(x => x.Client).WithMany(x => x.OwnedProjects).HasForeignKey(x => x.ClientId).OnDelete(DeleteBehavior.Restrict);
        });

        base.OnModelCreating(builder);
    }
}
