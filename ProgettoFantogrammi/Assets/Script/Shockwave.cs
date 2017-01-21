using UnityEngine;
using System.Collections;

public class Shockwave
{
	public Vector3 SWPosition;
	public float SWIntesity;
	public float SWDuration;
	public float SWRange;
	public float currentTime;

	public Shockwave(Vector3 _SWPosition, float _SWIntesity, float _SWDuration, float _SWRange)
	{
		SWPosition = _SWPosition;
		SWIntesity = _SWIntesity;
		SWDuration = _SWDuration;
		SWRange = _SWRange;
		currentTime = SWDuration;
	}
}

