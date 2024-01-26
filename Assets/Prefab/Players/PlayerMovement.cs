using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float drag;
    [SerializeField] private float steerAngle;
    [SerializeField] private float traction;

    private Vector3 MoveForce;

    void Update()
    {
        MoveForce += transform.forward * moveSpeed * Input.GetAxis("Vertical") * Time.deltaTime;
        float steerInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * steerInput * MoveForce.magnitude * steerAngle * Time.deltaTime);
        MoveForce *= drag;
        MoveForce = Vector3.ClampMagnitude(MoveForce, maxSpeed);
        Debug.DrawRay(transform.position, MoveForce.normalized * 3);
        Debug.DrawRay(transform.position, transform.forward * 3, Color.blue);
        MoveForce = Vector3.Lerp(MoveForce.normalized, transform.forward, traction * Time.deltaTime) * MoveForce.magnitude;

        transform.position += MoveForce * Time.deltaTime;
    }
}

