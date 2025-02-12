using ChatProject.Models;

namespace ChatProject.Data
{
    //public class ChatArchiverService : BackgroundService
    //{
        //private readonly IServiceScopeFactory _scopeFactory; //scope фабрика для використання dbContext
        //private readonly TimeSpan _checkInterval = TimeSpan.FromSeconds(5); // Перевірка кожні 10c


        //public ChatArchiverService(IServiceScopeFactory scopeFactory)
        //{
        //    _scopeFactory = scopeFactory;
        //}

        ///// <summary>
        ///// Основний цикл виконання сервісу
        ///// </summary>
        //protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        //{
        //    while (!stoppingToken.IsCancellationRequested)
        //    {
        //        using (var scope = _scopeFactory.CreateScope()) // щоразу створюється новоа область сервісів для отримання dbContext
        //        {
        //            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        //            await ArchiveExpiredChats(dbContext);
        //        }
        //        await Task.Delay(_checkInterval, stoppingToken);
        //    }
        //}

        ///// <summary>
        ///// Метод для автоматичної архівації чатів, які вже прострочені
        ///// </summary>
        //private async Task ArchiveExpiredChats(ApplicationDbContext dbContext)
        //{
        //    var now = DateTime.UtcNow;
        //    // Отримуємо всі чати, які не в архіві та прострочені
        //    var expiredChats = dbContext.Chats
        //        .Where(c => !c.isArchived &&
        //            (c.Expiration.HasValue && c.Expiration <= now) ||  // Чат має вказаний час архівації
        //            (!c.Expiration.HasValue && c.CreationTime.AddDays(14) <= now)) // Автоархівація через 14 днів
        //        .ToList();

        //    //отримуємо всі повідмолення і переносимо їхв таблицю архівних повідомлень
        //    foreach (var chat in expiredChats)
        //    {
        //        var messages = dbContext.Messages.Where(m => m.ChatId == chat.Id).ToList();

        //        var archivedMessages = messages.Select(m => new Archived
        //        {
        //            ChatId = m.ChatId,
        //            UserId = m.UserId,
        //            Content = m.Content,
        //            SentTime = m.SentTime
        //        }).ToList();

        //        dbContext.Archived.AddRange(archivedMessages); // Додаємо всі повідомлення до таблиці Archived
        //        dbContext.Messages.RemoveRange(messages); //видаляємр з таблиці повідмолень
        //        chat.isArchived = true;
        //    }
        //    await dbContext.SaveChangesAsync();
        //}
    //}

}

