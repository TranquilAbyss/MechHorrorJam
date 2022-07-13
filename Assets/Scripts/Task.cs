using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Task : MonoBehaviour
{
    public bool StartTask;
    public bool complete;
    public string taskName;
    public DialogSequence completeDialogSegment;
    public UnityEvent completeEvent;

    [Header("Unit Test")]
    [SerializeField] bool testComplete;


    private void Start()
    {
        if(StartTask)
        {
            CompleteTask();
        }
    }

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
            completeEvent?.Invoke();
            if (completeDialogSegment != null && DialogManager.instance != null)
                DialogManager.instance.PlayDialog(completeDialogSegment);
        }
    }
}
