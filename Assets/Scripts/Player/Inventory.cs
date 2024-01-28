using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject pickItemEffect;
    [SerializeField] private int inventorySize;
    private List<GameObject> inventory = new List<GameObject>();

    private int score = 0;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "item")
        {
            if(inventory.Count < inventorySize)
            {
                Instantiate(pickItemEffect, other.transform);
                AddInventory(other.gameObject);
                Destroy(other.gameObject);
            }
        }

        else if (other != null && other.gameObject.tag == "Payout")
        {
            for (int i = 0; i < inventory.Count; i++)
            {
                if (PlayerListScript.Instance.ListContain(inventory[i]))
                {
                    score += inventory[i].GetComponent<ItemScript>().GetPoints();
                    print(inventory[i].GetComponent<ItemScript>().GetPoints());
                }
                else
                {
                    score += 2;
                    print(2);
                }

                inventory.Remove(inventory[i]);
            }
        }
    }

    public void HitEvent()
    {
        int randomItemDrop = Random.Range(1, inventory.Count - 1);
       for (int i = 0; i < randomItemDrop; i++) 
        {
            RemoveInventory(i, true);
        }
    }

    public void AddInventory(GameObject item)
    {
        inventory.Add(item);
        for (int i = 0;i < inventory.Count; i++)
        {
            print(inventory[i]);
        }
    }
    public void RemoveInventory(int item, bool dropItem)
    {
        if(dropItem)
        {
            Instantiate(inventory[item], transform.right, Quaternion.identity);
        }
        inventory.Remove(inventory[item]);
    }
}
