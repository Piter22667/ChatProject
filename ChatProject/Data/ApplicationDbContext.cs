using Microsoft.EntityFrameworkCore;
using ChatProject.Models.Files;


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
        public DbSet<Models.ChatUsers> ChatUsers { get; set; }
        public DbSet<ChatFile> ChatFile { get; set; }
        public DbSet<ChatFileConnections> ChatFileConnections { get; set; }

        public DbSet<ChatFileNameMap> ChatFileNameMap { get; set; }


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

            modelBuilder.Entity<Models.ChatUsers>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.ChatId).IsRequired(false);
                entity.Property(e => e.UserId).IsRequired(false);
            });

            modelBuilder.Entity<ChatFile>(entity =>
            {
                entity.HasKey(e => e.StreamId);
                entity.Property(e => e.StreamId).HasColumnName("stream_id");
                entity.Property(e => e.Name).HasColumnName("name");
                entity.Property(e => e.CreationTime).HasColumnName("creation_time").HasColumnType("DateTimeOffset");
                entity.Property(e => e.LastAccessTime).HasColumnName("last_access_time").HasColumnType("DateTimeOffset");
                entity.Property(e => e.LastUpdatedTime).HasColumnName("last_write_time").HasColumnType("DateTimeOffset");
                entity.Property(e => e.FileStream).HasColumnName("file_stream").HasColumnType("varbinary(max)").IsRequired(false);
                entity.Property(e => e.FileType).HasColumnName("file_type").HasComputedColumnSql("[RIGHT(Name; CHARINDEX('.', RESERVE(Name)) -1\r\n]");
            });

            modelBuilder.Entity<ChatFileConnections>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.MessageId).IsRequired(false);
                entity.Property(e => e.FileId).IsRequired();


            });


            modelBuilder.Entity<ChatFileNameMap>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.HashedName).IsRequired(false);
                entity.Property(e => e.OriginalName).IsRequired(false);
                entity.Property(e => e.FileId).IsRequired(false);
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

            modelBuilder.Entity<Models.ChatUsers>()
                .HasOne(e => e.Chat)
                .WithMany(c => c.ChatUsers)
                .HasForeignKey(e => e.ChatId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Models.ChatUsers>()
                .HasOne(e => e.User)
                .WithMany(u => u.ChatUsers)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ChatFileConnections>()
                .HasOne(e => e.ChatFile)
                .WithMany(e => e.ChatFileConnections)
                .HasForeignKey(e => e.FileId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            //modelBuilder.Entity<ChatFileConnections>()
            //   .HasOne(e => e.Chat)
            //   .WithMany(e => e.ChatFileConnections)
            //   .HasForeignKey(e => e.ChatId)
            //   .OnDelete(DeleteBehavior.ClientSetNull);


            modelBuilder.Entity<ChatFileConnections>()
               .HasOne(e => e.Message)
               .WithMany(e => e.ChatFileConnections)
               .HasForeignKey(e => e.MessageId)
               .OnDelete(DeleteBehavior.ClientSetNull);


            ////Зв'зок між ChatFiles та ChatFileNameMap 1:N
            //modelBuilder.Entity<ChatFileNameMap>()
            //    .HasOne(e => e.ChatFile)
            //    .WithMany(e => e.ChatFileNameMap)
            //    .HasForeignKey(e => e.FileId)
            //    .OnDelete(DeleteBehavior.ClientSetNull);

            //modelBuilder.Entity<ChatFileNameMap>()
            //    .HasOne(e => e.Message)
            //    .WithMany(e => e.ChatFileNameMap)
            //    .HasForeignKey(e => e.MessageId)
            //    .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
