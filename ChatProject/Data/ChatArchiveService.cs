using ChatProject.Models;

namespace ChatProject.Data
{
    public class ChatArchiverService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly TimeSpan _checkInterval = TimeSpan.FromSeconds(5); // Перевірка кожні 10c

        public ChatArchiverService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                    await ArchiveExpiredChats(dbContext);
                }
                await Task.Delay(_checkInterval, stoppingToken);
            }
        }

        private async Task ArchiveExpiredChats(ApplicationDbContext dbContext)
        {
            var now = DateTime.UtcNow;
            var expiredChats = dbContext.Chats
                .Where(c => !c.isArchived &&
                    (c.Expiration.HasValue && c.Expiration <= now) ||  // Чат має вказаний час архівації
                    (!c.Expiration.HasValue && c.CreationTime.AddDays(14) <= now)) // Автоархівація через 14 днів
                .ToList();

            foreach (var chat in expiredChats)
            {
                var messages = dbContext.Messages.Where(m => m.ChatId == chat.Id).ToList();

                var archivedMessages = messages.Select(m => new Archived
                {
                    ChatId = m.ChatId,
                    UserId = m.UserId,
                    Content = m.Content,
                    SentTime = m.SentTime
                }).ToList();

                dbContext.Archived.AddRange(archivedMessages);
                dbContext.Messages.RemoveRange(messages);
                chat.isArchived = true;
            }

            await dbContext.SaveChangesAsync();
        }
    }

}

