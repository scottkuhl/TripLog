using System;

namespace TripLog.Models
{
    public class TripLogEntry
    {
        public DateTime Date { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Notes { get; set; }
        public int Rating { get; set; }
        public string Title { get; set; }
    }
}