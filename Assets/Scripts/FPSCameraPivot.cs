using UnityEngine;
using System.Collections;

public class FPSCameraPivot : MonoBehaviour {

	public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
	public RotationAxes axes = RotationAxes.MouseXAndY;
	public float sensitivityX = 15F;
	public float sensitivityY = 15F;
	
	public float minimumX = -360F;
	public float maximumX = 360F;
	
	public float minimumY = -60F;
	public float maximumY = 60F;

	public bool toggleMouseLock = false;

	public float currentRotationX = 0F;
	public float currentRotationY = 0F;

	public AudioSource rotatoinSound;
	private Quaternion previousQuaternion;

	void Update ()
	{
		if (toggleMouseLock) 
		{
			Cursor.lockState = CursorLockMode.Locked;

			if (axes == RotationAxes.MouseXAndY)
			{
				currentRotationX += Input.GetAxis("Mouse X") * sensitivityX;
				currentRotationX = Mathf.Clamp(currentRotationX, minimumX, maximumX);

				currentRotationY += Input.GetAxis("Mouse Y") * sensitivityY;
				currentRotationY = Mathf.Clamp (currentRotationY, minimumY, maximumY);

				transform.localEulerAngles = new Vector3(-currentRotationY, currentRotationX, 0);

			}
			else if (axes == RotationAxes.MouseX)
			{
				currentRotationX += Input.GetAxis("Mouse X") * sensitivityX;
				currentRotationX = Mathf.Clamp(currentRotationX, minimumX, maximumX);

				transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, -currentRotationX, 0);
			}
			else
			{
				currentRotationY += Input.GetAxis("Mouse Y") * sensitivityY;
				currentRotationY = Mathf.Clamp (currentRotationY, minimumY, maximumY);
				
				transform.localEulerAngles = new Vector3(-currentRotationY, transform.localEulerAngles.y, 0);
			}

			if(previousQuaternion != transform.rotation)
            {
				if(!rotatoinSound.isPlaying)
					rotatoinSound.Play();
			}				
			else
            {
				rotatoinSound.Stop();
			}				

			previousQuaternion = transform.rotation;
		}
		else
		{
			Cursor.lockState = CursorLockMode.None;
		}	
	}

	public void ToggleMouseLockOn()
    {
		toggleMouseLock = true;
	}

	void Start ()
	{
		// Make the rigid body not change rotation
		if (GetComponent<Rigidbody>())
			GetComponent<Rigidbody>().freezeRotation = true;
	}
}
	