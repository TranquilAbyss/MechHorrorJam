using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleMenu : MonoBehaviour
{
    public MechControls mechControls;
    public FPSCameraPivot upperBodyPivot;
    public GameObject MenuGraphics;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            mechControls.enabled = !mechControls.enabled;
            upperBodyPivot.toggleMouseLock = !upperBodyPivot.toggleMouseLock;
            MenuGraphics.SetActive(!MenuGraphics.activeSelf);
        }
    }
}
