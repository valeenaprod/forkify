using Godot;
using System;

namespace Forkify;
public partial class RestaurantData : Resource
{
    [Export] public int Money { get; set;  }
    [Export] public string Name { get; set;  }

    public RestaurantData(int money, string name) 
    {
        Money = money;
        Name = name;
    }
}
