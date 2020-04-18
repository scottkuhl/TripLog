using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TripLog.Models;

namespace TripLog.Services
{
    public interface ITripLogDataService
    {
        Action<string> AuthorizedDelegate { get; set; }

        Action UnauthorizedDelegate { get; set; }

        Task<TripLogEntry> AddEntryAsync(TripLogEntry entry);

        Task AuthenticateAsync(string idProvider, string idProviderToken);

        Task<IList<TripLogEntry>> GetEntriesAsync();

        void Unauthenticate();
    }
}