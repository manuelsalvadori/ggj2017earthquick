using UnityEngine;
using System.Collections;

public class Size : MonoBehaviour {

	public void ScaleDown()
	{
		transform.localScale = new Vector3(0.3f,0.3f,0.3f);
	}
	public void ScaleUp()
	{
		transform.localScale = new Vector3(0.4f,0.4f,0.4f);
	}
}
