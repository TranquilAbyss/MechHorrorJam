using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    private Task taskCompleted;
    public bool onlyOnce = true;

    private void Start()
    {
        taskCompleted = GetComponent<Task>();
    }

    public void doInteract()
    {
        if (enabled)
        {
            taskCompleted.CompleteTask();
            if (onlyOnce)
            {
                enabled = false;
            }
        }
        
    }
}
