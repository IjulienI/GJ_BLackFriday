using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float drag;
    [SerializeField] private float acceleration;
    [SerializeField] private float steeringSpeed;
    [SerializeField] private GameObject[] carAndWheel;

    private Vector2 input;
    private Rigidbody rb;
    private bool onIce;
    private float initialDrag;

    public void playerMovement(CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialDrag = drag;
    }

    void Update()
    {
        if(Vector3.Magnitude(input) > 0)
        {

            if(!onIce) 
            {
                drag = initialDrag;
                rb.drag = 5;
            }
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(input.x, 0, input.y)), steeringSpeed * Time.deltaTime);
            rb.velocity = rb.velocity + ((transform.forward*3) * moveSpeed * Vector3.Magnitude(input) * Time.deltaTime);
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        }
        else
        {
            if(!onIce)
            {
                rb.drag = 0.5f;
            }
        }

        if(onIce)
        {
            drag = 0;
            rb.drag = 0;
        }
        //carAndWheel[0].transform.localRotation = Quaternion.Euler(transform.forward - new Vector3(input.x,0,input.y)*20);

        //carAndWheel[3].transform.localRotation = Quaternion.Euler(0, input.x*30, 90);
        //carAndWheel[4].transform.localRotation = Quaternion.Euler(0, input.x*30, 90);
    }

    public void SetOnIce(bool state)
    {
        onIce = state;
    }
}

