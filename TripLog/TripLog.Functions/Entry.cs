using System;

namespace TripLog.Functions
{
    public class Entry
    {
        public DateTime Date { get; set; }
        public string Id => Guid.NewGuid().ToString("n");
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Notes { get; set; }
        public int Rating { get; set; }
        public string Title { get; set; }
    }
}