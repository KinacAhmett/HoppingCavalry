# KnightJump - Doodle Jump Clone

A 2D endless jumping game built in Unity, inspired by the classic Doodle Jump mechanics. Players control a character that automatically jumps on platforms while climbing infinitely upward.

## 🎮 Game Features

- **Auto-Jumping Mechanics**: Character automatically jumps when landing on platforms
- **Horizontal Movement**: Control left/right movement using arrow keys or A/D
- **Infinite Vertical Scrolling**: Endless upward gameplay with procedural platform generation
- **Progressive Difficulty**: Platform spacing and count dynamically adjust based on height
- **Score System**: Track your highest score with persistent high score storage
- **Death & Respawn**: Fall below camera view to restart from the beginning

## 🛠 Technical Implementation

### Core Scripts

#### `PlayerController.cs`
- Physics-based auto-jumping using Unity's new Input System
- Horizontal movement with `Rigidbody2D.linearVelocity`
- Collision detection via `OnCollisionEnter2D` with contact normal validation
- Death detection when falling below camera bounds

#### `PlatformSpawner.cs`
- Procedural infinite platform generation system
- Dynamic difficulty scaling based on camera height
- Platform count reduction: 7→6→5→4→3 as height increases
- Intelligent spawn distance calculation to maintain playability
- Platform cleanup system for performance optimization

#### `CameraController.cs`
- Upward-only camera follow system
- Smooth tracking of player Y position
- Prevents downward camera movement to maintain challenge

#### `UI_Manager.cs`
- Game state management (Menu → Game → Death)
- Real-time score calculation based on height achieved
- High score persistence using `PlayerPrefs`
- UI panel management to prevent interaction conflicts

## 🎯 Gameplay Mechanics

### Difficulty Progression
- **Height 0-100**: Standard 7 platforms per spawn group
- **Height 100-200**: Reduced to 6 platforms
- **Height 200-300**: Reduced to 5 platforms
- **Height 300-400**: Reduced to 4 platforms
- **Height 400+**: Minimum 3 platforms

### Platform Spawning
- **Base Distance**: 1.2 units between platforms
- **Dynamic Scaling**: Distance increases with height (capped at 500+ height)
- **Horizontal Variation**: Platform X positions become more constrained at higher levels
- **Randomization**: 70-100% variation in spawn distances for unpredictability

## 🚀 Setup Instructions

1. **Clone Repository**
   ```bash
   git clone [repository-url]
   cd KnightJump
   ```

2. **Open in Unity**
   - Unity 2022.3 LTS or newer recommended
   - Import project into Unity Hub

3. **Configure Scene**
   - Assign platform prefab to `PlatformSpawner`
   - Set up player character with `PlayerController` script
   - Configure camera with `CameraController`
   - Set up UI canvas with `UI_Manager`

4. **Input System Setup**
   - Ensure Unity Input System package is installed
   - Configure horizontal movement input actions

## 📁 Project Structure

```
Assets/
└── Scripts/
    ├── PlayerController.cs      # Player physics and movement
    ├── PlatformSpawner.cs       # Infinite platform generation
    ├── CameraController.cs      # Camera follow logic
    └── UI_Manager.cs           # Game state and UI management
```

## 🎨 Assets Not Included

This repository contains only the core game scripts. You'll need to provide your own:
- Sprite graphics (player character, platforms, backgrounds)
- Audio files (jump sounds, background music)
- UI elements (buttons, panels, fonts)
- Materials and visual effects

### Recommended Asset Pack
For quick setup, you can use the **Brackeys Platformer Bundle**:
- **Download**: [https://brackeysgames.itch.io/brackeys-platformer-bundle](https://brackeysgames.itch.io/brackeys-platformer-bundle)
- Includes sprites, tiles, and audio suitable for 2D platformer games
- Free download with high-quality pixel art assets

## 🔧 Key Features for Developers

- **Modular Architecture**: Each script handles a specific game system
- **Scalable Difficulty**: Easy to adjust progression parameters
- **Performance Optimized**: Automatic cleanup of off-screen platforms
- **Input System Integration**: Uses Unity's modern Input System
- **Physics-Based**: Realistic jump mechanics using Unity's physics

## 📝 Development Notes

- Built with Unity's new Input System for cross-platform compatibility
- Uses `Rigidbody2D.linearVelocity` (updated from deprecated `velocity`)
- Implements safe reference handling to prevent `MissingReferenceException`
- Balances challenge progression to maintain playability at higher levels

## 🎖 Credits

Developed as a learning project exploring Unity 2D physics, procedural generation, and game state management.

---

*Ready to jump? Clone the repo and start building your own endless jumping adventure!*
