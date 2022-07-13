using UnityEngine;

public class LaserSelect : MonoBehaviour {

	public GameObject laser;
    public float offset = 0;

	private GameObject laserInstance;

	private bool didRayHit;
    private RaycastHit hit;
    public RaycastHit Hit { get { return hit; } }
    public Color targetHitColor = new Color(165, 255, 17, 255);
    private Color startHitColor;
    public bool isTargetHit = false;

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
            ParticleSystemRenderer pr = laserInstance.GetComponent<ParticleSystemRenderer>();
            startHitColor = pr.material.color;
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

    public void ChangeToHitTargetColor()
    {
        isTargetHit = true;
        ParticleSystemRenderer pr = laserInstance.GetComponent<ParticleSystemRenderer>();
        pr.material.color = targetHitColor;
        ParticleSystem.MainModule psmm = laserInstance.GetComponent<ParticleSystem>().main;
        psmm.startColor = targetHitColor;
    }

    public void ChangeToDefaultColor()
    {
        isTargetHit = false;
        ParticleSystemRenderer pr = laserInstance.GetComponent<ParticleSystemRenderer>();
        pr.material.color = startHitColor;
        ParticleSystem.MainModule psmm = laserInstance.GetComponent<ParticleSystem>().main;
        psmm.startColor = startHitColor;
    }
}
