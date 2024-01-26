using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public static DoorManager instance;

    [SerializeField] private List<GameObject> doors;
    private GameObject lastDoor;
    private void Awake()
    {
        instance = this;
    }

    public GameObject ChoiceDoor()
    {
        GameObject door;        
        GameObject tempDoor;
        while (true)
        {
            tempDoor = doors[Random.Range(0, doors.Count)];
            if (tempDoor != lastDoor)
            {
                lastDoor = tempDoor;
                door = tempDoor;
                door.GetComponent<Door>().OpenDoor();
                return door;
            }
            Debug.Log("SameDoor");
        }
    }
}