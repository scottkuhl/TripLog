using System;

namespace TripLog.API
{
    public class Entry
    {
        public string Id => Guid.NewGuid().ToString("n");
        public string Title { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime Date { get; set; }
        public int Rating { get; set; }
        public string Notes { get; set; }
    }
}
