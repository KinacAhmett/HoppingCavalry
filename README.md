# Hopping Cavalry

A Doodle Jump-style endless platformer built in Unity 6 for **GMTK Game Jam 2025** (theme: *Loop*).
Jump, land, jump again — climb as high as you can before you fall.

🎮 **Play it on itch.io:** [kinacahmet.itch.io/hopping-cavalry](https://kinacahmet.itch.io/hopping-cavalry)

## Features

- **Auto-jumping:** The knight jumps automatically on landing; you only steer left and right.
- **Keyboard and touch controls:** A/D or arrow keys on desktop; tap the left or right half of the screen on mobile.
- **Procedural platforms:** Platforms are generated endlessly as you climb.
- **Height-based difficulty:** Fewer platforms and wider gaps the higher you go.
- **One-way platforms:** Jump through platforms from below, land on them from above.
- **Persistent high score:** Saved locally between sessions.

## Technical Overview

| Script | Responsibility |
|---|---|
| `PlayerController.cs` | Physics-based auto-jump with contact-normal checks, keyboard and touch input via the Input System |
| `PlatformSpawner.cs` | Endless platform generation, height-based difficulty scaling, and cleanup of off-screen platforms |
| `CameraController.cs` | Upward-only camera that follows the player |
| `UI_Manager.cs` | Game states (menu → game → death), score tracking, and high score persistence with `PlayerPrefs` |

## Changelog

**Post-jam update (2026)**
- Rebuilt the full project in Unity 6 and added all project files to the repository.
- Fixed high score tracking: the score now records the highest point reached instead of the height at the moment of death.
- Implemented cleanup of off-screen platforms to keep memory usage constant during long runs.
- Added touch controls for mobile.
- Replaced deprecated APIs (`FindFirstObjectByType` → `FindAnyObjectByType`).

## Getting Started

1. Clone the repository.
2. Open the project in **Unity 6** via Unity Hub.
3. Open `Assets/Scenes/SampleScene` and press Play.

## Credits

- Developed by Ahmet Kinac.
- Art, sounds and fonts: [Brackeys Platformer Bundle](https://brackeysgames.itch.io/brackeys-platformer-bundle) (CC0).