using Microsoft.EntityFrameworkCore;
public class ApplicationDbContext : DbContext
{
   public DbSet<Item> items { get; set; }
   public DbSet<TrackedItem> trackedItems { get; set; }
   public ApplicationDbContext()
   {

   }
   public ApplicationDbContext(DbContextOptions options) : base(options)
   {

   }
   protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
   {
      optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CodeFirst;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
   }

   public override int SaveChanges()
   {
      var tracker = ChangeTracker;
      foreach (var entry in tracker.Entries())
      {
         System.Console.WriteLine($"{entry.Entity} has state {entry.State}");
      }
      return base.SaveChanges();
   }
}
