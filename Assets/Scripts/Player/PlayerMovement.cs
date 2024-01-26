using System.Drawing;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.InputSystem.InputAction;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float drag;
    [SerializeField] private float acceleration;
    [SerializeField] private float steeringSpeed;

    private Vector2 input;
    private Rigidbody rb;

    public void playerMovement(CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(Vector3.Magnitude(input) > 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(input.x, 0, input.y)), steeringSpeed * Time.deltaTime);
            rb.velocity = rb.velocity + ((transform.forward*3) * moveSpeed * Vector3.Magnitude(input) * Time.deltaTime);
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        }
        else
        {
            rb.velocity /= 1 + (drag * Time.deltaTime);
        }

        

    }
}

