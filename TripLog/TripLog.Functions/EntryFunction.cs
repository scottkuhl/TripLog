using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace TripLog.Functions
{
    public static class EntryFunction
    {
        public static readonly List<Entry> Items = new List<Entry>
        {
            new Entry {
                Title = "Washington Monument",
                Notes = "Amazing!",
                Rating = 3,
                Date = new DateTime(2019, 2, 5),
                Latitude = 38.8895,
                Longitude = -77.0352
            },
            new Entry
            {
                Title = "Statue of Liberty",
                Notes = "Inspiring!",
                Rating = 4,
                Date = new DateTime(2019, 4, 13),
                Latitude = 40.6892,
                Longitude = -74.0444
            },
            new Entry
            {
                Title = "Golden Gate Bridge",
                Notes = "Foggy, but beautiful.",
                Rating = 5,
                Date = new DateTime(2019, 4, 26),
                Latitude = 37.8268,
                Longitude = -122.4798
            }
        };

        [FunctionName("entry")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation(req.Method);

            if (req.Method == "GET")
            {
                return (ActionResult)new OkObjectResult(Items);
            }

            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var entry = JsonConvert.DeserializeObject<Entry>(requestBody);

            if (entry != null)
            {
                Items.Add(entry);
                return (ActionResult)new OkObjectResult(entry);
            }

            return new BadRequestObjectResult("Invalid entry request.");
        }
    }
}