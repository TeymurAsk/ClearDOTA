using Dota2GSI;
using Dota2GSI.EventMessages;
using Dota2GSI.Nodes;
using Dota2GSI.Nodes.EventsProvider;
using System.Linq;

GameStateListener gsl = new GameStateListener(3000);

// GLOBAL Variables
var roshan_death_time = 0;
var roshan_spawn_min_time = 0;
var roshan_spawn_max_time = 0;
var roshan_dead = false;

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
    if (roshan_spawn_max_time == 0 && roshan_dead == false)
    {
        Console.WriteLine($"Roshan is still alive and will drop: {gs.Roshan.Drops.Items}");
    }
    else if (roshan_spawn_min_time == 0 && roshan_spawn_max_time != 0)
    {
        Console.WriteLine($"Roshan might be alive and will drop this items: {gs.Roshan.Drops.Items} OR it will be alive in {roshan_spawn_max_time} seconds for sure");
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
        }
    }
}
void OnGameEvent(DotaGameEvent gameEvent)
{
    if (gameEvent is TimeOfDayChanged tod_changed)
    {
        Console.WriteLine($"Is daytime: {tod_changed.IsDaytime} Is Nightstalker night: {tod_changed.IsNightstalkerNight}");
    }
    if (gameEvent is AbilityUpdated abilityUpdated)
    {
        if (abilityUpdated.New.Level > abilityUpdated.Previous.Level)
        {
            Console.WriteLine($"The ability {abilityUpdated.New.Name} is upgraded to level {abilityUpdated.New.Level}");
        }
        else
        {
            Console.WriteLine($"The ability {abilityUpdated.New.Name} is in cooldown for {abilityUpdated.New.Cooldown} seconds");
        }
    }    
}