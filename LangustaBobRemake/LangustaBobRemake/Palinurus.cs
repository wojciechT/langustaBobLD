﻿using System;
using System.Collections.Generic;
using System.Text;

namespace LangustaBobRemake
{
    class Palinurus
    {
        private int _leftOffset;
        private int _topOffset;
        private readonly int _maximumLeftOffset;
        private readonly int _maximumTopOffset;

        public int LeftOffset
        {
            get => _leftOffset;
            private set => _leftOffset = value < 0 
                                            ? 0 
                                            : value > _maximumLeftOffset 
                                                ? _maximumLeftOffset
                                                : value;
        }

        public int TopOffset
        {
            get => _topOffset;
            private set => _topOffset = value < 0
                                            ? 0
                                            : value > _maximumTopOffset
                                                ? _maximumTopOffset
                                                : value;
        }

        public Palinurus(Coordinates offset,Coordinates screenResolution)
        {
            _maximumLeftOffset = screenResolution.Left - 1;
            _maximumTopOffset = screenResolution.Top - 1;

            LeftOffset = offset.Left;
            TopOffset = offset.Top;
        }

        public void Move(MovementDirection direction)
        {
            switch (direction)
            {
                case MovementDirection.Right:
                    LeftOffset += 1;
                    break;
                case MovementDirection.Left:
                    LeftOffset -= 1;
                    break;
                case MovementDirection.Down:
                    TopOffset += 1;
                    break;
                case MovementDirection.Up:
                    TopOffset -= 1;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }

    }
}
