using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthQuakeEvent : Events
{
    [SerializeField] private GameObject train;
    public override void StartEvent()
    {
        FridgeManager.instance.ChoiceFriges();
        Instantiate(train,transform.position,transform.rotation);
    }
}
