using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<GameObject> inventory = new List<GameObject>();

    private int score = 0;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision != null && collision.gameObject.tag == "Payout")
        {
            for(int i = 0; i < inventory.Count; i++)
            {
                if(PlayerListScript.Instance.ListContain(inventory[i]))
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
