using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleMenu : MonoBehaviour
{
    public MechControls mechControls;
    public FPSCameraPivot upperBodyPivot;
    public GameObject MenuGraphics;
    public Text playButtonText;
    public Text continueButtonText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            playButtonText.enabled = false;
            continueButtonText.enabled = true;
            mechControls.enabled = !mechControls.enabled;
            upperBodyPivot.toggleMouseLock = !upperBodyPivot.toggleMouseLock;
            MenuGraphics.SetActive(!MenuGraphics.activeSelf);
        }
    }
}
