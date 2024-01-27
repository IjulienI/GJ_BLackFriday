using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public bool CanEarthQuake()
    {
        return GameObject.FindObjectsOfType<SlideFloor>().Length == 0;
    }
}