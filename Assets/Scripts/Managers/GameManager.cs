using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ShowMouse(false);
    }

    public bool CanEarthQuake()
    {
        return GameObject.FindObjectsOfType<SlideFloor>().Length == 0;
    }

    public void ShowMouse(bool statu)
    {
        if(statu)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}