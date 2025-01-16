# ClearDOTA
ClearDOTA is an application designed to assist players to focus more on game by providing real-time information about **Rune spawns**, **Roshan timers**, and **Roshan's inventory** and **allied buybacks**. It also uses **Text-to-Speech (TTS)** functionality to deliver notifications audibly, ensuring players never miss critical game events.

## Features
+ Tracks **Power Rune** and **Bounty Rune** spawn times and provides reminders.
+ Monitors **Roshan timers** (death, potential respawn window, and actual respawn time).
+ Notifies players about **Roshan's inventory**, including Aghanim’s Shard and Cheese.
+ Uses **Text-to-Speech (TTS)** for real-time audible notifications.
+ Lightweight and efficient CLI design for minimal resource usage.

## Tech Stack
+ **C#**: The primary programming language for the application.
+ **Dota GSI**: Used to fetch in-game data in real time.
+ **System.Speech.Synthesis**: Integrated TTS functionality to deliver notifications audibly.
+ **JSON Configuration**: For GSI configuration and storing application settings.

## Getting Started

### Clone the repository
Clone the repo to get access to the code and provide in you IDE.
```bash
git clone https://github.com/your-username/clear-dota.git
cd clear-dota

### Usage

1. **Set Up Dota 2 GSI**:
   - Navigate to the `gamestate_integration` folder in your Dota 2 directory:
     `Steam\steamapps\common\dota 2 beta\game\dota\cfg\gamestate_integration`.
   - Copy the `gamestate_integration_cleardota.cfg` file from this repository into the folder.

2. **Run the Application**:
   - Open a terminal in the project directory and run:
     ```bash
     dotnet run
     ```
   - Launch Dota 2, and the app will start tracking in-game events.

3. **Real-Time Notifications**:
   - The app will provide audible and on-screen notifications for:
     - **Rune spawns** at exact times.
     - **Roshan respawn windows** and inventory updates.
     - **Allied buybacks** to help with team coordination.

---

## Example Workflow

- Start ClearDOTA before your Dota 2 match.
- During the game, you will receive TTS notifications:
  - “Power Rune has spawned at 6 minutes!”
  - “Roshan may respawn in 2 minutes!”
  - “Allied hero has no buyback!”

---

## Limitations

+ **Enemy buyback** and certain events are not accessible due to Dota 2 GSI limitations.
+ Notifications rely on accurate GSI data and may vary slightly based on system performance.

---

## Future Enhancements

+ Add **customizable notification timings** for different events.
+ Include **visual overlays** to guide players on map events.
+ Extend TTS support for additional in-game events like courier deliveries and item purchases.

---

## You're Ready to **ClearDOTA!**

Elevate your gameplay with real-time assistance and focus entirely on strategy. For feedback or contributions, feel free to open an issue or submit a pull request on the repository.

---
