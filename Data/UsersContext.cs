using Microsoft.EntityFrameworkCore;
using Model.UsersModel;


namespace Data
{

    public class UsersContext : DbContext
    {
       public DbSet<UsersModel> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=users.sqlite");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UsersModel>((u) => {
                u.HasKey(u => u.Id);
                u.Property(u => u.Username).IsRequired();
                u.Property(u => u.Password).IsRequired();
            });
                
                
          
        }
    }
}