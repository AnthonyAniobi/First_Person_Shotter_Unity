using UnityEngine;
using UnityEngine.InputSystem;

public class CustomInputManager : MonoBehaviour
{
    static public CustomInputManager instance { get; private set; }
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

    public Vector2 GetMovementInput()
    {
        InputAction moveAction = InputSystem.actions.FindAction("Move");
        return moveAction.ReadValue<Vector2>();
    }

    public Vector2 GetViewInput()
    {
        InputAction lookAction = InputSystem.actions.FindAction("Look");
        return lookAction.ReadValue<Vector2>();
    }

    public bool GetShootInput(bool continuous = false)
    {
        InputAction shoot = InputSystem.actions.FindAction("Attack");
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
        InputAction jump = InputSystem.actions.FindAction("Jump");
        return jump.WasPressedThisFrame();
    }

    public bool GetReloadInput()
    {
        InputAction reload = InputSystem.actions.FindAction("Reload");
        return reload.WasPressedThisFrame();
    }
}
