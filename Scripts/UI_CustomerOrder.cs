using Forkify;
using Godot;
using System;
using System.Linq;

namespace Forkify;
public partial class UI_CustomerOrder : Control
{
	#region Setters
	private UI_CustomerOrder _orderUI;
	[Export] private Panel _panel;
	[Export] private Label _titleUI;
	[Export] private Label _customerNameUI;
	[Export] private Label _priceUI;
	[Export] private Label _recipeNameUI;
	[Export] private GridContainer ingredientsDisplay;
	private RecipeManager _recipeManager;
    #endregion

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		_orderUI = this;
		var recipe = GD.Load<PackedScene>("res://Scenes//RecipeManager.tscn");
		_recipeManager = recipe.Instantiate<RecipeManager>();
		DisplayOrder();
	}

	public void DisplayOrder()
	{ 
		var lenght = _recipeManager.Recipes.Count;
		var random = new Random().Next(0, lenght);
		_recipeNameUI.Text = $"Recipe: { _recipeManager.Recipes.ElementAt(random).Key}";
		string ingredients = _recipeManager.Recipes.ElementAt(random).Value;
		for(int i = 0 ; i < ingredients.Split(", ").Length ; i++)
		{
			var label = new Label();
			label.Text = $"[{i+1}] {ingredients.Split(",")[i]}";

			ingredientsDisplay.AddChild(label);
		}
		_panel.Position = new Vector2(640, 360);
		_orderUI.Show();
	}
}
