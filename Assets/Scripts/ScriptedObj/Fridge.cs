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
        var iceChild = Instantiate(ice, new Vector3(transform.position.x +7f,transform.position.y+.1f, transform.position.z), Quaternion.identity);
        iceChild.transform.parent = gameObject.transform;
    }
    public bool GetOpen()
    {
        return open;
    }
    public void SetOpen(bool state)
    {
        open = state;
    }
}