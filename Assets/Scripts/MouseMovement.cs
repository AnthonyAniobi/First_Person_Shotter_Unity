using UnityEngine;
using UnityEngine.InputSystem;

public class MouseMovement : MonoBehaviour
{

    [SerializeField] private float mouseSensitivity = 19f;
    [SerializeField] private float topClamp = -90f;
    [SerializeField] private float bottomClamp = 90f;

    private float rotationX = 0f;
    private float rotationY = 0f;
    

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
       Vector2 mouseDelta = CustomInputManager.instance.GetViewInput();
       /// mouseX is for rotation up and down
       float mouseX = mouseDelta.x * mouseSensitivity * Time.deltaTime;
       /// mouseY is for rotation left and right
       float mouseY = mouseDelta.y * mouseSensitivity * Time.deltaTime;

       rotationX += mouseY;
       rotationX = Mathf.Clamp(rotationX, topClamp, bottomClamp);
       rotationY += mouseX;

       transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0f);
    }
}
