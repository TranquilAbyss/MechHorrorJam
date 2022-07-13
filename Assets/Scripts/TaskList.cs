using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;

public class TaskList : MonoBehaviour
{
    public Task[] tasksToComplete;
    public UnityEvent completeEvent;

    // Update is called once per frame
    void Update()
    {
        if(tasksToComplete.All(x => x.complete))
        {
            completeEvent?.Invoke();
        }
    }
}
