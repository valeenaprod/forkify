using Godot;
using System.Collections.Generic;

namespace Forkify;
public partial class RecipeManager : Node
{
    public Dictionary<string, string> Recipes { get; } = new()
    {
        {"Burger", " Lettuce, Tomato, Bread, Patty"},
        {"BLT Sandwich", " Lettuce, Tomato, Bread, Chicken, Mayo"},
        {"Spaguetti", " Tomato Sauce, Spaguetti Noodles, Beef" }
    };
}