using UnityEngine;
using System.Collections;

public class SwitchModel : MonoBehaviour {

	bool r = false;
	float rot = 0f;

	public void SwitchLeft ()
	{
		TouchRotation trb = GameObject.Find (""+ (int)rot).GetComponent<TouchRotation> ();
		trb.SetInactive ();
		r = true;
		if (rot == 0) rot = 360f;
		rot =  rot - 60f;
		TouchRotation tr = GameObject.Find (""+ (int)rot).GetComponent<TouchRotation> ();
		tr.SetActive ();
	}

	public void SwitchRight ()
	{
		TouchRotation trb = GameObject.Find (""+ (int)rot).GetComponent<TouchRotation> ();
		trb.SetInactive ();
		r = true;
		if (rot == 360) rot = 0f;
		if (rot == 300)
			rot = 0f;
		else
			rot = rot + 60f;
		TouchRotation tr = GameObject.Find (""+ (int)rot).GetComponent<TouchRotation> ();
		tr.SetActive ();
	}

	public void quitGame()
	{
		Application.Quit();
	}

	void Update ()
	{
		if (r)
		{
			transform.rotation = Quaternion.Lerp (this.transform.rotation, Quaternion.Euler (0f, rot, 0f), 3f * Time.deltaTime);
			if (transform.rotation.eulerAngles.y < rot + 0.25f && transform.rotation.eulerAngles.y > rot - 0.25f)
			{
				r = false;
				transform.rotation = Quaternion.Euler(0.0f, rot, 0.0f);

			}
		}
	}
}
