using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Baby : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float speed;
    [SerializeField] private float angle;
    [SerializeField] private float minDist;
    [Header("LifeTime (seconds)")]
    [SerializeField] private float minTime;
    [SerializeField] private float maxTime;
    [SerializeField] private float malusTime;

    private PlayerMovement playerTouched;
    private float timeBeforeDestroy;

    private void Start()
    {
        timeBeforeDestroy = Random.Range(minTime, maxTime);
        Invoke(nameof(DestroyObject), timeBeforeDestroy);
    }

    private void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
        RaycastHit hit;
        Physics.Raycast(transform.position, transform.forward, out hit);
        Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.red,0.1f);

        if(hit.distance < minDist && hit.collider != null)
        {
            if(hit.collider.tag == "Player")
            {
                AttachBaby(hit.collider.gameObject);
            }
            else
            {
                if (Random.Range(0, 2) == 0)
                {
                    transform.rotation *= Quaternion.Euler(0, angle, 0);
                }
                else
                {
                    transform.rotation *= Quaternion.Euler(0, -angle, 0);
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            AttachBaby(collision.gameObject);
        }
    }

    private void AttachBaby(GameObject player)
    {
        Destroy(GetComponent<SphereCollider>());
        CancelInvoke(nameof(DestroyObject));
        if(playerTouched != null && playerTouched != player.GetComponent<PlayerMovement>())
        {
            playerTouched.BabyEvent(false);
        }
        playerTouched = player.GetComponent<PlayerMovement>();
        speed = 0;
        transform.SetParent(playerTouched.transform.GetChild(0).transform);
        transform.localPosition = Vector3.zero;
        playerTouched.BabyEvent(true);
        Invoke(nameof(DestroyObject), malusTime);
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
        if(playerTouched != null)
        {
            playerTouched.BabyEvent(false);
        }
    }
}
