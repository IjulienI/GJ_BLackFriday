using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FridgeManager : MonoBehaviour
{
    public static FridgeManager instance;
    [Header("References")]
    [SerializeField] private List<Fridge> fridges;
    [Header("Settings")]
    [SerializeField] private int minClosedFride;
    [SerializeField] private int maxClosedFride;

    private void Awake()
    {
        instance = this;
    }

    public void ChoiceFriges()
    {
        int closedFridge = Random.Range(minClosedFride, maxClosedFride);
        for (int i = closedFridge; i < fridges.Count; i++)
        {
            while (true)
            {
                int index = Random.Range(0, fridges.Count);
                if (!fridges[index].GetOpen())
                {
                    fridges[index].OpenDoor();
                    break;
                }
            }
        }
    }
}