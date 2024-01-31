using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject pickItemEffect;
    [SerializeField] private GameObject playerCanvas;
    [SerializeField] private int inventorySize;
    private List<GameObject> inventory = new List<GameObject>();

    private int score = 0;
    private TextMeshProUGUI inventoryText;

    private void Start()
    {
        inventoryText = playerCanvas.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }
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
                    print(inventory[i].name);
                }
                else
                {
                    score += 2;
                    print(+2);
                }
                
            }
            inventory.Clear();
        }
    }

    private void Update()
    {

        if(inventory.Count > 0)
        {
            playerCanvas.transform.LookAt(Camera.main.transform.position);
            playerCanvas.SetActive(true);
            inventoryText.text = inventory.Count + "/" + inventorySize;
        }
        else
        {
            playerCanvas.SetActive(false);
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
        if (inventory.Count < inventorySize)
        {
            inventory.Add(item);
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
