using Godot;
using System;

namespace Forkify;

public partial class CharacterData : Resource
{

    public Vector2 Position { get; set; }
    public string Name { get; set; }
    public int Money { get; set; }

    public CharacterData() : this(Vector2.Zero, null, 0) { }

    public CharacterData(Vector2 position, string name, int money)
    {
        Position = position;
        Name = name;
        Money = money;
    }

}   


