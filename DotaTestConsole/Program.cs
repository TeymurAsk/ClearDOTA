using Dota2GSI;
using Dota2GSI.EventMessages;
using Dota2GSI.Nodes;
using Dota2GSI.Nodes.EventsProvider;

GameStateListener gsl = new GameStateListener(3000);

// GLOBAL Variables
var roshan_death = 0; 


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
    Console.WriteLine($"Roshan health: {gs.Roshan.Health}");
    foreach(var gameevent in gs.Events)
    {
        if (gameevent.EventType == EventType.Roshan_killed)
        {
            roshan_death = gs.Map.ClockTime;  
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