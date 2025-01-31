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
            { 22,"Zeus" },
            { 23,"Kunkka" },
            { 24,""},
            { 25,"Lina" },
            { 26,"Lion" },
            { 27,"Shadow-Shaman" },
            { 28,"Slardar"},
            { 29,"Tidehunter"},
            { 30,"Witch-Doctor"},
            { 31,"Lich"},
            { 32,"Riki"},
            { 33,"Enigma"},
            { 34,"Tinker"},
            { 35,"Sniper"},
            { 36,"Necromancer"},
            { 37,"Warlock"},
            { 38,"Beastmaster"},
            { 39,"Queen-Of-Pain"},
            { 40,"Venomancer"},
            { 41,"Faceless-Void"},
            { 42,"Wraith-King"},
            { 43,"Death-Prophet"},
            { 44,"Phantom-Assassin"},
            { 45,"Pugna"},
            { 46,"Templar-Assassin"},
            { 47,"Viper"},
            { 48,"Luna"},
            { 49,"Dragon-Knight"},
            { 50,"Dazzle"},
            { 51,"Clockwerk"},
            { 52,"Leshrac"},
            { 53,"Nature's-Prophet"},
            { 54,"Lifestealer"},
            { 55,"Nyx"},
            { 56,"Clinkz"},
            { 57,"OmniKnight"},
            { 58,"Enchantress"},
            { 59,"Huskar"},
            { 60,"Night-Stalker"},
            { 61,"Broodmother"},
            { 62,"Bounty-Hunter"},
            { 63,"Weaver"},
            { 64,"Jakiro"},
            { 65,"Batrider"},
            { 66,"Chen"},
            { 67,"Spectre"},
            { 68,"Ancient-Apparition"},
            { 69,"Doom"},
            { 70,"Ursa"},
            { 71,"Spirit-Breaker"},
            { 72,"Gyrocopter"},
            { 73,"Alchemist"},
            { 74,"Invoker"},
            { 75,"Silencer"},
            { 76,"Outworld-Destroyer"},
            { 77,"Lycan"},
            { 78,"Brewmaster"},
            { 79,"Shadow-Demon"},
            { 80,"Lone-Druid"},
            { 81,"Chaos-Knight"},
            { 82,"Meepo"},
            { 83,"Treant-Protector"},
            { 84,"Ogre-Magi"},
            { 85,"Undying"},
            { 86,"Rubick"},
            { 87,"Disrupter"},
            { 88,"Nyx"},
            { 89,"Silencer"},
            { 90,"Silencer"},
            { 91,"Silencer"},
            { 92,"Silencer"},
            { 93,"Silencer"},
            { 94,"Silencer"},
            { 95,"Silencer"},
            { 96,"Silencer"},
            { 97,"Silencer"},
            { 98,"Silencer"},
            { 99,"Silencer"},
            { 100,"Silencer"},
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
