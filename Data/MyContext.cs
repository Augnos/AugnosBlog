#pragma warning disable CS8618
using Microsoft.EntityFrameworkCore;
namespace AugnosBlog.Models;

public class MyContext : DbContext
{
    public MyContext(DbContextOptions options) : base(options) { }
    public DbSet<BlogUser> BlogUsers { get; set; }
}