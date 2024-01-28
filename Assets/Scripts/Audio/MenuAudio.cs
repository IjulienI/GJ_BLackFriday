using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuAudio : MonoBehaviour
{
    private List<int> safeScene = new List<int> {0,1};
    private bool destroy;
    private void Start()
    {
        Debug.Log(SceneManager.GetActiveScene().buildIndex);
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        destroy = true;
        for(int i = 0; i < safeScene.Count; i++)
        {
            if (SceneManager.GetActiveScene().buildIndex == safeScene[i])
            {
                destroy = false;
            }
        }
        if(destroy)
        {
            Destroy(gameObject);
            Debug.Log("Destroy");
        }
    }
}
