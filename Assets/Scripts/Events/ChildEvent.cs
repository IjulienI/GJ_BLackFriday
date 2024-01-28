using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildEvent : Events
{
    [SerializeField] private GameObject child;
    [SerializeField] private List<GameObject> spawners = new List<GameObject>();
    [SerializeField] private int maxBaby;
    public override void StartEvent()
    {
        if(spawners != null)
        {
            if(spawners.Count < 1) 
            {
                Instantiate(child, spawners[0].transform.position, spawners[0].transform.rotation);
            }
            else
            {
                int babyCount = Random.Range(1, maxBaby + 1 );
                Debug.Log(babyCount);
                for(int i = 0; i < babyCount; i++)
                {
                    Instantiate(child, spawners[i].transform.position, spawners[i].transform.rotation);
                }
            }
        }
    }
}