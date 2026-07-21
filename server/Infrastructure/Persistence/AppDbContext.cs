using Microsoft.EntityFrameworkCore;
using server.Models.Projects;

namespace server.Infrastructure.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Project> Projects { get; set; }
    public DbSet<ProjectLocalization> ProjectLocalizations { get; set; }
}