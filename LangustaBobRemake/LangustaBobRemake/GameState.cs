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
            var wallOne = new Wall(new Coordinates(20,20));
            _gameMap.CurrentMap[new Coordinates(20, 20)] = wallOne;
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
                        if (!_gameMap.CurrentMap.ContainsKey(new Coordinates(Palinurus.LeftOffset - 1,
                            Palinurus.TopOffset - 2 + i))) continue;
                        if (_gameMap.CurrentMap[
                                new Coordinates(Palinurus.LeftOffset - 1, Palinurus.TopOffset - 2 + i)].Category ==
                            ObjectCategory.DumbStand)
                        {
                            return false;
                        }
                    }
                    break;
                case MovementDirection.Down:
                    break;
                case MovementDirection.Up:
                    break;
            }
            return true;
        }

        private int GetStartingPalinurusTopOffset(int resolutionHeight) => resolutionHeight / 2;

    }
}
