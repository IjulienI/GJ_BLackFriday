using UnityEngine;

public class EarthQuakeEvent : Events
{
    [SerializeField] private GameObject train;
    public override void StartEvent()
    {
        FridgeManager.instance.ChoiceFriges();
        CameraScript.Instance.Shake(2, 0.25f);
        Instantiate(train,transform.position,transform.rotation);
    }
}
