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
    

    private void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
        RaycastHit hit;
        Physics.Raycast(transform.position, transform.forward, out hit);
        Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.red,0.1f);

        if(hit.distance < minDist)
        {
            if(hit.collider.tag == "Player")
            {

            }
            if(Random.Range(0,2) == 0)
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
