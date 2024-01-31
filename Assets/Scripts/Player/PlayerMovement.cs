using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float boostForce;
    [SerializeField] private float boostTime = 1f;
    [SerializeField] private float acceleration;
    [SerializeField] private float steeringSpeed;
    [SerializeField] private GameObject[] carAndWheel;
    [SerializeField] private GameObject[] hitParticles;
    [SerializeField] private GameObject spinParticle;
    [SerializeField] private ParticleSystem[] driveParticle;
    [SerializeField] private float attackCooldown = 0.4f;
    [SerializeField] private bool iceResistance;
    [SerializeField] private AudioClip punch, impact;

    private AudioSource audioSource;
    private Vector2 input;
    private Rigidbody rb;
    private bool onIce, isAttacking, isBoosted, canMove = true;
    private float iceRotSpeed, babySpeed;
    private float baseMoveSpeed, basesteering;

    public void InputMovement(Vector2 newInput)
    {
        input = newInput;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        baseMoveSpeed = moveSpeed;
        babySpeed = baseMoveSpeed / 2f;
        basesteering = steeringSpeed;
        driveParticle[0] = transform.GetChild(0).GetComponent<ParticleSystem>();
        driveParticle[1] = transform.GetChild(1).GetComponent<ParticleSystem>();
        driveParticle[2] = transform.GetChild(2).GetComponent<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();
    }

    public void OpenList()
    {
        CancelInvoke(nameof(ToggleList));
        canMove = false;
        Invoke(nameof(ToggleList), 1.5f);
    }
    private void ToggleList()
    {
        canMove = true;
    }

    public void InputBoost()
    {
        if(!isBoosted)
        {
            driveParticle[0].Stop();
            driveParticle[1].Stop();
            driveParticle[2].Play();
            isBoosted = true;
            Invoke(nameof(stopBoost), boostTime);
        }
    }
    private void stopBoost()
    {
        isBoosted = false;
        driveParticle[0].Play();
        driveParticle[1].Play();
        driveParticle[2].Stop();
    }

    public void AttackLeft()
    {
        if(!isAttacking && canMove)
        {
            isAttacking = true;

            RaycastHit hit;

            if (Physics.SphereCast(transform.position, 1, transform.right, out hit, 4))
            {
                if(hit.collider.gameObject.GetComponent<PlayerMovement>() != null)
                {


                    hit.collider.gameObject.GetComponent<PlayerMovement>().SetOnIce(true);
                }
                if(hit.collider.gameObject.GetComponent<RayonScript>() != null)
                {

                    gameObject.GetComponent<Inventory>().AddInventory(hit.collider.gameObject.GetComponent<RayonScript>().GetItem());
                }
            }
            //audioSource.clip = punch;
            //audioSource.volume = 0.1f;
            //audioSource.Play();
            Invoke(nameof(ResetAttack), attackCooldown);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<PlayerMovement>() != null)
        {
            foreach (ContactPoint contact in collision.contacts)
            {
                Debug.DrawRay(contact.point, contact.normal, Color.white);

                if (collision.relativeVelocity.magnitude > 0.5f)
                {
                    Instantiate(hitParticles[0], contact.point, Quaternion.identity);
                }
            }
        }
        else if(collision.gameObject.tag != "floor")
        {
            Instantiate(hitParticles[1], transform.position + transform.forward, Quaternion.identity);
            //audioSource.clip = impact;
            //audioSource.volume = 0.02f;
            //audioSource.Play();
        }
    }
    public void AttackRight()
    {
        if (!isAttacking && canMove)
        {
            isAttacking = true;

            RaycastHit hit;

            if (Physics.SphereCast(transform.position, 3, -transform.right, out hit, 4))
            {
                if (hit.collider.gameObject.GetComponent<PlayerMovement>() != null)
                {
                    hit.collider.gameObject.GetComponent<PlayerMovement>().SetOnIce(true);
                }
                else if (hit.collider.gameObject.GetComponent<RayonScript>() != null)
                {
                    print("get item");
                    gameObject.GetComponent<Inventory>().AddInventory(hit.collider.gameObject.GetComponent<RayonScript>().GetItem());
                }
            }
            //audioSource.clip = punch;
            //audioSource.volume = 0.1f;
            //audioSource.Play();
            Invoke(nameof(ResetAttack), attackCooldown);
        }
    }

    private void ResetAttack()
    {
        isAttacking = false;
    }


    void Update()
    {
        if(!onIce)
        {
            if (Vector3.Magnitude(input) > 0 && canMove)
            {
                rb.drag = 5;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(input.x, 0, input.y)), steeringSpeed * Time.deltaTime);
                if(isBoosted)
                {
                    rb.velocity = rb.velocity + ((transform.forward * 3) * moveSpeed * boostForce * Vector3.Magnitude(input) * Time.deltaTime);
                    steeringSpeed = basesteering * 1.5f;
                }
                else
                {
                    rb.velocity = rb.velocity + ((transform.forward * 3) * moveSpeed * Vector3.Magnitude(input) * Time.deltaTime);
                    rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
                    if(steeringSpeed > basesteering)
                    {
                        steeringSpeed -= Time.deltaTime;
                    }
                }
            }
            else
            {

                rb.drag = 3f;
            }
            float angleToGlobalForward = Vector3.SignedAngle(new Vector3(input.x, 0, input.y), transform.forward, Vector3.up);
            float normalizedAngle = angleToGlobalForward / 180f;
            float targetTiltAngle = 50 * -normalizedAngle;
            carAndWheel[0].transform.localRotation = Quaternion.Slerp(carAndWheel[0].transform.localRotation, Quaternion.Euler(-targetTiltAngle, 90, 0), steeringSpeed / 2 * Time.deltaTime);
        }
        else
        {
            iceRotSpeed -= 700 * Time.deltaTime;
            transform.rotation *= Quaternion.Euler(0, iceRotSpeed * Time.deltaTime, 0);
        }
    }

    public void SetOnIce(bool state)
    {
        onIce = state;
        if(state)
        {
            var spin = Instantiate(spinParticle, transform.position, Quaternion.identity);
            spin.transform.SetParent(transform, false);
            CancelInvoke(nameof(DisableIce));
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
            moveSpeed = babySpeed;
        }
        else
        {
            moveSpeed = baseMoveSpeed;
        }
    }

    public bool GetIceResistance()
    {
        return iceResistance;
    }
}

