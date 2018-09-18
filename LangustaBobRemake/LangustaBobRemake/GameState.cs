using System;
using System.Collections.Generic;
using System.Text;

namespace LangustaBobRemake
{
    public enum ObjectCategory {
        Enemy,
        Empowerment,
        DumbStand,
        DeepSeaDepths
    }

    class GameState
    {
        private GameMap _gameMap;
        public Palinurus Palinurus;

        public GameState(Coordinates screenResolution)
        {
            _gameMap = new GameMap();
            var palinurusPosition = new Coordinates(0, GetStartingPalinurusTopOffset(screenResolution.Top));
            Palinurus = new Palinurus(palinurusPosition, screenResolution);
            var wallOnecoordinates = new Coordinates(10, 20);
            var wallOne = new Wall(wallOnecoordinates);
            _gameMap.CurrentMap[wallOnecoordinates] = wallOne;
        }

        public void Draw()
        {
            foreach (var entry in _gameMap.CurrentMap)
            {
                Console.CursorLeft = entry.Value.Position.Left;
                Console.CursorTop = entry.Value.Position.Top;
                foreach (var line in entry.Value.CharacterRepresentation)
                {
                    Console.Write($"{line}\n");
                    Console.CursorLeft = entry.Value.Position.Left;
                }

            }
        }

        public bool PalinurusCanMove(MovementDirection direction)
        {
            switch (direction)
            {
                case MovementDirection.Right:
                    for (var i = 0; i < 5; i++)
                    {
                        if (!_gameMap.CurrentMap.ContainsKey(new Coordinates(Palinurus.LeftOffset + 5,
                            Palinurus.TopOffset - 2 + i))) continue;                        
                        if (_gameMap.CurrentMap[
                                new Coordinates(Palinurus.LeftOffset + 5, Palinurus.TopOffset - 2 + i)].Category ==
                            ObjectCategory.DumbStand)
                        {
                            return false;
                        }
                    }
                    break;
                case MovementDirection.Left:
                    for (var i = 0; i < 5; i++)
                    {
                        if (!_gameMap.CurrentMap.ContainsKey(new Coordinates(Palinurus.LeftOffset - 5,
                            Palinurus.TopOffset - 2 + i))) continue;
                        if (_gameMap.CurrentMap[
                                new Coordinates(Palinurus.LeftOffset - 5, Palinurus.TopOffset - 2 + i)].Category ==
                            ObjectCategory.DumbStand)
                        {
                            return false;
                        }
                    }
                    break;
                case MovementDirection.Down:
                    for (var i = 0; i < 9; i++)
                    {
                        if (!_gameMap.CurrentMap.ContainsKey(new Coordinates(Palinurus.LeftOffset - 4 + i,
                            Palinurus.TopOffset + 3))) continue;
                        if (_gameMap.CurrentMap[
                                new Coordinates(Palinurus.LeftOffset - 4 + i, Palinurus.TopOffset + 3)].Category ==
                            ObjectCategory.DumbStand)
                        {
                            return false;
                        }
                    }
                    break;
                case MovementDirection.Up:
                    for (var i = 0; i < 9; i++)
                    {
                        if (!_gameMap.CurrentMap.ContainsKey(new Coordinates(Palinurus.LeftOffset - 4 + i,
                            Palinurus.TopOffset - 3))) continue;
                        if (_gameMap.CurrentMap[
                                new Coordinates(Palinurus.LeftOffset - 4 + i, Palinurus.TopOffset - 3)].Category ==
                            ObjectCategory.DumbStand)
                        {
                            return false;
                        }
                    }
                    break;
            }
            return true;
        }

        private int GetStartingPalinurusTopOffset(int resolutionHeight) => resolutionHeight / 2;

    }
}
