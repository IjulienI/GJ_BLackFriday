using UnityEngine;

public class ItemScript : MonoBehaviour
{
    [SerializeField] private int points;
    [SerializeField] private int count;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Inventory>().AddInventory(gameObject);
            Destroy(gameObject);
        }
    }
    public int GetPoints()
    {
        return points;
    }
    public void SetMultiplier()
    {
        points *= 2;
    }
}
