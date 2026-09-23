using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float gravity = -9.81f;
    
    private CharacterController controller;
    private Vector3 velocity;

    void Start()
    {
       controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        InputAction moveAction = InputSystem.actions.FindAction("Move");
        InputAction jumpAction = InputSystem.actions.FindAction("Jump");

        Vector2 input = moveAction.ReadValue<Vector2>();
        Vector3 move =  Vector3.zero;
        if (controller.isGrounded)
        {
            move = transform.right * input.x + transform.forward * input.y;
            move.y = 0f;
            move.Normalize();
        }
        
        // controller.Move(speed * Time.deltaTime * move);

        if(jumpAction.WasPressedThisFrame() && controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }

        if(controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        Vector3 finalVelocity = move * speed + velocity;
        controller.Move(finalVelocity * Time.deltaTime);
    }
}
