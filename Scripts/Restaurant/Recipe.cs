using Godot;
using System;
using System.Collections.Generic;

namespace Forkify;
public class Recipe
{ 
    public string Name { get; set; }
    public Dictionary<string, int> Ingredients { get; set; }

    public Recipe()
    {
        Ingredients = new Dictionary<string, int>();
    }
}
