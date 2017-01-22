using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TouchRotation : MonoBehaviour
{

	public float rotationRate = 1.5f;
    //public Text testo;

    private void Start()
    {
        transform.rotation = Quaternion.Euler(new Vector3(360*Random.Range(0f,1f), 360 * Random.Range(0f, 1f), 0f));
    }

    void Update ()
	{
		foreach (Touch touch in Input.touches)
		{
            Debug.Log(Screen.currentResolution.width);
            Debug.Log(Screen.currentResolution.height);

            if (touch.phase == TouchPhase.Moved)
			{
                transform.Rotate((touch.deltaPosition.y) / Screen.currentResolution.height * rotationRate * Time.smoothDeltaTime, 0f,  -(touch.deltaPosition.x / Screen.currentResolution.width * rotationRate * Time.smoothDeltaTime), Space.World);
            }
            //testo.text = ((touch.deltaPosition.y) / Screen.currentResolution.height * rotationRate * Time.smoothDeltaTime).ToString();
        }


	}

}
