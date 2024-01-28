using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SlideFloor : MonoBehaviour
{
    [SerializeField] private float minTimeLife;
    [SerializeField] private float maxTimeLife;
    private PlayerMovement playerMovement;
    private bool destroy;
    private bool start = true;
    private float lifeTime;
    private BoxCollider boxCollider;

    private void Start()
    {
        transform.localScale = Vector3.zero;
        boxCollider = GetComponent<BoxCollider>();
        boxCollider.enabled = false;
        lifeTime = Random.Range(minTimeLife,maxTimeLife);
        Invoke(nameof(StartDestroy), lifeTime);
    }
    private void Update()
    {
        if (start)
        {
            if (transform.localScale.magnitude < 9.6f)
            {
                transform.localScale += new Vector3(7 * Time.deltaTime, 7 * Time.deltaTime, 7 * Time.deltaTime);
            }
            else
            {
                start = false;
                boxCollider.enabled = true;
            }
        }
        if (destroy)
        {   
            transform.parent.transform.parent.GetComponent<Fridge>().SetOpen(false);
            transform.localScale -= new Vector3(5 * Time.deltaTime, 5 * Time.deltaTime, 5 * Time.deltaTime);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            playerMovement = other.GetComponent<PlayerMovement>();
            if (!playerMovement.GetIceResistance())
            {
                playerMovement.SetOnIce(true);
                destroy = true;
                Destroy(boxCollider);
                Invoke(nameof(DestroyIce), 1f);
            }
        }
    }

    private void StartDestroy()
    {
        destroy = true;
        Invoke(nameof(DestroyIce), 1f);
    }
    private void DestroyIce()
    {
        Destroy(gameObject);
    }

}
