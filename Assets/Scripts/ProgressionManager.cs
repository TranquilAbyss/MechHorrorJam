using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ProgressionManager : MonoBehaviour
{
    public EventProgression[] eventProgressions;
    public int currentEventIndex;
    public EventProgression currentEventProgression;


    private void Start()
    {
        currentEventProgression = eventProgressions[0];
        currentEventProgression.beginEvent?.Invoke();
    }

    void Update()
    {

        bool complete = true;
        foreach (var task in currentEventProgression.tasks)
        {
            complete &= task.complete;
        }
        currentEventProgression.complete = complete;

        if (currentEventProgression.complete)
        {
            currentEventProgression.completeEvent?.Invoke();
            if (currentEventIndex < eventProgressions.Length - 1)
            {
                currentEventIndex++;

                currentEventProgression = eventProgressions[currentEventIndex];
                eventProgressions[currentEventIndex].beginEvent?.Invoke();
            }
        }
    }
}

[System.Serializable]
public class EventProgression
{
    public bool complete;
    public Task[] tasks;
    public UnityEvent beginEvent;
    public UnityEvent completeEvent;
}


