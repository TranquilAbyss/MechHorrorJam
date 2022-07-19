using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Visibility))]
public class HideCollider : MonoBehaviour
{
    Visibility vis;

    public Image headCameraStatic;
    public Image leftCameraStatic;
    public Image rightCameraStatic;
    public Image backCameraStatic;

    // Start is called before the first frame update
    void Start()
    {
        vis = GetComponent<Visibility>();        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Camera camera in vis.OnCameras())
        {

            if (camera.gameObject.name != "PlayerCamera")
            {
                Debug.Log(camera.gameObject.name);

                if (camera.gameObject.name == "HeadCamera")
                {
                    headCameraStatic.enabled = true;
                }
                if (camera.gameObject.name == "LeftCam")
                {
                    leftCameraStatic.enabled = true;
                }
                if (camera.gameObject.name == "RightCam")
                {
                    rightCameraStatic.enabled = true;
                }
                if (camera.gameObject.name == "BackCam")
                {
                    backCameraStatic.enabled = true;
                }
            }
        }

        foreach (Camera camera in vis.NotOnCameras())
        {
            if (camera.gameObject.name != "PlayerCamera")
            {
                if (camera.gameObject.name == "HeadCamera")
                {
                    headCameraStatic.enabled = false;
                }
                if (camera.gameObject.name == "LeftCam")
                {
                    leftCameraStatic.enabled = false;
                }
                if (camera.gameObject.name == "RightCam")
                {
                    rightCameraStatic.enabled = false;
                }
                if (camera.gameObject.name == "BackCam")
                {
                    backCameraStatic.enabled = false;
                }
            }
        }

    }
}
