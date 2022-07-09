using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task : MonoBehaviour
{
    public bool complete;
    public string taskName;

    [Header("Unit Test")]
    public bool testComplete;

    private void Update()
    {
        if (testComplete)
        {
            complete = true;
            testComplete = false;
        }
    }

    public void CompleteTask()
    {
        complete = true;
    }
}
