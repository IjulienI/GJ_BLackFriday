using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{


    public void OpenDoor()
    {
        GetComponent<MeshRenderer>().material.color = Color.green;
        Invoke(nameof(CloseDoor), 1f);
    }

    private void CloseDoor()
    {
        GetComponent<MeshRenderer>().material.color = Color.red;
    }
}
