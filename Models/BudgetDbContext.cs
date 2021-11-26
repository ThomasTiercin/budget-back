using Microsoft.EntityFrameworkCore;

namespace Budget.Models
{
    public class BudgetDbContext : DbContext
    {
        public DbSet<Categorie> Categorie { get; set; }
        public DbSet<Compte> Compte { get; set; }
        public DbSet<Echeance> Echeance { get; set; }
        public DbSet<Mouvement> Mouvement { get; set; }
        public DbSet<Organisme> Organisme { get; set; }
        public DbSet<Type> Type { get; set; }
        public DbSet<User> User { get; set; }
        public BudgetDbContext(DbContextOptions<BudgetDbContext> options)
            : base(options)
        { }

    }
}
