using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throttle : MonoBehaviour
{
    public MechControls mechControls;
    public Transform knobTranform;
    public Transform frameTranform;
    public float throttleEdgeOffset = 0.05f;

    private float notches = 0;
    private float knobChangeDistance = 0;

    // Update is called once per frame
    void Update()
    {
        notches = ((mechControls.maxSpeed - mechControls.minSpeed) / mechControls.throttleIncrement) + 1;
        knobChangeDistance = (frameTranform.localScale.z - throttleEdgeOffset) / notches;
        knobTranform.localPosition = new Vector3(0, knobTranform.localPosition.y, knobChangeDistance * (mechControls.currentThrottleSpeed / mechControls.throttleIncrement));
    }
}
