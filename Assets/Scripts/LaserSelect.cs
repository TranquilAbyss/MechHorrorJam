using UnityEngine;

public class LaserSelect : MonoBehaviour {

	public GameObject laser;
    public float offset = 0;

	private GameObject laserInstance;

	private bool didRayHit;
    private RaycastHit hit;
    public RaycastHit Hit { get { return hit; } }

	
	// Update is called once per frame
	void Update () {
        UpdateLaserPointer();
	}

    void UpdateLaserPointer()
    {
		didRayHit = Physics.Raycast(transform.position, transform.forward, out hit, 1000.0f);

        //laser position
        if (laserInstance == null)
        {
            laserInstance = Instantiate(laser, hit.point, Quaternion.identity) as GameObject;
        }
        if (didRayHit)
        {
            laserInstance.SetActive(true);
            laserInstance.transform.position = hit.point + hit.normal * offset;
        }
        else
        {
            laserInstance.SetActive(false);
        }
    }

}
