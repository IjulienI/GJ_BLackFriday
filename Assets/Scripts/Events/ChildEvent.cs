using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildEvent : Events
{
    [SerializeField] private GameObject child;
    public override void StartEvent()
    {
        GameObject door = DoorManager.instance.ChoiceDoor();
        Instantiate(child,transform.position, transform.rotation);
    }
}