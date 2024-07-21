
# Memory Game

A C# console application implementing a Memory Game where players flip over pairs of cards to find matching pairs. This project showcases object-oriented programming principles and basic game logic.

## Table of Contents

- [Description](#description)
- [Installation](#installation)
- [Usage](#usage)
- [File Structure](#file-structure)
- [Classes and Methods](#classes-and-methods)

## Description

The Memory Game is a console-based game where players take turns flipping over cards to find matching pairs. The game keeps track of the players' scores and declares the winner at the end of the game.

## Installation

To run this project, you need to have .NET installed on your machine. You can download it from the official [.NET website](https://dotnet.microsoft.com/download).

1. Clone the repository:
   ```sh
   git clone https://github.com/yourusername/MemoryGame.git
   ```

2. Navigate to the project directory:
   ```sh
   cd MemoryGame
   ```

3. Build the project:
   ```sh
   dotnet build
   ```

## Usage

Run the project using the following command:
```sh
dotnet run
```

Follow the on-screen instructions to play the game.

## File Structure

- **Card.cs**: Represents a card in the memory game.
- **ComputerPlayer.cs**: Implements logic for a computer player.
- **ConsoleRender.cs**: Handles the rendering of the game board to the console.
- **GameBoard.cs**: Represents the game board and manages card positions.
- **GameEngine.cs**: Core game logic and flow control.
- **GameManager.cs**: Manages the overall game state and player turns.
- **MemoryGame.csproj**: Project file for the Memory Game.
- **MemoryGame.sln**: Solution file for the Memory Game.
- **MemoryGameUtils.cs**: Utility functions used throughout the game.
- **Player.cs**: Represents a human player.
- **Program.cs**: Entry point of the application.
- **Runner.cs**: Initializes and starts the game.
- **eBoardMapper.cs**: Maps board positions to card values.
- **eCardMarks.cs**: Enumeration for card marks.
- **ePlayerType.cs**: Enumeration for player types.

## Classes and Methods

### Card.cs
Represents a card with properties such as `Value` and `IsFlipped`.

### ComputerPlayer.cs
Implements logic for a computer-controlled player, inheriting from `Player`.

### ConsoleRender.cs
Handles rendering the game board to the console.

### GameBoard.cs
Manages the game board, card positions, and flipping logic.

### GameEngine.cs
Contains the main game loop and game logic.

### GameManager.cs
Tracks game state, player turns, and scores.

### MemoryGameUtils.cs
Utility functions for various game tasks.

### Player.cs
Represents a human player with properties like `Name` and `Score`.

### Program.cs
The main entry point of the application.

### Runner.cs
Initializes game components and starts the game.

### eBoardMapper.cs
Maps board positions to card values for rendering.

### eCardMarks.cs
Enumeration for different card marks used in the game.

### ePlayerType.cs
Enumeration for different player types (Human, Computer).
