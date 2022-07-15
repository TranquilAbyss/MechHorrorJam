using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joystick : MonoBehaviour
{
    public FPSCameraPivot upperBody;
    public MechControls mechControls;
    public Transform pivot;
    public Transform handle;
    public Transform button;

    public float horizontalMultiplier = .6f;
    public float verticalMultiplier = .6f;
    public float twistAngleLimit = 50;

    private float mechTurnSpeedPercent = 0;
    private Vector3 buttonStartPosition;
    private float buttonTimeStamp = 0;
    private float buttonCooldown = .5f; 

    // Start is called before the first frame update
    void Start()
    {
        buttonStartPosition = button.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        mechTurnSpeedPercent = mechControls.currentTurnThrottled;

        pivot.localEulerAngles = new Vector3(upperBody.currentRotationY * horizontalMultiplier, 0, -upperBody.currentRotationX * verticalMultiplier);
        handle.localEulerAngles = new Vector3(0, mechTurnSpeedPercent * twistAngleLimit, 0);

        if (Input.GetMouseButtonDown(0))
        {
            buttonTimeStamp = buttonCooldown + Time.time;
            button.localPosition = buttonStartPosition - button.up * .02f;
        }

        if (Time.time > buttonTimeStamp)
        {
            button.localPosition = buttonStartPosition;
        }

    }
}
