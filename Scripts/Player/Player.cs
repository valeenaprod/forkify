using Godot;
using System;

namespace Forkify;

public partial class Player : CharacterBody2D
{
    #region private setters
    private Vector2 _velocity = Vector2.Zero;
    [Export] public const float Speed = 1.0f;
    private CharacterBody2D _player;
    private AnimatedSprite2D _sprite;
    private bool _isNearNPC;
    private Area2D _interactionArea;
    private Node _npc;
    private Customer_Control _targetCustomer;
    #endregion

    public float Gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

    public override void _Ready()
    {

        _player = this;
        _isNearNPC = false;
        _sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        _interactionArea = GetNode<Area2D>("InteractionArea");
        _interactionArea.BodyEntered += OnInteractionAreaBodyEntered;
        _interactionArea.BodyExited += OnInteractionAreaBodyExited;

    }

    public override void _PhysicsProcess(double delta)
    {

        _velocity = Vector2.Zero;

        // basic movement system
        if (Input.IsActionPressed("move_up")) 
        {
            _velocity.Y -= 1;
            _sprite.Play("move_up");
        }

        if (Input.IsActionPressed("move_down")) { 
            _velocity.Y += 1;
            _sprite.Play("move_down");
        }

        if (Input.IsActionPressed("move_right")) 
        { 
            _velocity.X += 1;
            _sprite.Play("move_right");
        }

        if (Input.IsActionPressed("move_left")) 
        { 
            _velocity.X -= 1;
            _sprite.Play("move_left");
        }

        if (_velocity.Length() > 0)
        {
            _velocity = _velocity.Normalized() * Speed;
            _sprite.Play();
        }
        else _sprite.Stop();

        MoveAndCollide(_velocity);
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        // When player interact with an object && object is an npc
        if(@event.IsActionPressed("interact") && _isNearNPC)
            InteractWithNPC(@event);
    }


    // When player interacts with an NPC
    private void InteractWithNPC(InputEvent @event)
    {
        GD.Print($"Interacting with: {_npc.Name}");
       // if(_targetCustomer.IsInteractable()) GD.Print("Is Interactable");
        
    }

    // When a body enters the InteractionArea
    private void OnInteractionAreaBodyEntered(Node body)
    {
        if(body is Customer_Control)
        {
            GD.Print("body entered");
            _isNearNPC = true;
            _targetCustomer = (Customer_Control)body;
        }
    }

    private void OnInteractionAreaBodyExited(Node body)
    { 
        if (body is Customer_Control)
        {
            GD.Print("body exited");
            _isNearNPC = false;
            _targetCustomer = null;
        }
    }
}
