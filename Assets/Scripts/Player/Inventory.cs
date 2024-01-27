using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<string> inventory = new List<string>();

    public void AddInventory(string item)
    {
        inventory.Add(item);
    }
    public void RemoveInventory(string item)
    {
        Debug.Log("Remove Item");
    }
}
