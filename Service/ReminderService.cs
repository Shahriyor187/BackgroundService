using Background.Model;
using MongoDB.Driver;

namespace Background.Service;
public class ReminderService : IHostedService, IDisposable
{
    private Timer _timer;
    private readonly EmailService _emailService;
    private readonly TodoDb _todoDb;

    public ReminderService(TodoDb todoDb, EmailService emailService, Timer timer)
    {
        _todoDb = todoDb;
        _emailService = emailService;
        _timer = timer;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        //Check every 1 minutes
        _timer = new Timer(CheckDeadLines, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    private async void CheckDeadLines(object state)
    {
        //if user's todo deadline is finished and didn't do todo, find 
        var todos = await _todoDb.Todos.Find(t => t.DeadLine < DateTime.UtcNow && !t.Completed).ToListAsync();
        foreach (var todo in todos)
        {
            //For test:
            //Console.WriteLine($"Reminder: Todo with title '{todo.Title}' is due!");
            //Send message to user's Email 
            _emailService.SendEmail(todo.Email);
            var filter = Builders<Todo>.Filter.Eq(t => t.Id, todo.Id);
            await _todoDb.Todos.ReplaceOneAsync(filter, todo);
        }
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}
