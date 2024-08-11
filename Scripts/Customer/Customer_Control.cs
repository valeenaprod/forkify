using Godot;
using System;


namespace Forkify;
public partial class Customer_Control : CharacterBody2D
{

	private NavigationAgent2D _navAgent;
	private TileMap _restaurantMap;
	private Vector2 _movementTargetPosition = new Vector2(56, 128);

	private float _movementSpeed = 20.0f;

	public Vector2 MovementTarget
	{
		get { return _navAgent.TargetPosition; }
		set { _navAgent.TargetPosition = value; }
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_navAgent = GetNode<NavigationAgent2D>("NavigationAgent2D");

		Callable.From(ActorSetup).CallDeferred();
	}

	public override void _PhysicsProcess(double delta)
	{
		if(_navAgent.IsNavigationFinished()) return;

		Vector2 currentAgentPosition = GlobalTransform.Origin;
		Vector2 nextPathPosition = _navAgent.GetNextPathPosition();

		Velocity = currentAgentPosition.DirectionTo(nextPathPosition) * _movementSpeed;
		//GD.Print("Moving!");
		MoveAndSlide();
	}

	private async void ActorSetup()
	{
		await ToSignal(GetTree(), SceneTree.SignalName.PhysicsFrame);

		MovementTarget = _movementTargetPosition;
		GD.Print("Target: " + _movementTargetPosition);
	}
}
