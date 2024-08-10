using Godot;
using System;

namespace Forkify;

public partial class SaveLoadManager : Node
{

	private const string SavePath = "user://savegame.tres";

	public void SaveCharacter(Vector2 characterPosition, int characterMoney)
	{
		var saveData = new CharacterData
		{
			Position = characterPosition,
			Money = characterMoney
		};

		ResourceSaver.Save(saveData, SavePath);
		GD.Print("Game Saved!");
	}

	public void LoadCharacter()
	{
		var loadedData = (CharacterData)ResourceLoader.Load(SavePath);
		if (loadedData == null) 
		{
			GD.Print("No savefile found!");
			return;
		}
		GD.Print("Game loaded!");

	}

	public void CreateNewSaveGame(string characterName)
	{
		var saveData = new CharacterData
		{
			Name = characterName
		};

		ResourceSaver.Save(saveData, SavePath);
		GD.Print("New SaveGame created!");
	}

}
