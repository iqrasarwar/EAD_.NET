using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{

   public DbSet<Item> Items { get; set; }
   public DbSet<Category> Categories { get; set; }
   public DbSet<CategoryDetail> CategoryDetails { get; set; }
   public DbSet<Company> Companies { get; set; }

   public ApplicationDbContext()
   {

   }
   public ApplicationDbContext(DbContextOptions options) : base(options)
   {
      //intentionally empty
   }

   protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
   {
      optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=InventoryManagerDb2");
   }

   /*  public override int SaveChanges()
     {
         var tracker = ChangeTracker;
         foreach (var entry in tracker.Entries())
         {
         System.Console.WriteLine($"{entry.Entity} has state {entry.State}");
         }
         return base.SaveChanges();
     }*/

   public override int SaveChanges()
   {
      var tracker = ChangeTracker;
      foreach (var entry in tracker.Entries())
      {
         if (entry.Entity is FullAuditModel)
         {
            var referenceEntity = entry.Entity as Item;
            switch (entry.State)
            {
               case EntityState.Added:
                  referenceEntity.CreatedDate = DateTime.Now;
                  referenceEntity.CreatedByUserId = "1";//hard coded user id
                  break;
               case EntityState.Deleted:
                  referenceEntity.IsDeleted = true;
               case EntityState.Modified:
                  referenceEntity.LastModifiedDate = DateTime.Now;
                  referenceEntity.LastModifiedUserId = "1";//hard coded user id
                  break;
               default:
                  break;
            }
         }
      }
      return base.SaveChanges();
   }

}
