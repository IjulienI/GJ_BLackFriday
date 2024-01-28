using UnityEngine;

public class FXScript : MonoBehaviour
{
    private void Start()
    {
        Invoke(nameof(DestroySelf), 5);
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
