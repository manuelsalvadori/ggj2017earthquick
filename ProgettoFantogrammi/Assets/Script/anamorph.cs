using UnityEngine;
using System.Collections;

public class anamorph : MonoBehaviour {

	public float gap = 0.5f; //coefficiente di traslazione frustum
	public float cf = 0.1f; //coefficiente filtro accelerometro
	public float dz = 8f; //coefficiente spostamento view vector lungo l'asse z
	public float dx = 2f; //coefficiente spostamento view vector lungo l'asse x

	public Transform corner_TL;
	public Transform corner_TR;
	public Transform corner_BL;
	public Transform corner_BR;

    public Vector3 pe;

	public Transform lookTarget;

	Camera theCam;
	Vector3 at, at1, acc, vr, vu, vn, va, vb, vc, vd;
	float n,f,l,r,b,t,d;

	void Start ()
	{
		theCam = GetComponent<Camera>();
		acc = Input.acceleration;
		at1 = cf * acc + (1 - cf) * acc; //filtro passa-basso accelerometro
	}

	void Update ()
	{
		Vector3 pa, pb, pc, pd;

		pa = corner_BL.position;
		pb = corner_BR.position;
		pc = corner_TL.position;
		pd = corner_TR.position;

		Matrix4x4 vm = theCam.worldToCameraMatrix;
		acc = Input.acceleration;
		at1 = cf * acc + (1 - cf) * at1; //filtro passa-basso accelerometro

		vm [1, 3] = (dz*at1.y - theCam.transform.position.z); //aggiorno il vettore vista nella view matrix in base all'accelerometro
		vm [0, 3] = (dx*at1.x - theCam.transform.position.x);

		pe = new Vector3 (); //posizione vettore vista

		pe.x = -vm [0 ,3];
		pe.y = -vm [2, 3];
		pe.z = -vm [1, 3];

		theCam.worldToCameraMatrix = vm;

		vr = ( pb - pa ).normalized; // right axis of screen
		vu = ( pc - pa ).normalized; // up axis of screen
		vn = Vector3.Cross( vr, vu ).normalized; // normale dello screen

		//vettori da view point a angoli screen

		va = pa - pe;
		vb = pb - pe;
		vc = pc - pe;
		vd = pd - pe;

		n = -lookTarget.InverseTransformPoint( theCam.transform.position ).z; // distanza dallo screen
		f = theCam.farClipPlane; // distanza dal piano far
		d = Vector3.Dot( va, vn ); // distanza dal view point allo screen

		//distanze dei lati dello screen dal centro:
		l = Vector3.Dot( vr, va ) * n / d; //lato sinistro
		r = Vector3.Dot( vr, vb ) * n / d; //lato destro
		b = Vector3.Dot( vu, va ) * n / d; //lato inferiore
		t = Vector3.Dot( vu, vc ) * n / d; //lato superiore

		Matrix4x4 p = new Matrix4x4(); // Projection matrix
		p[0, 0] = 2.0f * n / ( r - l );
		p[0, 2] = ( r + l ) / ( r - l );
		p[1, 1] = 2.0f * n / ( t - b );
		p[1, 2] = ( t + b ) / ( t - b );
		p[2, 2] = ( f + n ) / ( n - f );
		p[2, 3] = ( 2.0f * f * n / ( n - f ) ) * gap; //gap avvicina l'intero frustum al view point
		p[3, 2] = -1.0f;

		theCam.projectionMatrix = p; // Assign matrix to camera

		//debug: disegno frustum e screen nell'editor

		Debug.DrawLine( pa, pb, Color.green );
		Debug.DrawLine( pb, pd, Color.green );
		Debug.DrawLine( pd, pc, Color.green );
		Debug.DrawLine( pc, pa, Color.green );
		Debug.DrawRay( pe, va, Color.blue );
		Debug.DrawRay( pe, vb, Color.blue );
		Debug.DrawRay( pe, vc, Color.blue );
		Debug.DrawRay( pe, vd, Color.blue );

		DrawFrustum( theCam ); //disegno frustum
	}

	Vector3 ThreePlaneIntersection ( Plane p1, Plane p2, Plane p3 ) { //get the intersection point of 3 planes
		return ( ( -p1.distance * Vector3.Cross( p2.normal, p3.normal ) ) +
		        ( -p2.distance * Vector3.Cross( p3.normal, p1.normal ) ) +
		        ( -p3.distance * Vector3.Cross( p1.normal, p2.normal ) ) ) /
			( Vector3.Dot( p1.normal, Vector3.Cross( p2.normal, p3.normal ) ) );
	}

	void DrawFrustum ( Camera cam ) {
		Vector3[] nearCorners = new Vector3[4]; //Approx'd nearplane corners
		Vector3[] farCorners = new Vector3[4]; //Approx'd farplane corners
		Plane[] camPlanes = GeometryUtility.CalculateFrustumPlanes( cam ); //get planes from matrix
		Plane temp = camPlanes[1]; camPlanes[1] = camPlanes[2]; camPlanes[2] = temp; //swap [1] and [2] so the order is better for the loop

		for ( int i = 0; i < 4; i++ ) {
			nearCorners[i] = ThreePlaneIntersection( camPlanes[4], camPlanes[i], camPlanes[( i + 1 ) % 4] ); //near corners on the created projection matrix
			farCorners[i] = ThreePlaneIntersection( camPlanes[5], camPlanes[i], camPlanes[( i + 1 ) % 4] ); //far corners on the created projection matrix
		}

		for ( int i = 0; i < 4; i++ ) {
			Debug.DrawLine( nearCorners[i], nearCorners[( i + 1 ) % 4], Color.red, Time.deltaTime, false ); //near corners on the created projection matrix
			Debug.DrawLine( farCorners[i], farCorners[( i + 1 ) % 4], Color.red, Time.deltaTime, false ); //far corners on the created projection matrix
			Debug.DrawLine( nearCorners[i], farCorners[i], Color.red, Time.deltaTime, false ); //sides of the created projection matrix
		}
	}
}