using Dota2GSI;
using Dota2GSI.EventMessages;
using Dota2GSI.Nodes;
using Dota2GSI.Nodes.EventsProvider;
using DotaTestConsole;
using System.Linq;
using System.Speech.Synthesis;


htmlparser.FetchMetaHeroesAfterButtonClick();
GameStateListener gsl = new GameStateListener(3000);
SpeechSynthesizer synthesizer = new SpeechSynthesizer();
synthesizer.Volume = 100; // Volume (0-100)
synthesizer.Rate = 0; // Speaking rate (-10 to 10)
// GLOBAL Variables
var roshan_death_time = 0;
var roshan_spawn_min_time = 0;
var roshan_spawn_max_time = 0;
var roshan_death_count = 0;
var roshan_dead = false;
var my_buyback_cooldown = 0;
var heroRecommendations = new Dictionary<string, List<string>>
{
    { "1", new List<string> { "Phantom Assassin", "Lifestealer", "Juggernaut", "Terrorblade", "Drow Ranger" } },
    { "2", new List<string> { "Storm Spirit", "Invoker", "Shadow Fiend", "Tinker", "Puck" } },
    { "3", new List<string> { "Tidehunter", "Centaur Warrunner", "Dark Seer", "Beastmaster", "Bristleback" } },
    { "5", new List<string> { "Crystal Maiden", "Lion", "Dazzle", "Disruptor", "Warlock" } },
    { "4", new List<string> { "Earth Spirit", "Tusk", "Rubick", "Mirana", "Clockwerk" } }
};
// Generate the GSI configuration file if it doesn't exist
if (!gsl.GenerateGSIConfigFile("Example"))
{
    Console.WriteLine("Could not generate GSI configuration file. Exiting.");
    return;
}

// Subscribe to events
gsl.GameEvent += OnGameEvent;
gsl.NewGameState += OnNewGameState;

// Start listening for game state updates
if (!gsl.Start())
{
    Console.WriteLine("GameStateListener could not start. Try running as Administrator. Exiting.");
    return;
}

// Keep the program running to process events
Console.WriteLine("Listening for Dota 2 game state updates...");
Console.WriteLine("Please ensure Dota 2 is running and Game State Integration is enabled.");
Console.WriteLine("Press Ctrl+C to exit.");
while (true)
{
    // Keep the console alive
    System.Threading.Thread.Sleep(1000);
}

// Callback: General game state updates
void OnNewGameState(GameState gs)
{
    // Ensure game state is valid
    if (gs == null || gs.Provider == null || gs.Player == null)
    {
        Console.WriteLine("Waiting for game state data...");
        return;
    }
    Console.Clear();
    Console.WriteLine($"Your Steam name: {gs.Player.LocalPlayer.Name}");
    Console.WriteLine($"Playing Team: {gs.Player.LocalPlayer.Team}");
    Console.WriteLine($"Radiant Score: {gs.Map.RadiantScore}, Dire Score: {gs.Map.DireScore}");
    Console.WriteLine($"Game Clock Time: {gs.Map.ClockTime} seconds");
    Console.WriteLine($"Hero's buyback cd: {gs.Hero.LocalPlayer.BuybackCooldown}");
    if((gs.Map.ClockTime % 120 == 0) && gs.Map.ClockTime < 300 && gs.Map.ClockTime>10)
    {
        synthesizer.Speak("The rune of water appeared!");
        Console.WriteLine("The rune of water appeared");
    }
    else if((gs.Map.ClockTime % 120 == 0) && gs.Map.ClockTime > 300 && gs.Map.ClockTime > 10)
    {
        synthesizer.Speak("The power rune appeared!");
        Console.WriteLine("The power rune appeared");
    }
    if (gs.Map.ClockTime % 420 == 0 && gs.Map.ClockTime > 10)
    {
        synthesizer.Speak("The run of wisdom appeared!");
        Console.WriteLine("The run of wisdom appeared");
    }
    if (gs.Map.ClockTime % 180 == 0 && gs.Map.ClockTime > 10)
    {
        synthesizer.Speak("The bounty and healing lotus just appeared!");
        Console.WriteLine("The bounty and healing lotus just appeared");
    }
    if (my_buyback_cooldown > 0)
    {
        my_buyback_cooldown--;
    }
    if (roshan_spawn_max_time == 0 && roshan_dead == false)
    {
        if (roshan_death_count == 0)
        {
            Console.WriteLine("Roshan is still alive and will drop: Aegis");
        }
        else if(roshan_death_count == 1)
        {
            if (gs.Map.IsDaytime)
            {
                Console.WriteLine("Roshan is still alive and will drop: Aegis, Roshan's Banner");
            }
            else
            {
                Console.WriteLine("Roshan is still alive and will drop: Aegis, Cheese");
            }
        }
        else if(roshan_death_count >= 2)
        {
            if (gs.Map.IsDaytime)
            {
                Console.WriteLine("Roshan is still alive and will drop: Aegis, Roshan's Banner, Refresher Shard");
            }
            else
            {
                Console.WriteLine("Roshan is still alive and will drop: Aegis, Cheese, Aghanim's Blessing");
            }
        }
    }
    else if (roshan_spawn_min_time == 0 && roshan_spawn_max_time != 0)
    {
        if (roshan_death_count == 0)
        {
            synthesizer.Speak("Roshan might be alive!");
            Console.WriteLine($"Roshan might be alive and will drop this items: Aegis OR it will be alive in {roshan_spawn_max_time} seconds for sure");
        }
        else if (roshan_death_count == 1)
        {
            if (gs.Map.IsDaytime)
            {
                synthesizer.Speak("Roshan might be alive!");
                Console.WriteLine($"Roshan might be alive and will drop this items: Aegis, Roshan's Banner OR it will be alive in {roshan_spawn_max_time} seconds for sure");
            }
            else
            {
                synthesizer.Speak("Roshan might be alive!");
                Console.WriteLine($"Roshan might be alive and will drop this items: Aegis, Cheese OR it will be alive in {roshan_spawn_max_time} seconds for sure");
            }
        }
        else if (roshan_death_count >= 2)
        {
            if (gs.Map.IsDaytime)
            {
                synthesizer.Speak("Roshan might be alive!");
                Console.WriteLine($"Roshan might be alive and will drop this items: Aegis, Roshan's Banner, Refresher Shard OR it will be alive in {roshan_spawn_max_time} seconds for sure");
            }
            else
            {
                synthesizer.Speak("Roshan might be alive!");
                Console.WriteLine($"Roshan might be alive and will drop this items: Aegis, Cheese, Aghanim's Blessing OR it will be alive in {roshan_spawn_max_time} seconds for sure");
            }
        }
        roshan_spawn_max_time--;
    }
    else
    {
        Console.WriteLine($"Roshan is dead and gonna be revived in a period from {roshan_spawn_min_time} to {roshan_spawn_max_time} seconds");
        roshan_spawn_max_time--;
        roshan_spawn_min_time--;
    }
    if (roshan_spawn_min_time == 0)
    {
        roshan_dead = false;
    }
    foreach(var gameevent in gs.Events)
    {
        if (gameevent.EventType == EventType.Roshan_killed && !roshan_dead) 
        {
            roshan_death_time = gs.Map.ClockTime;
            roshan_spawn_min_time += 480;
            roshan_spawn_max_time += 660;
            roshan_dead = true;
            roshan_death_count++;
        }
    }
}
void OnGameEvent(DotaGameEvent gameEvent)
{
    if (gameEvent is TimeOfDayChanged tod_changed)
    {
        Console.WriteLine($"Is daytime: {tod_changed.IsDaytime} Is Nightstalker night: {tod_changed.IsNightstalkerNight}");
    }
}