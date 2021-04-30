using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AlaMaKota
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string str1 = req.Query["str1"];
            string str2 = req.Query["str2"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data1 = JsonConvert.DeserializeObject(requestBody);
            str1 = str1 ?? data1?.str1;

            dynamic data2 = JsonConvert.DeserializeObject(requestBody);
            str2 = str2 ?? data2?.str2;

            string responseMessage = (string.IsNullOrEmpty(str1)  && string.IsNullOrEmpty(str2))
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Zwrocone: {str1}{str2}.";

            return new OkObjectResult(responseMessage);
        }
    }
}
