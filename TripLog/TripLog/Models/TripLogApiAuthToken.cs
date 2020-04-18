namespace TripLog.Models
{
    public class TripLogApiAuthToken
    {
        public string AuthenticationToken { get; set; }
        public TripLogApiUser User { get; set; }
    }

    public class TripLogApiUser
    {
        public string UserId { get; set; }
    }
}