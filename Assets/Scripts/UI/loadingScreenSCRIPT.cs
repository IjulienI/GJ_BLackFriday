using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadingScreenSCRIPT : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        Invoke(nameof(DestroyLoadingScreen), 5f);
    }

    private void DestroyLoadingScreen()
    {
        EventManager.Instance.StartEvent();
        Destroy(gameObject);
    }
}
