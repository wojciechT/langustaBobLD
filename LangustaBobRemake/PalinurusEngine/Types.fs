namespace PalinurusEngine

open System

[<AutoOpen>]
module Types =
    type Position =
        {
            LeftOffset : int;
            TopOffset : int;
        }

    type ColouredChar =
        {
            Character : char
            Colour : ConsoleColor
            BackgroundColour : ConsoleColor
        }

    type ThingRepresentation =
        {
            Top : ColouredChar[];
            Middle : ColouredChar[];
            Bottom : ColouredChar[];
        }
