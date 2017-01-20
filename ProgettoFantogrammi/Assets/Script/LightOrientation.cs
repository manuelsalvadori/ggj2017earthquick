using UnityEngine;
using System.Collections;

public class LightOrientation : MonoBehaviour {

	Quaternion r;
	public float a=0f;
	float d=0;

	void Start()
	{
		Input.gyro.enabled = true;
		d= Input.gyro.attitude.eulerAngles.z;
	}

	void Awake()
	{
		Input.gyro.enabled = true;
		d= Input.gyro.attitude.z;
	}

	void Update ()
	{
		Input.gyro.enabled = true;
		r = transform.rotation;
		float y = Input.gyro.attitude.eulerAngles.z-d;
		if (y < 0)
		{
			y += 360;
		}

		r = Quaternion.Euler (new Vector3 (0, y, 0));
		a = Input.gyro.attitude.eulerAngles.z;
		transform.rotation = r;
	}
}