using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace TripLog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EntryController : ControllerBase
    {
        private static List<Entry> _entries = new List<Entry>();

        private readonly ILogger<EntryController> _logger;

        static EntryController()
        {
            _entries.Add(new Entry
            {
                Title = "Washington Monument",
                Notes = "Amazing!",
                Rating = 3,
                Date = new DateTime(2019, 2, 5),
                Latitude = 38.8895,
                Longitude = -77.0352
            });

            _entries.Add(new Entry
            {
                Title = "Statue of Liberty",
                Notes = "Inspiring!",
                Rating = 4,
                Date = new DateTime(2019, 4, 13),
                Latitude = 40.6892,
                Longitude = -74.0444
            });

            _entries.Add(new Entry
            {
                Title = "Golden Gate Bridge",
                Notes = "Foggy, but beautiful.",
                Rating = 5,
                Date = new DateTime(2019, 4, 26),
                Latitude = 37.8268,
                Longitude = -122.4798
            });
        }

        public EntryController(ILogger<EntryController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Entry> Get()
        {
            return _entries.ToArray();
        }

        [HttpPost]
        public ActionResult Post(Entry entry)
        {
            _entries.Add(entry);
            return Ok(entry);
        }
    }
}