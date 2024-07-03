using System;
using System.IO.Pipes;
using System.Threading;
using System.Threading.Tasks;
using Start;

public static class SchedulerProcessorRunner
{
    private static Mutex mutex = new Mutex(true, "{SchedulerProcessorMutex}");
    private static CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

    public static async Task RunAsync(string[] args)
    {
        if (!mutex.WaitOne(TimeSpan.Zero, true))
        {
            Console.WriteLine("Another instance is already running.");
            return;
        }

        try
        {
            bool isRunning = args.Length > 0 && bool.Parse(args[0]);
            Console.WriteLine("Starting main application...");
            var appTask = Task.Run(() => App.Start(isRunning, cancellationTokenSource.Token));
            var shutdownListenerTask = Task.Run(() => ListenForShutdownSignal());

            await Task.WhenAny(appTask, shutdownListenerTask);
        }
        finally
        {
            mutex.ReleaseMutex();
        }
    }

    private static async Task ListenForShutdownSignal()
    {
        using (var server = new NamedPipeServerStream("SchedulerProcessorPipe"))
        {
            await server.WaitForConnectionAsync();

            using (var reader = new StreamReader(server))
            {
                string message = await reader.ReadLineAsync();
                if (message == "shutdown")
                {
                    Console.WriteLine("Shutdown signal received.");
                    cancellationTokenSource.Cancel();
                }
            }
        }
    }
}
