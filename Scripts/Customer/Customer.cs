using Godot;
using System;

namespace Forkify;

public partial class Customer : Node
{
    public enum CustomerType
    {
        Regular,
        VIP,
        Critic
    }

    public enum CustomerState
    {
        Entering,
        Ordering,
        Waiting,
        Eating,
        Leaving
    }

    public string[] CustomerNamesList =
    {
        "Arianne",
        "Felix",
        "William",
        "Questing",
        "waikup"
    };

    public CustomerType Type { get; set; }
    public String CustomerName { get; set; }

    public CustomerState State { get; set; }
    public bool IsInteractable { get; set; }

}
