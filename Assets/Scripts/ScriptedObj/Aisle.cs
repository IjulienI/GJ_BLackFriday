using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aisle : MonoBehaviour
{
    [SerializeField] private GameObject item;
    [SerializeField] private float time;

    private bool canGetItem;
    private float delay = 0;

    private void Update()
    {
        if(!canGetItem)
        {
            delay -= 1 * Time.deltaTime;
            if(delay <= 0)
            {
                canGetItem = !canGetItem;
            }
        }
    }
    public string GetItem()
    {
        delay = time;
        return item.name;        
    }
}
