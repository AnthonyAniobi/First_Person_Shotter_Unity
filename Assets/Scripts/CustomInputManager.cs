using System;
using UnityEngine;
using UnityEngine.InputSystem;

public enum ActionMapType
{
    Player,
    UI
}

static class ActionMapNames
{
    public static String GetName(this ActionMapType actionMapType)
    {
        switch (actionMapType)
        {
            case ActionMapType.Player:
                return "Player";
            case ActionMapType.UI:
                return "UI";
            default:
                throw new ArgumentOutOfRangeException(nameof(actionMapType), actionMapType, null);
        }
    }
}

public class CustomInputManager : MonoBehaviour
{
    static public CustomInputManager instance { get; private set; }

    private InputActionMap inputMap; 

    void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        SwitchActionMap(ActionMapType.Player);
    }

    void SwitchActionMap(ActionMapType actionMapName)
    {
        inputMap = InputSystem.actions.FindActionMap(actionMapName.GetName());
        inputMap.Enable();
    }

    public Vector2 GetMovementInput()
    {
        return inputMap.FindAction("Move").ReadValue<Vector2>();
    }

    public Vector2 GetViewInput()
    {
        return inputMap.FindAction("Look").ReadValue<Vector2>();
    }

    public bool GetShootInput(bool continuous = false)
    {
        InputAction shoot = inputMap.FindAction("Attack");
        if(continuous)
        {
            return shoot.IsPressed();
        }
        else
        {
            return shoot.WasPressedThisFrame();
        }
    }

    public bool GetJumpInput()
    {
        InputAction jump = inputMap.FindAction("Jump");
        return jump.WasPressedThisFrame();
    }

    public bool GetReloadInput()
    {
        InputAction reload = inputMap.FindAction("Reload");
        return reload.WasPressedThisFrame();
    }

    public bool PickUpWeaponInput()
    {
        InputAction pickUpWeapon = inputMap.FindAction("PickUpWeapon");
        return pickUpWeapon.WasPressedThisFrame();
    }
}
