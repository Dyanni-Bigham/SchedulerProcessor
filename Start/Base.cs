using System;
using System.Threading;
using Log;

namespace Start
{
    public class Base
    {
        static async Task Main(string[] args)
        {
            await SchedulerProcessorRunner.RunAsync(args);
        }
    }
}