using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task : MonoBehaviour
{
    public bool complete;
    public string taskName;
    public DialogSequence completeDialogSegment;

    [Header("Unit Test")]
    public bool testComplete;

    private void Update()
    {
        if (testComplete)
        {
            CompleteTask();
            testComplete = false;
        }
    }

    public void CompleteTask()
    {
        if (!complete)
        {
            complete = true;
            if (completeDialogSegment != null && DialogManager.instance != null)
                DialogManager.instance.PlayDialog(completeDialogSegment);
        }
    }
}
