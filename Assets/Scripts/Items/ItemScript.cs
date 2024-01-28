using UnityEngine;

public class ItemScript : MonoBehaviour
{
    [SerializeField] private int points;
    [SerializeField] private string nameInList;

    public string GetName()
    {
        return nameInList;
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
