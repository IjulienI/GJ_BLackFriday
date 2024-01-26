using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private bool canEarthQuake = true;

    private void Awake()
    {
        Instance = this;
    }
    public void SetEarthQuake(bool statu)
    {
        canEarthQuake = statu;
    }
    public bool GetEarthQuake()
    {
        return canEarthQuake;
    }
}