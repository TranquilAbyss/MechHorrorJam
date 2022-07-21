using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationTrigger : MonoBehaviour
{
    public Animation animation;
    public DialogSequence completeDialogSegment;
    public UnityEvent events;
    public float time = 10;
    public UnityEvent CompletEvents;
    bool completed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       // transform.localEulerAngles = 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!completed)
        {
            StartCoroutine(WaitforCompleteEvent());
            completed = true;
        }
    }

    IEnumerator WaitforCompleteEvent()
    {
        yield return new WaitForSeconds(time);
        if (completeDialogSegment != null && DialogManager.instance != null)
            DialogManager.instance.PlayDialog(completeDialogSegment);
        if (animation)
        {
            animation.Play();
        }
        gameObject.GetComponent<Collider>().enabled = false;
        events?.Invoke();
    }
}
