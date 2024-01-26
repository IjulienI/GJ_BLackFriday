using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fridge : MonoBehaviour
{
    [SerializeField] private GameObject ice;
    private bool open;
    public void OpenDoor()
    {
        open = true;
        GetComponent<MeshRenderer>().material.color = Color.green;
        Instantiate(ice, transform.forward * 2, transform.rotation);
    }
    public bool GetOpen()
    {
        return open;
    }
}