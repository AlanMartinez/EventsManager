using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Hosting;
using System;

namespace ManagerAzureFunctions
{
    internal class Program
    {
        static void Main(string[] args)
        {
            FunctionsDebugger.Enable();

            Environment.SetEnvironmentVariable("ASPNETCORE_URLS", "http://localhost:7173");
            // Forzar que el host gRPC use "localhost"
            Environment.SetEnvironmentVariable("FUNCTIONS_WORKER_GRPC_HOST", "localhost");

            var host = new HostBuilder()
                .ConfigureFunctionsWorkerDefaults()

                .Build();

            host.Run();
        }
    }
}
