using Forkify;
using Godot;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Forkify;
public partial class GameManager : Node2D
{
	#region private setters
	private Random _random = new Random();
    private int _customerCount;
	#endregion

	#region class setters
	private PackedScene _uiScene;
	private PackedScene _playerScene;
    private PackedScene _customerScene;
	private PackedScene _saveLoadManagerScene;
	//
    private UIManager _uiManager;
	private SaveLoadManager _saveLoadManager;
	private Customer _customer;
    private CharacterBody2D _player;

	private List<Vector2> _customerTarget;
	//private PackedScene 
	#endregion
	private Panel _uiMainMenu;
	private Panel _uiCreateNewGame;


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		
		// Load the scenes
		_uiScene = GD.Load<PackedScene>("res://Scenes//UI_Scene.tscn");
		_playerScene = GD.Load<PackedScene>("res://Scenes//PlayerScene.tscn");
		_saveLoadManagerScene = GD.Load<PackedScene>("res://Scenes//SaveLoadManager.tscn");
		//
		_saveLoadManager = _saveLoadManagerScene.Instantiate<SaveLoadManager>();
		AddChild(_saveLoadManager);

		_uiManager = _uiScene.Instantiate<UIManager>();
		AddChild(_uiManager);
		_uiMainMenu = _uiManager.GetNode<Panel>("UI_MainMenu");
		_uiCreateNewGame = _uiManager.GetNode<Panel>("UI_CreateNewGame");
		_uiMainMenu.Show();
        _uiMainMenu.GetNode<Button>("Button_NewGame").Pressed += CreateNewGame;
		_uiMainMenu.GetNode<Button>("Button_LoadGame").Pressed += LoadGame;
		//
		_customerTarget = new List<Vector2>();

		_customerTarget.Add(new Vector2(56, 128));
		_customerTarget.Add(new Vector2(120, 128));
		_customerTarget.Add(new Vector2(216, 128));
		_customerTarget.Add(new Vector2(280, 128));
    }

	private void LoadGame()
	{
		_saveLoadManager.LoadCharacter();
		StartGame();
	}

    private void StartGame()
	{
    
		_player = _playerScene.Instantiate<Player>();
		_player.Position = GetNode<Marker2D>("PlayerSpawn").Position;
		GetNode<TileMap>("RestaurantInterior").Show();
		AddChild(_player);
        _customerCount = 0;
        SetNewCustomerTimer();
        _uiMainMenu.Hide();
    }

	private async void SetNewCustomerTimer()
	{
		float randomTime = (float) _random.NextDouble() * 4.0f + 1.0f;
		await ToSignal(GetTree().CreateTimer(randomTime), SceneTreeTimer.SignalName.Timeout);
		SetNewCustomerTimerTimeout();
	}

	private void SetNewCustomerTimerTimeout()
	{
		// if there's already a customer in the store
		if(_customerCount >= 1) return;
        //
        _customerScene = GD.Load<PackedScene>("res://Scenes//NPC//Customer.tscn");
        _customer = _customerScene.Instantiate<Customer>();
		var name = _customer.CustomerNamesList[_random.Next(0, 5)];
		_customerCount++;
        _customer.Type = 0; // type = regular
        _customer.State = 0; // state = entering
        _customer.CustomerName = name;
		AddChild(_customer);

    }

	private async void SetNewOrderTimer()
	{
        float randomTime = (float)_random.NextDouble() * 4.0f + 1.0f;
        await ToSignal(GetTree().CreateTimer(randomTime), SceneTreeTimer.SignalName.Timeout);
		SetNewOrderTimerTimeout();
    }

	private void SetNewOrderTimerTimeout()
	{
		_customer.GetNode<Label>("UI_OrderAvailable").Show();
	}

	#region SaveSystem
	private  void CreateNewGame()
	{

		// text to display

		string welcome_Text = "Welcome to Forkify!";
		string askCharacterName_Text = "What is the name of the character?";
		string askRestaurantName_Text = "What is the name of the restaurant?";


		// text nodes for the ui
		var welcome_Node = _uiCreateNewGame.GetNode<Label>("UI_CreateNewGame_Welcome");
		var askCharacterName_Node = _uiCreateNewGame.GetNode<Label>("UI_CreateNewGame_AskCharacterName");
		var askRestaurantName_Node = _uiCreateNewGame.GetNode<Label>("UI_CreateNewGame_AskRestaurantName");
		
		welcome_Node.Text = welcome_Text;
		askCharacterName_Node.Text = askCharacterName_Text;
		askRestaurantName_Node.Text = askRestaurantName_Text;

		var submitButton = _uiCreateNewGame.GetNode<Button>("Button_CreateNewGame_Submit");

		submitButton.Pressed += CreateNewGame_OnSubmitButtonPressed;

		// Hide the main menu
		_uiMainMenu.Hide();

		// Show the game creation menu

		_uiCreateNewGame.Show();

    }

	private void CreateNewGame_OnSubmitButtonPressed()
	{
        // input nodes
        var askCharacterName_Input = _uiCreateNewGame.GetNode<LineEdit>("Input_CreateNewGame_CharacterName");
        var askRestaurantName_Input = _uiCreateNewGame.GetNode<LineEdit>("Input_CreateNewGame_RestaurantName");
		var charNameLenght = askCharacterName_Input.Text.Length;
		var restNameLenght = askRestaurantName_Input.Text.Length;
		if (charNameLenght == 0)
		{
			_uiCreateNewGame.GetNode<Label>("Error_CharacterName_Empty").Show();
		}
		if (restNameLenght == 0)
		{
			_uiCreateNewGame.GetNode<Label>("Error_RestaurantName_Empty").Show();
		}
		if(restNameLenght  >= 1 && charNameLenght >= 1)
		{
			_uiCreateNewGame.Hide();
			_saveLoadManager.CreateNewSaveGame("roger");
			StartGame();

		}
		else return;



    }

	#endregion
}
