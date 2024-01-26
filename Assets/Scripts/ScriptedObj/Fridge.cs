using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fridge : MonoBehaviour
{
    private bool open;
    public void OpenDoor()
    {
        open = true;
        GetComponent<MeshRenderer>().material.color = Color.green;
    }
    public bool GetOpen()
    {
        return open;
    }
}