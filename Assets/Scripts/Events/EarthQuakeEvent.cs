using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthQuakeEvent : Events
{
    public override void StartEvent()
    {
        FridgeManager.instance.ChoiceFriges();
    }
}
