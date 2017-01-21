using UnityEngine;
using System.Collections;

public class TouchRotation : MonoBehaviour
{

	public float rotationRate = 1.5f;

	void Update ()
	{
		foreach (Touch touch in Input.touches)
		{
			if (touch.phase == TouchPhase.Moved)
			{
                transform.Rotate((touch.deltaPosition.y) * rotationRate * Time.deltaTime, 0f,  -(touch.deltaPosition.x * rotationRate * Time.deltaTime), Space.World);
			}
		}
	}
}
