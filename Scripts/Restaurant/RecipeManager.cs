using Godot;
using System;
using System.Collections.Generic;
using System.IO;

namespace Forkify;
public partial class RecipeManager : Node
{
    private List<Recipe> _recipes;

    public override void _Ready()
    {
        base._Ready();
    }

}
