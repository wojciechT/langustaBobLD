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
            SetResolution(RESOLUTION_WIDTH, RESOLUTION_HEIGHT);
            var screenResolution = new Coordinates(RESOLUTION_WIDTH, RESOLUTION_HEIGHT);
            var palinurusPosition = new Coordinates(0, GetStartingPalinurusTopOffset(RESOLUTION_HEIGHT));
            var palinurus = new Palinurus(palinurusPosition, screenResolution);

            while (true)
            {
                DrawPalinurus(palinurus);

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
                        palinurus.Move(MovementDirection.Right);
                        break;
                    case ConsoleKey.LeftArrow:
                        palinurus.Move(MovementDirection.Left);
                        break;
                    default:
                        Console.WriteLine("Proszę nie pisać głupot.");
                        break;
                }


            }
        }

        private void DrawPalinurus(Palinurus palinurus)
        {
            Console.Clear();
            Console.CursorLeft = palinurus.LeftOffset;
            Console.CursorTop = palinurus.TopOffset;
            Console.Write("O");
        }

        private int GetStartingPalinurusTopOffset(int resolutionHeight) => resolutionHeight / 2;

        private void SetResolution(int resolutionWidth, int resolutionHeight)
        {
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetWindowSize(RESOLUTION_WIDTH+SAFETY_MARGIN, RESOLUTION_HEIGHT);
            Console.SetBufferSize(RESOLUTION_WIDTH+SAFETY_MARGIN, RESOLUTION_HEIGHT);
        }

    }
}
