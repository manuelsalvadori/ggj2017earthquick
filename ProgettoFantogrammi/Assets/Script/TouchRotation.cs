using UnityEngine;
using System.Collections;

public class TouchRotation : MonoBehaviour {
	
	public float rotationRate = 1.5f;
	public bool active = false;

	void Update ()
	{
		if (active)
		{
			foreach (Touch touch in Input.touches)
			{
				if (touch.phase == TouchPhase.Moved)
				{
                    transform.Rotate (-(touch.deltaPosition.x) * rotationRate, -(touch.deltaPosition.x + touch.deltaPosition.y) * rotationRate, 0, Space.World);
				}
			}
		}
	}

	public void SetActive()
	{
		active = true;
	}
	public void SetInactive()
	{
		active = false;
	}
}
