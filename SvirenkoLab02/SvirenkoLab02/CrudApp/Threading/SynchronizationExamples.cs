using System.Threading;

public class SynchronizationExamples
{
    private readonly object _lockObject = new object();
    private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(2, 2); // Допускає 2 потоки одночасно
    private readonly AutoResetEvent _autoResetEvent = new AutoResetEvent(false); // Працює як турнікет (пропускає по 1)

    // 1. Приклад Lock (Ексклюзивне блокування для синхронного коду)
    public void LockExample()
    {
        lock (_lockObject)
        {
            // Критична секція: Тільки один потік може виконувати цей код одночасно
            Console.WriteLine($"Потік {Thread.CurrentThread.ManagedThreadId} увійшов у lock");
            Thread.Sleep(500);
        }
    }

    // 2. Приклад SemaphoreSlim (Дозволяє обмежену кількість потоків, підтримує async/await)
    public async Task SemaphoreExampleAsync()
    {
        await _semaphore.WaitAsync();
        try
        {
            Console.WriteLine($"Потік {Thread.CurrentThread.ManagedThreadId} працює в Semaphore");
            await Task.Delay(500);
        }
        finally
        {
            _semaphore.Release();
        }
    }

    // 3. Приклад AutoResetEvent (Сигналізування між потоками)
    public void WorkerThread()
    {
        Console.WriteLine("Робочий потік чекає на сигнал...");
        _autoResetEvent.WaitOne(); // Блокується доки не отримає сигнал (Set)
        Console.WriteLine("Робочий потік отримав сигнал і продовжує роботу!");
    }

    public void MainThreadSignals()
    {
        Thread.Sleep(2000); // Імітація роботи
        Console.WriteLine("Головний потік дає сигнал...");
        _autoResetEvent.Set(); // Пропускає ОДИН потік, який очікує на WaitOne()
    }
}