using UnityEngine;
using System.Collections;

public class TouchRotation : MonoBehaviour
{
	
	public float rotationRate = 1.5f;

	void FixedUpdate ()
	{
		foreach (Touch touch in Input.touches)
		{
			if (touch.phase == TouchPhase.Moved)
			{
                transform.Rotate((touch.deltaPosition.y) * rotationRate * Time.fixedDeltaTime, 0f,  -(touch.deltaPosition.x * rotationRate * Time.fixedDeltaTime), Space.World);
			}
		}
	}
}
