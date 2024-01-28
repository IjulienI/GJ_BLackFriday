using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;

    [Header("References")]
    [SerializeField] private List<Events> events;
    [Header("Settings")]
    [SerializeField] private float minEventsRemanence;
    [SerializeField] private float maxEventsRemanence;

    private Events lastEvent;
    private float TimeBeforeEvent;

    private void Start()
    {
        Instance = this;
        TimeBeforeEvent = Random.Range(minEventsRemanence, maxEventsRemanence);
    }

    public void StartEvent()
    {
        TimeBeforeEvent = Random.Range(minEventsRemanence, maxEventsRemanence);
        Invoke(nameof(EventBegin), TimeBeforeEvent);
    }

    private void EventBegin()
    {
        Events eventTemp;
        while (true)
        {
            eventTemp = events[Random.Range(0, events.Count)];
            if (eventTemp != lastEvent)
            {
                Debug.Log(eventTemp.name);
                if (eventTemp.name == "EarthQuakeEvent")
                {
                    if (GameManager.Instance.CanEarthQuake())
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
        
        StartEvent();
    }
}