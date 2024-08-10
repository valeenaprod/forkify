using Godot;
using System;

namespace Forkify;
public partial class UIManager : Control
{
    #region CustomerOrderUI_Setters
    private Panel _customerOrderUI;
    private Label _customerOrderTitle;
    #endregion
    public override void _Ready()
    {
        // Customer Order
        _customerOrderUI = GetNode<Panel>("CustomerOrder");
        _customerOrderTitle = _customerOrderUI.GetNode<Label>("Title");

        // ...
    }

    private void OnButtonPressed()
    {

    }



}
