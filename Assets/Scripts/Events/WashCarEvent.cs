using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WashCarEvent : Events
{
    public override void StartEvent()
    {
        GameObject door = DoorManager.instance.ChoiceDoor();
        if (door.tag == "Left") Debug.Log("Left");
        else Debug.Log("Right");
    }
}