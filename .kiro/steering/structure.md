# Project Structure

```
learn-structured-programming/
├── Program.cs                    # Entry point with menu selection
├── learn-structured-programming.csproj
├── .editorconfig                 # Code style rules
├── .devcontainer/                # DevContainer configuration
└── src/
    ├── Section00_UnstructuredProgramming/   # Intro: global vars, procedural
    ├── Section01_UnstructuredProgramming/   # goto statements, graphics
    ├── Section02_StructuredProgramming/     # Loops, conditionals, static functions
    ├── Section03_StructuredProgrammingPlus/ # 2D extension
    ├── Section04_ObjectOrientedProgramming/ # Clean architecture, SOLID
    └── Section05_ObjectOrientedProgrammingPlus/ # Extended OOP with state pattern
```

## OOP Sections Architecture (Section04-05)

Clean architecture with 5 layers:

```
Core/           # Value objects: Position, Direction, Bounds, GameSettings
Domain/         # Business logic
  ├── Entities/     # Entity, Player, Enemy (+ Lizard, Tail in Section05)
  ├── Behaviors/    # IMovementBehavior implementations (Strategy pattern)
  └── Events/       # Game events
Application/    # Use cases
  ├── Interfaces/   # IGameRenderer, IInputHandler, IGameClock
  └── Services/     # GameLoopService, GameFactory
Infrastructure/ # External systems
  ├── Input/        # ConsoleInputHandler
  └── Timing/       # SystemGameClock
Presentation/   # UI
  └── Console/      # ConsoleGameRenderer
```

## Naming Conventions & Code Style

**Follow `.editorconfig` for all naming conventions and code style rules.**
