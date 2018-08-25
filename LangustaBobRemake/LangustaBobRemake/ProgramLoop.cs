using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace LangustaBobRemake
{
    public enum MovementDirection
    {
        Right,
        Left,
        Down,
        Up,
    }

    internal class Coordinates
    {
        public int Left { get; }
        public int Top { get; }
        public Coordinates(int left, int top)
        {
            Left = left;
            Top = top;
        }
    }

    class ProgramLoop
    {
        private const int RESOLUTION_WIDTH = 120;
        private const int RESOLUTION_HEIGHT = 40;
        private const int SAFETY_MARGIN = 1;

        public ProgramLoop()
        {
        
        }
        public void Run()
        {
            var exit = false;
            SetResolution(RESOLUTION_WIDTH, RESOLUTION_HEIGHT);
            var screenResolution = new Coordinates(RESOLUTION_WIDTH, RESOLUTION_HEIGHT);

            var gameState = new GameState(screenResolution);
            var palinurus = gameState.Palinurus;

            while (!exit)
            {
                //Console.Beep(37, 300);
                //Console.Beep(200, 300);
                //Console.Beep(440, 300);
                
                DrawWorld(palinurus, gameState);
                var keyPressed = Console.ReadKey(true);
                switch (keyPressed.Key)
                {
                    case ConsoleKey.DownArrow:
                        palinurus.Move(MovementDirection.Down);
                        break;
                    case ConsoleKey.UpArrow:
                        palinurus.Move(MovementDirection.Up);
                        break;
                    case ConsoleKey.RightArrow:
                        if (gameState.PalinurusCanMove(MovementDirection.Right)) palinurus.Move(MovementDirection.Right);
                        Console.WriteLine(gameState.PalinurusCanMove(MovementDirection.Right));
                        break;
                    case ConsoleKey.LeftArrow:
                        if (gameState.PalinurusCanMove(MovementDirection.Right)) palinurus.Move(MovementDirection.Left);
                        Console.WriteLine(gameState.PalinurusCanMove(MovementDirection.Left));
                        break;
                    case ConsoleKey.Escape:
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Proszę nie pisać głupot.");
                        break;
                }
            }
        }

        private void DrawWorld(Palinurus palinurus, GameState gameState)
        {
            Console.Clear();
            gameState.Draw();
            Console.CursorLeft = palinurus.LeftOffset;
            Console.CursorTop = palinurus.TopOffset;
            foreach (var line in palinurus.CharacterRepresentation)
            {
                Console.Write($"{line}\n");
                Console.CursorLeft = palinurus.LeftOffset;
            }
        }

        private void SetResolution(int resolutionWidth, int resolutionHeight)
        {
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.SetWindowSize(resolutionWidth + SAFETY_MARGIN, resolutionHeight);
            Console.SetBufferSize(resolutionWidth + SAFETY_MARGIN, resolutionHeight);
        }

    }
}
