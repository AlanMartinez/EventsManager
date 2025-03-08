using ManagerAzureFunctions.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.IO;
using System.Text;
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
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequestData req, FunctionContext executionContext)
        {
            string requestBody;
            using (var reader = new StreamReader(req.Body, Encoding.UTF8))
            {
                requestBody = await reader.ReadToEndAsync();
            }
            var eventData = JsonConvert.DeserializeObject<EventCreateModel>(requestBody);

            return new OkResult();
        }
    }
}
