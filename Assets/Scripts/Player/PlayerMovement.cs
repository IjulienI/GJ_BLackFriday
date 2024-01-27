using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float acceleration;
    [SerializeField] private float steeringSpeed;
    [SerializeField] private GameObject[] carAndWheel;

    private Vector2 input;
    private Rigidbody rb;
    private bool onIce;
    private float iceRotSpeed;
    private float baseMoveSpeed;

    public void playerMovement(CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        baseMoveSpeed = moveSpeed;
    }

    void Update()
    {
        if(!onIce)
        {
            if (Vector3.Magnitude(input) > 0)
            {
                rb.drag = 5;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(input.x, 0, input.y)), steeringSpeed * Time.deltaTime);
                rb.velocity = rb.velocity + ((transform.forward * 3) * moveSpeed * Vector3.Magnitude(input) * Time.deltaTime);
                rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
            }
            else
            {
                rb.drag = 3f;
            }
        }
        else
        {
            iceRotSpeed -= 700 * Time.deltaTime;
            transform.rotation *= Quaternion.Euler(0, iceRotSpeed * Time.deltaTime, 0);
        }
        //carAndWheel[0].transform.localRotation = Quaternion.Euler(transform.forward - new Vector3(input.x,0,input.y)*20);

        //carAndWheel[3].transform.localRotation = Quaternion.Euler(0, input.x*30, 90);
        //carAndWheel[4].transform.localRotation = Quaternion.Euler(0, input.x*30, 90);
    }

    public void SetOnIce(bool state)
    {
        onIce = state;
        if(state)
        {
            CancelInvoke();
            rb.drag = 1;
            iceRotSpeed = 1000;
            Invoke(nameof(DisableIce), 1.5f);
        }
    }

    private void DisableIce()
    {
        onIce = false;
    }

    public void BabyEvent(bool statu)
    {
        if (statu)
        {
            moveSpeed /= 2;
        }
        else
        {
            moveSpeed = baseMoveSpeed;
        }
    }
}

