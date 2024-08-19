using Microsoft.EntityFrameworkCore;

namespace VAPV.Models {
    public class ExcuseDbContext : DbContext {
        public ExcuseDbContext(DbContextOptions<ExcuseDbContext> options) : base(options) {
        }

        public DbSet<Excuse> Excuses { get; set; }
        public DbSet<CalendarDay> CalendarDays { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CalendarDay>().HasNoKey(); // Označení entity jako bezklíčové
        }
    }

   
}
    

