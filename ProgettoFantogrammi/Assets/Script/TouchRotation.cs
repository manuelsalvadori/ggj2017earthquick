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
                    if (Mathf.Abs(touch.deltaPosition.y) < Mathf.Abs(touch.deltaPosition.x))
                    {
                        transform.Rotate(0f, touch.deltaPosition.x * rotationRate, 0f, Space.World);
                    }
                    else
                    {
                        transform.Rotate(-(touch.deltaPosition.y) * rotationRate, 0f, 0f, Space.World);

                    }
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
