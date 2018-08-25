namespace LangustaBobRemake
{
    internal interface IPositionable
    {
        Coordinates Position { get; }
        ObjectCategory Category { get; }
        string[] CharacterRepresentation { get; }
    }
}