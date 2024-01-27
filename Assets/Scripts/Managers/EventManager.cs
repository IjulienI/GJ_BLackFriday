using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    [SerializeField] List<Events> events;

    private Events lastEvent;

    private void Start()
    {
        Invoke(nameof(StartEvent), 1f);
    }

    private void StartEvent()
    {
        Events eventTemp;
        while (true)
        {
            eventTemp = events[Random.Range(0, events.Count)];
            if(eventTemp != lastEvent )
            {
                Debug.Log(eventTemp.name);
                if(eventTemp.name == "EarthQuakeEvent")
                {
                    if(GameManager.Instance.CanEarthQuake())
                    {
                        lastEvent = eventTemp;
                        eventTemp.StartEvent();
                        break;
                    }
                    Debug.Log("CancelEarthQuake");
                }
                else
                {
                    lastEvent = eventTemp;
                    eventTemp.StartEvent();
                    break;
                }
            }
        }
        Invoke(nameof(StartEvent), 5f);
    }
}