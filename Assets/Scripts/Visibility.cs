using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visibility : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }


    public List<Camera> OnCameras()
    {
        List<Camera> cameras = new List<Camera>();
        RaycastHit hit;
        foreach (Camera camera in Camera.allCameras)
        {
            if (IsInCameraView(camera, gameObject))
            {
                Vector3 dir = (transform.position - camera.transform.position).normalized;
                Ray ray = new Ray(camera.transform.position, dir);
                Physics.Raycast(ray, out hit);
                Debug.DrawRay(camera.transform.position, dir);

                Debug.Log(hit);

                if (hit.collider && hit.collider.gameObject.GetHashCode() == gameObject.GetHashCode())
                {
                    Debug.Log("test");
                    cameras.Add(camera);
                }
            }
        }

        return cameras;
    }

    public List<Camera> NotOnCameras()
    {
        List<Camera> cameras = new List<Camera>();
        RaycastHit hit;
        foreach (Camera camera in Camera.allCameras)
        {
            if (IsInCameraView(camera, gameObject))
            {
                Vector3 dir = (transform.position - camera.transform.position).normalized;
                Ray ray = new Ray(camera.transform.position, dir);
                Physics.Raycast(ray, out hit);
                Debug.DrawRay(camera.transform.position, dir);

                if (hit.collider && hit.collider.gameObject.GetHashCode() != gameObject.GetHashCode())
                {
                    cameras.Add(camera);
                }
            }
            else
            {
                cameras.Add(camera);
            }
        }

        return cameras;
    }

    public bool IsVisable()
    {
        bool visible = false;
        RaycastHit hit;
        foreach (Camera camera in Camera.allCameras)
        {
            if (IsInCameraView(camera, gameObject))
            {
                Vector3 dir = (transform.position - camera.transform.position).normalized;
                Ray ray = new Ray(camera.transform.position, dir);
                Physics.Raycast(ray, out hit);
                Debug.DrawRay(camera.transform.position, dir);

                if (hit.collider.gameObject.GetHashCode() == gameObject.GetHashCode())
                {
                    visible = true;
                }
            }
        }

        return visible;
    }

    bool IsInCameraView(Camera c, GameObject go)
    {
        var planes = GeometryUtility.CalculateFrustumPlanes(c);
        //var point = go.transform.position;
        foreach (var plane in planes)
        {
            if (!GeometryUtility.TestPlanesAABB(planes, go.GetComponent<Collider>().bounds))
                return false;
        }
        return true;
    }
}
