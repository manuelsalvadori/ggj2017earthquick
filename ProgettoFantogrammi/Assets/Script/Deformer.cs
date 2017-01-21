using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Deformer : MonoBehaviour
{
	private Mesh mesh;
	private Vector3[] vertices, startingVertices;
	private List<Shockwave> shockwaves;

	void Start ()
	{		
		Init ();
	}

	public void Init ()
	{
		shockwaves = new List<Shockwave> ();
		mesh = GetComponent<MeshFilter> ().mesh;

		vertices = mesh.vertices;
		startingVertices = mesh.vertices;
	}

	public void AddShockwave (Shockwave shockwave)
	{
		shockwaves.Add (shockwave);
	}

	public void ProcessShockwave (Shockwave shockwave)
	{	
		float relativeTime = shockwave.currentTime / shockwave.SWDuration;
		
		for (int index = 0; index < vertices.Length; index++) {
			float distanceFromOrigin = Vector3.Distance (shockwave.SWPosition, transform.TransformPoint (startingVertices [index]));

			if (distanceFromOrigin < shockwave.SWRange) {
				Ray ray = new Ray (shockwave.SWPosition, transform.TransformPoint (startingVertices [index]) - shockwave.SWPosition);

				float relativeDistance = 1 - distanceFromOrigin / shockwave.SWRange;
				float rippleEffect = 
					-Mathf.Sin (((relativeTime * Mathf.PI * (shockwave.bounceTime * 2)) - (relativeDistance * Mathf.PI * (shockwave.rippleOffset * 2))) *
					shockwave.rippleFrequency);
				
				vertices [index] = startingVertices [index] +
				((ray.direction * shockwave.SWIntesity * relativeTime * relativeDistance * rippleEffect) / transform.lossyScale.x);
			}

		}  

		mesh.vertices = vertices;
		mesh.RecalculateBounds ();
				
		shockwave.currentTime -= Time.deltaTime;
	}


	void Update ()
	{
		foreach (Shockwave shockwave in shockwaves) {
			ProcessShockwave (shockwave);
		}

		shockwaves.RemoveAll (o => o.currentTime <= 0);
	}

}

