using System.Collections.Generic;
using System.Threading.Tasks;
using TripLog.Models;

namespace TripLog.Services
{
    public interface ITripLogDataService
    {
        Task<TripLogEntry> AddEntryAsync(TripLogEntry entry);

        Task<IList<TripLogEntry>> GetEntriesAsync();
    }
}