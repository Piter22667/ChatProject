using Microsoft.EntityFrameworkCore;


namespace ChatProject.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {

        }
        public DbSet<Models.User> User { get; set; }
        public DbSet<Models.Chat> Chats { get; set; }
        public DbSet<Models.Message> Messages { get; set; }
        public DbSet<Models.Archived> Archived { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Models.User>(entity =>
            {
                entity.HasKey(e => e.Id); //встановлення primary key
                entity.Property(e => e.Login).IsRequired().HasMaxLength(20);
                entity.Property(e => e.Password).IsRequired().HasMaxLength(60);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Surname).IsRequired().HasMaxLength(50);
                entity.Property(e => e.LastName).HasMaxLength(50);
            });
            modelBuilder.Entity<Models.Chat>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(50);
                entity.Property(e => e.UserId);
                entity.Property(e => e.IsClosed).IsRequired().HasDefaultValue("false");
                entity.Property(e => e.CreationTime).IsRequired().HasDefaultValueSql("GETUTCDATE()");
                entity.Property(e => e.CreationTime);
                entity.Property(e => e.Expiration);
                entity.Property(e => e.UpdatedTime).IsRequired().HasDefaultValueSql("GETUTCDATE()");
            });
            ///
            modelBuilder.Entity<Models.Message>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.ChatId).IsRequired();
                entity.Property(e => e.UserId).IsRequired();
                entity.Property(e => e.Content).IsRequired();
                entity.Property(e => e.SentTime).IsRequired().HasDefaultValueSql("GETUTCDATE()").ValueGeneratedOnAdd();
            });
            ///
            modelBuilder.Entity<Models.Archived>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.ChatId).IsRequired();
                entity.Property(e => e.UserId).IsRequired();
                entity.Property(e => e.Content).IsRequired();
                entity.Property(e => e.SentTime).IsRequired();
            });


            //встановлення зв'язків
            modelBuilder.Entity<Models.Message>() //один юзер може мати багато повідомлень
                .HasOne(m => m.User)
                .WithMany(u => u.Messages)
                .HasForeignKey(m => m.UserId)
                .OnDelete(DeleteBehavior.Restrict);



            modelBuilder.Entity<Models.Archived>()
                .HasOne(m => m.User)
                .WithMany(u => u.Archived)
                .HasForeignKey(m => m.UserId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Models.Message>()
                .HasOne(m => m.Chat)
                .WithMany(c => c.Messages)
                .HasForeignKey(m => m.ChatId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Models.Archived>()
                .HasOne(m => m.Chat)
                .WithMany(c => c.ArchivedMessages)
                .HasForeignKey(m => m.ChatId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Models.Chat>()
                .HasOne(c => c.User)
                .WithMany(u => u.Chats)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
