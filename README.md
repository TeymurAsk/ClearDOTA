# ClearDOTA
ClearDOTA is an application designed to assist players to focus more on game by providing real-time information about **Rune spawns**, **Roshan timers**, and **Roshan's inventory** and **allied buybacks**. It also uses **Text-to-Speech (TTS)** functionality to deliver notifications audibly, ensuring players never miss critical game events.

## Features
+ Tracks Power Rune and Bounty Rune spawn times and provides reminders.
+ Monitors Roshan timers (death, potential respawn window, and actual respawn time).
+ Notifies players about Roshan's inventory, including Aghanim’s Shard and Cheese.
+ Uses Text-to-Speech (TTS) for real-time audible notifications.
+ Lightweight and efficient CLI design for minimal resource usage.

## Tech Stack
+ **C#**: The primary programming language for the application.
+ **Dota GSI**: Used to fetch in-game data in real time.
+ **System.Speech.Synthesis**: Integrated TTS functionality to deliver notifications audibly.
+ **JSON Configuration**: For GSI configuration and storing application settings.

## Getting Started

### Clone the repository
Clone the repo to get access to the code and provide in you IDE.

### Usage
1. Configure Dota 2 for GSI:
+ Locate the gamestate_integration folder in your Dota 2 directory: Steam\steamapps\common\dota 2 beta\game\dota\cfg\gamestate_integration.
+ Copy the provided gamestate_integration_dotahelper.cfg file from the repository into this folder.
