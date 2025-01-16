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
var roshan_death_count = 0;
var roshan_dead = false;
var my_buyback_cooldown = 0;
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
    if((gs.Map.ClockTime % 120 == 0) && gs.Map.ClockTime < 300)
    {
        Console.WriteLine("The rune of water appeared");
    }
    if(gs.Map.ClockTime % 420 == 0)
    {
        Console.WriteLine("The run of wisdom appeared");
    }
    if (gs.Map.ClockTime % 180 == 0)
    {
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
            Console.WriteLine($"Roshan might be alive and will drop this items: Aegis OR it will be alive in {roshan_spawn_max_time} seconds for sure");
        }
        else if (roshan_death_count == 1)
        {
            if (gs.Map.IsDaytime)
            {
                Console.WriteLine($"Roshan might be alive and will drop this items: Aegis, Roshan's Banner OR it will be alive in {roshan_spawn_max_time} seconds for sure");
            }
            else
            {
                Console.WriteLine($"Roshan might be alive and will drop this items: Aegis, Cheese OR it will be alive in {roshan_spawn_max_time} seconds for sure");
            }
        }
        else if (roshan_death_count >= 2)
        {
            if (gs.Map.IsDaytime)
            {
                Console.WriteLine($"Roshan might be alive and will drop this items: Aegis, Roshan's Banner, Refresher Shard OR it will be alive in {roshan_spawn_max_time} seconds for sure");
            }
            else
            {
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
    //if(gameEvent is AbilityDetailsChanged abilityDetailsChanged)
    //{
    //    var abis = abilityDetailsChanged.New.Where(x => x.IsUltimate);
    //    foreach (var ability in abis)
    //    {
    //        Console.WriteLine($"{ability.Name} ::: {ability.Cooldown}");
    //    }
    //}

}