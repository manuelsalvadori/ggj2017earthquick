using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Camera))]

public class Stereo : MonoBehaviour
{
	Camera camL;
	Camera camR;
	private		RenderTexture 		leftEyeRT;   
	private 	RenderTexture		rightEyeRT;
	public 		Material	 		anaglyphMat;
	public float delta = 0.8f;
	GameObject L,R;

	void Start ()
	{
		camL = GameObject.Find("CameraSX").GetComponent<Camera>();
		camR = GameObject.Find ("CameraDX").GetComponent<Camera> ();
		L = GameObject.Find ("CameraSX");
		R = GameObject.Find ("CameraDX");
		leftEyeRT = new RenderTexture (Screen.width, Screen.height, 24);
		rightEyeRT = new RenderTexture (Screen.width, Screen.height, 24);

		camL.targetTexture = leftEyeRT;
		camR.targetTexture = rightEyeRT;

		anaglyphMat.SetTexture ("_LeftTex", leftEyeRT);
		anaglyphMat.SetTexture ("_RightTex", rightEyeRT);
	}
	
	void Update ()
	{
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		Vector3 lp = L.transform.position;
		Vector3 rp = R.transform.position;
		lp.x = -delta / 2;
		rp.x = delta / 2;
		L.transform.position = lp;
		R.transform.position = rp;

		camL.targetTexture = leftEyeRT;
		camR.targetTexture = rightEyeRT;

		anaglyphMat.SetTexture ("_LeftTex", leftEyeRT);
		anaglyphMat.SetTexture ("_RightTex", rightEyeRT);
	}

	void OnRenderImage(RenderTexture src, RenderTexture dest)
	{
		RenderTexture.active = dest;
		GL.PushMatrix();
		GL.LoadOrtho();
		for(int i = 0; i < anaglyphMat.passCount; i++) {
			anaglyphMat.SetPass(i);
			DrawQuad();
		}
		GL.PopMatrix();
	}

	private void DrawQuad() {
		GL.Begin (GL.QUADS);      
		GL.TexCoord2( 0.0f, 0.0f ); GL.Vertex3( 0.0f, 0.0f, 0.0f );
		GL.TexCoord2( 1.0f, 0.0f ); GL.Vertex3( 1.0f, 0.0f, 0.0f );
		GL.TexCoord2( 1.0f, 1.0f ); GL.Vertex3( 1.0f, 1.0f, 0.0f );
		GL.TexCoord2( 0.0f, 1.0f ); GL.Vertex3( 0.0f, 1.0f, 0.0f );
		GL.End();
	} 
}
