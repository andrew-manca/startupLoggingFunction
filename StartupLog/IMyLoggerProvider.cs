using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace StartupLog
{
    public interface IMyLoggerProvider
    {
        void TestMethod();
        void WriteLog(string messsage);
        void WriteLogProvider(string logMessage, ExecutionContext context);
    }
}