using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Train : MonoBehaviour
{
    [SerializeField] private float speed;

    private void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }
}
