using UnityEngine;

public class EarthQuakeEvent : Events
{
    [SerializeField] private GameObject train;
    public override void StartEvent()
    {
        FridgeManager.instance.ChoiceFriges();
        CameraScript.Instance.Shake(4, 4);
        Instantiate(train,transform.position,transform.rotation);
    }
}
