using ManagerAzureFunctions.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace ManagerAzureFunctions.Functions
{
    public class EventFunctions
    {
        private readonly ILogger _logger;

        public EventFunctions(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<EventFunctions>();
        }

        [Function("EventCreatedFunction")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req, ILogger log)
        {
            log.LogInformation("Se disparó la función EventCreatedFunction.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var eventData = JsonConvert.DeserializeObject<EventCreateModel>(requestBody);

            return new OkResult();
        }
    }
}
