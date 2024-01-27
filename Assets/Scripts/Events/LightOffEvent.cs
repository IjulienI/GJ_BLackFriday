using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LightOffEvent : Events
{
    [Header("References")]
    [SerializeField] private List<GameObject> lights = new List<GameObject>();
    [Header("Settings")]
    [SerializeField] private float minEventTime;
    [SerializeField] private float maxEventTime;

    private float eventTime;
    public override void StartEvent()
    {
        if(lights.Count == 0)
        {
            Debug.Log("LightsOff");
        }
        for(int i = 0; i < lights.Count; i++)
        {
            lights[i].SetActive(false);
        }
        List<GameObject> players = GameObject.FindGameObjectsWithTag("Player").ToList();
        for(int i = 0;i < players.Count; i++)
        {
            Debug.Log(players[i].name);
        }

        eventTime = Random.Range(minEventTime, maxEventTime);
        Invoke(nameof(EndEvent), eventTime);
    }

    private void EndEvent()
    {
        if (lights.Count == 0)
        {
            Debug.Log("LightsOn");
        }
        for (int i = 0; i < lights.Count; i++)
        {
            lights[i].SetActive(true);
        }
    }
}
