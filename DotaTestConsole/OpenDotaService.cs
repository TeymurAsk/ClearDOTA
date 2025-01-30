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
        Dictionary<int, string> heroes = new()
        {
            { 1,"Anti-Mage" },
            { 2,"Axe" },
            { 3,"Bane" },
            { 4,"Bloodseeker" },
            { 5,"Crystal-Maiden" },
            { 6,"Drow-Ranger" },
            { 7,"Earth-Shaker" },
            { 8,"Juggernaught" },
            { 9,"Mirana" },
            { 10,"Morphling" },
            { 11,"Shadow-Fiend" },
            { 12,"Phantom-Lancer" },
            { 13,"Puck" },
            { 14,"Pudge" },
            { 15,"Razor" },
            { 16,"Sand-King" },
            { 17,"Storm-Spirit" },
            { 18,"Sven" },
            { 19,"Tiny" },
            { 20,"Vengeful-Spirit" },
            { 21,"Windranger" },
            { 22,"" },
            { 23,"Anti-mage" },
            { 24,"Anti-mage" },
            { 25,"Anti-mage" },
            { 26,"Anti-mage" },
            { 27,"Anti-mage" },
            { 28,"Anti-mage" },
        };
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
