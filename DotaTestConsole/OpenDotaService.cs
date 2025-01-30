using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace DotaTestConsole
{
    public class OpenDotaService
    {
        HttpClient _httpClient = new();
        string BaseUrl = "https://api.opendota.com/api";
        async Task<int> GetMatchDetailsAsync()
        {
            var loses = 0;
            var wins = 0;
            var url = $"{BaseUrl}/heroes/{1}/matches";
            try
            {
                var matches = await _httpClient.GetFromJsonAsync<List<MatchResult>>(url);

                foreach (var match in matches)
                {
                    if ((match.Radiant == true && match.RadiantWin == true) || (match.Radiant == false && match.RadiantWin == false))
                    {
                        wins++;
                    }
                    else
                    {
                        loses++;
                    }
                }
                return wins;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching match data: {ex.Message}");
                return 0;
            }
        }
        public class MatchResult
        {
            public long MatchId { get; set; }
            public bool RadiantWin { get; set; }
            public bool Radiant { get; set; }
        }
    }
}
